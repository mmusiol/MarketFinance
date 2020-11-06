using SlothEnterprise.External;

namespace SlothEnterprise.ProductApplication.Submitters
{
    public class SubmitApplicationResult
    {
        private readonly IApplicationResult _extendedResult;

        private readonly int _simplifiedResult = ErrorIntValue;

        public const int ErrorIntValue = -1;

        public SubmitApplicationResult(int result)
        {
            _simplifiedResult = result;
        }

        public SubmitApplicationResult(IApplicationResult result)
        {
            _extendedResult = result;
        }

        public static implicit operator int(SubmitApplicationResult result)
        {
            if (result._extendedResult == null)
            {
                return result._simplifiedResult;
            }

            return result._extendedResult.Success
                ? result._extendedResult.ApplicationId ?? SubmitApplicationResult.ErrorIntValue
                : SubmitApplicationResult.ErrorIntValue;
        }
    }
}