using SlothEnterprise.External.V1;
using SlothEnterprise.ProductApplication.Applications;
using SlothEnterprise.ProductApplication.Mappers;
using SlothEnterprise.ProductApplication.Products;

namespace SlothEnterprise.ProductApplication.Submitters
{
    public class ConfidentialInvoiceDiscountApplicationSubmitter : IApplicationSubmitter<ConfidentialInvoiceDiscount>
    {
        private readonly IConfidentialInvoiceService _confidentialInvoiceWebService;

        public ConfidentialInvoiceDiscountApplicationSubmitter(IConfidentialInvoiceService confidentialInvoiceWebService)
        {
            _confidentialInvoiceWebService = confidentialInvoiceWebService;
        }

        public SubmitApplicationResult Submit(ISellerApplication application)
        {
            var product = (ConfidentialInvoiceDiscount)application.Product;
            return new SubmitApplicationResult(
                _confidentialInvoiceWebService.SubmitApplicationFor(
                    new CompanyDataRequestMapper().Map(application),
                    product.TotalLedgerNetworth,
                    product.AdvancePercentage,
                    product.VatRate));
        }
    }
}