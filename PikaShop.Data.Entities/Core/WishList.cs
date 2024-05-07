using PikaShop.Data.Entities.Audit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PikaShop.Data.Entities.Core
{
    public class WishList : AuditEntity
    {
        public int Quantity { get; set; }
    }
}