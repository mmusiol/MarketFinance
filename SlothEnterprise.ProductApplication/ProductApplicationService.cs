using SlothEnterprise.External.V1;
using SlothEnterprise.ProductApplication.Applications;
using SlothEnterprise.ProductApplication.Submitters;

namespace SlothEnterprise.ProductApplication
{
    public class ProductApplicationService
    {
        private readonly SubmitterFactory _submitterFactory;

        public ProductApplicationService(
            ISelectInvoiceService selectInvoiceService,
            IConfidentialInvoiceService confidentialInvoiceWebService,
            IBusinessLoansService businessLoansService)
        {
            /*
             * Better solution would be to leave creating SubmitterFactory to IoC container,
             * but because of lack of insight to dependency root, rest of the code
             * and backward compability requirement can't make that call here.
             */
            _submitterFactory = new SubmitterFactory(selectInvoiceService, confidentialInvoiceWebService, businessLoansService);
        }

        public int SubmitApplicationFor(ISellerApplication application) =>
            _submitterFactory.CreateForApplication(application).Submit(application);
    }
}
