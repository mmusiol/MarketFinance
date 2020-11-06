using SlothEnterprise.External.V1;
using SlothEnterprise.ProductApplication.Applications;
using SlothEnterprise.ProductApplication.Mappers;
using SlothEnterprise.ProductApplication.Products;

namespace SlothEnterprise.ProductApplication.Submitters
{
    public class BusinessLoansApplicationSubmitter : IApplicationSubmitter<BusinessLoans>
    {
        private readonly IBusinessLoansService _businessLoansService;

        public BusinessLoansApplicationSubmitter(IBusinessLoansService businessLoansService)
        {
            _businessLoansService = businessLoansService;
        }

        public SubmitApplicationResult Submit(ISellerApplication application) =>
            new SubmitApplicationResult(
                _businessLoansService.SubmitApplicationFor(
                    new CompanyDataRequestMapper().Map(application),
                    new LoansRequestMapper().Map(application)));
    }
}