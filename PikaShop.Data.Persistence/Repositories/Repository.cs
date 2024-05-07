
using System.Formats.Asn1;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using PikaShop.Data.Context;
using PikaShop.Data.Contracts.Partial;
using PikaShop.Data.Contracts.Repositories;
using PikaShop.Data.Entities.Audit;
using PikaShop.Data.Contracts;

namespace PikaShop.Data.Persistence.Repositories
{
	public class Repository<TEntity, TKey>(ApplicationDbContext _context)
		: IRepository<TEntity, TKey>, ISoftDelete<TEntity, TKey>
		where TEntity : class
	{
		protected ApplicationDbContext context = _context;

		protected DbSet<TEntity> entities = _context.Set<TEntity>();

		public IQueryable<TEntity> GetSet()
		{
			return entities.AsQueryable();
		}

		#region Create
		public virtual void Create(TEntity entity, string username = "system")
		{
			if(typeof(TEntity).IsSubclassOf(typeof(AuditEntity)))
			{
				AuditEntity? audit = entity as AuditEntity;
				if(audit != null)
				{
					audit.DateCreated = DateTime.Now;
					audit.CreatedBy = username;
				}
			}
			entities.Add(entity);
		}

		public virtual void CreateRange(IEnumerable<TEntity> _entities, string username = "system")
		{
			foreach (TEntity entity in _entities)
			{
				if (typeof(TEntity).IsSubclassOf(typeof(AuditEntity)))
				{
					AuditEntity? audit = entity as AuditEntity;
					if (audit != null)
					{
						audit.DateCreated = DateTime.Now;
						audit.CreatedBy = username;
					}
				}
			}
			entities.AddRange(_entities);
		}
		#endregion

		#region Read
		public virtual IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
		{
			return entities.Where(predicate);
		}

		public virtual IQueryable<TEntity> GetAll()
		{
			return entities;
		}

		public virtual TEntity? GetById(TKey id)
		{
			return entities.Find(id);
		}
		#endregion

		#region Update
		public virtual void Update(TEntity entity)
		{
			var entry = context.Entry(entity);
			if (entry != null)
			{
				if(entry.State == EntityState.Detached || entry.State == EntityState.Added)
				{
					entities.Add(entity);
				}
				else if(entry.State == EntityState.Modified)
				{
					entities.Update(entity);
				}
				else if (entry.State == EntityState.Deleted)
				{
					 entities.Remove(entity);
				}
			}
		}
		#endregion

		#region Update Audit
		public virtual void UpdateAudit(TEntity entity, string username = "system")
		{
			if (typeof(TEntity).IsSubclassOf(typeof(AuditEntity)))
			{
				AuditEntity? audit = entity as AuditEntity;
				if (audit != null)
				{
					audit.DateModified = DateTime.Now;
					audit.ModifiedBy = username;
					entities.Update(entity);
				}
			}
		}

		public virtual void UpdateAuditById(TKey id, string username = "system")
		{
			var target = GetById(id);
			if (target != null)
			{
				UpdateAudit(target, username);
			}
		}
		#endregion

		#region Delete
		public virtual void Delete(TEntity entity)
		{
			entities.Remove(entity);
		}

		public virtual void DeleteById(TKey id)
		{
			var target = GetById(id);
			if (target != null)
			{
				entities.Remove(target);
			}
		}

		public virtual void DeleteRange(IEnumerable<TEntity> _entities)
		{
			entities.RemoveRange(_entities);
		}

		#endregion

		#region Soft Delete
		public virtual void SoftDeleteById(TKey id, string username = "system")
		{
			if(typeof(IEntitySoftDelete).IsAssignableFrom(typeof(TEntity)))
			{
				var target = GetById(id);
				if(target != null)
				{
					IEntitySoftDelete? softDeleteTarget = target as IEntitySoftDelete;
					if (softDeleteTarget != null)
					{
						softDeleteTarget.IsDeleted = true;
						UpdateAudit(target, username);
					}
				}
			}
		}

		public virtual void SoftDelete(TEntity entity, string username = "system")
		{
			if (typeof(IEntitySoftDelete).IsAssignableFrom(typeof(TEntity)))
			{
				IEntitySoftDelete? softDeleteTarget = entity as IEntitySoftDelete;
				if (softDeleteTarget != null)
				{
					softDeleteTarget.IsDeleted = true;
					UpdateAudit(entity, username);
				}
			}
		}
		#endregion
	}
}
