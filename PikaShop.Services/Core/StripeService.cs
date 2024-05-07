using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PikaShop.Services.Core
{
    using PikaShop.Data.Contracts.UnitsOfWork;
    using PikaShop.Services.Contracts;
    using Stripe;

    public class StripeService : IStripeService
    {
        private readonly string _stripeSecretKey;

        public StripeService(string stripeSecretKey)
        {
            _stripeSecretKey = stripeSecretKey;
        }

        public IUnitOfWork UnitOfWork => throw new NotImplementedException();

        public PaymentIntent CreatePaymentIntent(decimal amount, string currency, string stripeToken)
        {
            StripeConfiguration.ApiKey = _stripeSecretKey;

            var options = new PaymentIntentCreateOptions
            {
                Amount = (long)(amount * 100), // Stripe requires the amount in cents
                Currency = currency,
                PaymentMethod = stripeToken,
                ConfirmationMethod = "manual", // You can customize this based on your needs
                Confirm = true,
            };

            var service = new PaymentIntentService();
            var paymentIntent = service.Create(options);

            return paymentIntent;
        }
    }

}
