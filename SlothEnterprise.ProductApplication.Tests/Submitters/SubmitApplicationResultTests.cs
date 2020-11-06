using FluentAssertions;
using Moq;
using SlothEnterprise.External;
using SlothEnterprise.ProductApplication.Submitters;
using Xunit;

namespace SlothEnterprise.ProductApplication.Tests.Submitters
{
    public class SubmitApplicationResultTests
    {
        [Fact]
        public void ImplicitIntCastShouldReturnSimplifiedResult()
        {
            const int expectedResult = 12345;
            var result = new SubmitApplicationResult(expectedResult);
            expectedResult.Should().Be((int) result);
        }

        [Fact]
        public void ImplicitIntCastShouldReturnErrorIntValue()
        {
            //ARRANGE
            var applicationResultMock = new Mock<IApplicationResult>();
            applicationResultMock.Setup(p => p.Success).Returns(false);

            //ACT
            var result = new SubmitApplicationResult(applicationResultMock.Object);

            //ASSERT
            ((int)result).Should().Be(SubmitApplicationResult.ErrorIntValue);
        }

        [Fact]
        public void ImplicitIntCastShouldReturnErrorIntValueInCaseOfLackOfApplicationId()
        {
            //ARRANGE
            var applicationResultMock = new Mock<IApplicationResult>();
            applicationResultMock.Setup(p => p.Success).Returns(true);

            //ACT
            var result = new SubmitApplicationResult(applicationResultMock.Object);

            //ASSERT
            ((int)result).Should().Be(SubmitApplicationResult.ErrorIntValue);
        }

        [Fact]
        public void ImplicitIntCastShouldReturnApplicationId()
        {
            //ARRANGE
            const int applicationId = 123456;

            var applicationResultMock = new Mock<IApplicationResult>();
            applicationResultMock.Setup(p => p.Success).Returns(true);
            applicationResultMock.Setup(p => p.ApplicationId).Returns(applicationId);

            //ACT
            var result = new SubmitApplicationResult(applicationResultMock.Object);

            //ASSERT
            ((int)result).Should().Be(applicationId);
        }
    }
}
