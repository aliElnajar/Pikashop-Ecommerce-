using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PikaShop.Data.Contracts.UnitsOfWork;

namespace PikaShop.Services.Contracts
{
    public interface IServices
    {
        public IUnitOfWork UnitOfWork { get; }
    }
}
