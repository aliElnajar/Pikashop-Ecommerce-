using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PikaShop.Services.Contracts
{
    public interface IStripeService : IServices
    {
        PaymentIntent CreatePaymentIntent(decimal amount, string currency, string stripeToken);
    }
}
