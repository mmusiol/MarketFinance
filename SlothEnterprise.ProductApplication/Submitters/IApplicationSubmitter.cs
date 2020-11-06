using SlothEnterprise.ProductApplication.Applications;
using SlothEnterprise.ProductApplication.Products;

namespace SlothEnterprise.ProductApplication.Submitters
{
    public interface IApplicationSubmitter
    {
        SubmitApplicationResult Submit(ISellerApplication application);
    }

    public interface IApplicationSubmitter<T>: IApplicationSubmitter where T: IProduct
    {
    }
}
