using SlothEnterprise.External.V1;
using SlothEnterprise.ProductApplication.Applications;
using SlothEnterprise.ProductApplication.Products;

namespace SlothEnterprise.ProductApplication.Submitters
{
    public class SelectiveInvoiceDiscountApplicationSubmitter : IApplicationSubmitter<SelectiveInvoiceDiscount>
    {
        private readonly ISelectInvoiceService _selectInvoiceService;

        public SelectiveInvoiceDiscountApplicationSubmitter(ISelectInvoiceService selectInvoiceService)
        {
            _selectInvoiceService = selectInvoiceService;
        }

        public SubmitApplicationResult Submit(ISellerApplication application)
        {
            var product = (SelectiveInvoiceDiscount)application.Product;
            return new SubmitApplicationResult(
                _selectInvoiceService.SubmitApplicationFor(
                    application.CompanyData.Number.ToString(), 
                    product.InvoiceAmount, 
                    product.AdvancePercentage));
        }
    }
}