using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PikaShop.Data.Context.Enums
{
    public enum UserDiscriminator
    {
        User = 0,
        Admin,
        Customer,
        DeliveryPerson
    }
}
