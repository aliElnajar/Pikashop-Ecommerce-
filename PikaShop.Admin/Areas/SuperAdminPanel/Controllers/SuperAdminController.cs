#nullable enable

using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using PikaShop.Admin.Areas.AdminPanel.ViewModel;
using PikaShop.Admin.Areas.Identity.Pages.Account;
using PikaShop.Admin.Areas.SuperAdminPanel.ViewModel;
using PikaShop.Common.Pagination;
using PikaShop.Data.Context;
using PikaShop.Data.Context.ContextEntities.Identity;

namespace PikaShop.Admin.Areas.AdminPanel.Controllers
{
	[Area("SuperAdminPanel")]
	[Authorize(Roles = "SuperAdmin,Admin")]
	[Route("[area]/[controller]/[action]")]
	public class SuperAdminController : Controller
	{
		private readonly SignInManager<ApplicationUserEntity> _signInManager;
		private readonly UserManager<ApplicationUserEntity> _userManager;
		readonly RoleManager<ApplicationUserRoleEntity> _roleManager;
		private readonly IUserStore<ApplicationUserEntity> _userStore;
		private readonly IUserEmailStore<ApplicationUserEntity> _emailStore;
		private readonly ILogger<RegisterAdminModel> _logger;
		readonly ApplicationDbContext _context;
		readonly IMapper _mapper;


		public SuperAdminController(UserManager<ApplicationUserEntity> userManager,
			IUserStore<ApplicationUserEntity> userStore,
			SignInManager<ApplicationUserEntity> signInManager,
			ILogger<RegisterAdminModel> logger,
			RoleManager<ApplicationUserRoleEntity> roleManager,
			ApplicationDbContext dbContext, IMapper mapper)
		{

			_userManager = userManager;
			_userStore = userStore;
			_emailStore = GetEmailStore();
			_signInManager = signInManager;
			_logger = logger;
			_roleManager = roleManager;
			_context = dbContext;
			_mapper = mapper;

		}

		// /SuperAdminPanel/SuperAdmin/Index
		[HttpGet]
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Index()
		{
			var admins = GetUsersInRole("Admin").ToList();
			var adminsModel = _mapper.Map<List<ApplicationUserEntityViewModel>>(admins);
			return View(adminsModel);
		}

		[HttpGet]
		[Route("{id:int}")]
		public IActionResult Details(int id)
		{
			var targetUser = _userManager.Users.FirstOrDefault(x => x.Id == id);
			if (targetUser == null)
			{
				return RedirectToAction(nameof(Index));
			}
			var userModel = _mapper.Map<ApplicationUserEntityViewModel>(targetUser);
			return View(userModel);
		}

		[HttpGet]
		public IActionResult Add()
		{
			return View(new ApplicationUserEntityViewModel());
		}

		[HttpPost]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Add(ApplicationUserEntityViewModel userModel)
		{
			if (!ModelState.IsValid)
			{
				return View(userModel);
			}

			var user = CreateUser();
			user.FirstName = userModel.FirstName;
			user.LastName = userModel.LastName;

			await _userStore.SetUserNameAsync(user, userModel.Email, CancellationToken.None);
			await _emailStore.SetEmailAsync(user, userModel.Email, CancellationToken.None);

			var result = await _userManager.CreateAsync(user, userModel.Password);

			if (result.Succeeded)
			{
				_logger.LogInformation("User created a new account with password.");

				// Adding Role Customer after Successfully Registered
				await _userManager.AddToRoleAsync(user, "Admin");


				return RedirectToAction(nameof(Index));
			}
			foreach (var error in result.Errors)
			{
				ModelState.AddModelError(string.Empty, error.Description);
			}
			return View(new ApplicationUserEntityViewModel());
		}

		#region Edit

		//[HttpGet]
		//[Route("{id:int}")]
		//public IActionResult Edit(int id)
		//{
		//	var user = _userManager.Users.FirstOrDefault(x => x.Id == id);
		//	if(user == null)
		//	{
		//		return RedirectToAction(nameof(Index));
		//	}

		//	var userModel = _mapper.Map<ApplicationUserEntityViewModel>(user);
		//	return View(userModel);
		//}


