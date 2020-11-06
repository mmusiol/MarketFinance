using SlothEnterprise.External;
using SlothEnterprise.ProductApplication.Applications;
using SlothEnterprise.ProductApplication.Products;

namespace SlothEnterprise.ProductApplication.Mappers
{
    public class LoansRequestMapper : ISellerApplicationMapper<LoansRequest>
    {
        public LoansRequest Map(ISellerApplication src)
        {
            var product = (BusinessLoans)src.Product;
            return new LoansRequest
            {
                InterestRatePerAnnum = product.InterestRatePerAnnum,
                LoanAmount = product.LoanAmount
            };
        }
    }
}