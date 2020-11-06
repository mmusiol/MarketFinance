using SlothEnterprise.ProductApplication.Applications;

namespace SlothEnterprise.ProductApplication.Mappers
{
    internal interface IMapper<in TFrom, out TTo> where TFrom : class where TTo : class
    {
        TTo Map(TFrom src);
    }

    internal interface ISellerApplicationMapper<out TTo>: IMapper<ISellerApplication, TTo> where TTo : class
    {
        
    }
}