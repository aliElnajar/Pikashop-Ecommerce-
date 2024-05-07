using PikaShop.Data.Contracts.UnitsOfWork;
using PikaShop.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PikaShop.Services.Core
{
    public class CategorySpecsServices(IUnitOfWork _uow) : ICategorySpecsServices
    {
        public IUnitOfWork UnitOfWork { get; set; } = _uow;
    }
}
