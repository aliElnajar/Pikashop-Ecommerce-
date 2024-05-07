using PikaShop.Data.Contracts.UnitsOfWork;
using PikaShop.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PikaShop.Services.Core
{
    public class ProductSpecsServices(IUnitOfWork _uow):IProductSpecsServices
    {
        public IUnitOfWork UnitOfWork { get; set; } = _uow;
    }
}
