using System;
using SlothEnterprise.External.V1;
using SlothEnterprise.ProductApplication.Applications;
using SlothEnterprise.ProductApplication.Products;

namespace SlothEnterprise.ProductApplication.Submitters
{
    public class SubmitterFactory
    {
        private readonly ISelectInvoiceService _selectInvoiceService;
        private readonly IConfidentialInvoiceService _confidentialInvoiceWebService;
        private readonly IBusinessLoansService _businessLoansService;

        public SubmitterFactory(
            ISelectInvoiceService selectInvoiceService, 
            IConfidentialInvoiceService confidentialInvoiceWebService, 
            IBusinessLoansService businessLoansService)
        {
            _selectInvoiceService = selectInvoiceService;
            _confidentialInvoiceWebService = confidentialInvoiceWebService;
            _businessLoansService = businessLoansService;
        }

        public IApplicationSubmitter CreateForApplication(ISellerApplication application)
        {
            //In production solution instead of creating submit handlers using I'd retrieve them from IoC container:
            /*
            
            var productType = application.Product.GetType();
            var genericHandlerType = typeof(IApplicationSubmitter<>);
            var handlerType = genericHandlerType.MakeGenericType(productType);

            try  
            {  
                return (IApplicationSubmitter)container.Resolve(handlerType);
            }
            catch (ResolutionFailedException)
            {
                throw new InvalidOperationException();
            }

             */

            switch (application.Product)
            {
                case SelectiveInvoiceDiscount selectiveInvoiceDiscount:
                    return new SelectiveInvoiceDiscountApplicationSubmitter(_selectInvoiceService);
                case ConfidentialInvoiceDiscount confidentialInvoiceDiscount:
                    return new ConfidentialInvoiceDiscountApplicationSubmitter(_confidentialInvoiceWebService);
                case BusinessLoans businessLoans:
                    return new BusinessLoansApplicationSubmitter(_businessLoansService);
                default:
                    throw new InvalidOperationException();
            }
        }
    }
}
