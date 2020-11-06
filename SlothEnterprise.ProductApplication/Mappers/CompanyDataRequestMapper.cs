using SlothEnterprise.External;
using SlothEnterprise.ProductApplication.Applications;

namespace SlothEnterprise.ProductApplication.Mappers
{
    public class CompanyDataRequestMapper : ISellerApplicationMapper<CompanyDataRequest>
    {
        public CompanyDataRequest Map(ISellerApplication src) => 
            new CompanyDataRequest
            {
                CompanyFounded = src.CompanyData.Founded,
                CompanyNumber = src.CompanyData.Number,
                CompanyName = src.CompanyData.Name,
                DirectorName = src.CompanyData.DirectorName
            };
    }
}