		//[HttpPost]
		//public async Task<IActionResult> ConfirmEdit(ApplicationUserEntityViewModel userModel)
		//{
		//	if(userModel == null)
		//	{
		//		return RedirectToAction(nameof(Index));
		//	}
		//	var user = _userManager.Users.FirstOrDefault(u => u.Id == userModel.Id);
		//	if(user == null || !ModelState.IsValid)
		//	{
		//		return RedirectToAction(nameof(Edit), new { id = userModel.Id });
		//	}
		//	user.FirstName = userModel.FirstName;
		//	user.LastName = userModel.LastName;
		//	if(!string.IsNullOrEmpty(userModel.OldPassword))
		//	{
		//              var result = await _userManager.ChangePasswordAsync(user, userModel.OldPassword ?? "", userModel.Password);
		//          }
		//	_context.Update(user);
		//	_logger.LogInformation("User Updated.");
		//          return RedirectToAction(nameof(Index));
		//      }

		#endregion

		#region Delete

		//[HttpGet]
		//[Route("{id:int}")]
		//[Authorize(Roles = "SuperAdmin")]
		//public IActionResult Delete(int id)
		//{
		//	var user = _userManager.Users.FirstOrDefault(u => u.Id == id);
		//	if (user == null)
		//	{
		//		return RedirectToAction(nameof(Index));
		//	}
		//	var userModel = _mapper.Map<ApplicationUserEntityViewModel>(user);
		//	return View(userModel);
		//}

		//[HttpPost]
		//[Authorize(Roles = "SuperAdmin")]
		//public IActionResult ConfirmDelete(ApplicationUserEntityViewModel userModel)
		//{
		//	var user = _userManager.Users.FirstOrDefault(u => u.Id == userModel.Id);
		//	if (user == null)
		//	{
		//		return RedirectToAction(nameof(Index));
		//	}
		//	var target = _userManager.FindByEmailAsync(userModel.Email);
		//	if (!ModelState.IsValid || target == null)
		//	{
		//		return RedirectToAction(nameof(Delete), new { id = userModel.Id });
		//	}
		//	_userManager.DeleteAsync(user);
		//	_context.SaveChanges();
		//	return RedirectToAction(nameof(Index));
		//}

		#endregion

		public async Task<IActionResult> ManageUserRoles()
		{
			var users = await _userManager.Users.ToListAsync();
			var roles = await _roleManager.Roles.ToListAsync();

			var viewModel = new UsersAndRoles
			{
				Users = users,
				Roles = roles
			};

			return View(viewModel);
		}

		[HttpPost]
		public async Task<IActionResult> SetUserRole(string SelectedUserID, string SelectedUserRole)
		{
			var user = await _userManager.FindByIdAsync(SelectedUserID);
			var role = await _roleManager.FindByIdAsync(SelectedUserRole);

			if (user != null && role != null)
			{
				// Remove user from any existing roles
				var currentRoles = await _userManager.GetRolesAsync(user);
				await _userManager.RemoveFromRolesAsync(user, currentRoles);

				// Assign user to selected role
				var result = await _userManager.AddToRoleAsync(user: user, role: role.Name ?? throw new ArgumentNullException("Role name cannot be null"));

				if (result.Succeeded)
				{
					// Role assignment was successful
					TempData["SuccessMessage"] = $"Role assigned successfully to user {user.UserName}.";
				}
				else
				{
					// Handle errors
					TempData["ErrorMessage"] = $"An error occurred while assigning role to user {user.UserName}.";
				}
			}
			else
			{
				TempData["ErrorMessage"] = "Invalid user or role selected.";
			}

			return RedirectToAction("ManageUserRoles");
		}

		public IQueryable<ApplicationUserEntity> GetUsersInRole(string roleName)
		{
			var usersInRole = _context.UserRoles
				.Join(_context.Roles,
					userRole => userRole.RoleId,
					role => role.Id,
					(userRole, role) => new { UserRole = userRole, Role = role })
				.Join(_context.Users,
					userRoleRole => userRoleRole.UserRole.UserId,
					user => user.Id,
					(userRoleRole, user) => new { UserRoleRole = userRoleRole, User = user })
				.Where(result => result.UserRoleRole.Role.Name == roleName)
				.Select(result => result.User)
				.AsQueryable();

			return usersInRole;
		}

		private ApplicationUserEntity CreateUser()
		{
			try
			{
				return Activator.CreateInstance<ApplicationUserEntity>();
			}
			catch
			{
				throw new InvalidOperationException($"Can't create an instance of '{nameof(ApplicationUserEntity)}'. " +
					$"Ensure that '{nameof(ApplicationUserEntity)}' is not an abstract class and has a parameterless constructor, or alternatively " +
					"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
			}
		}

		private IUserEmailStore<ApplicationUserEntity> GetEmailStore()
		{
			if (!_userManager.SupportsUserEmail)
			{
				throw new NotSupportedException("The default UI requires a user store with email support.");
			}
			return (IUserEmailStore<ApplicationUserEntity>)_userStore;
		}
	}
}