using FluentAssertions;
using Moq;
using SlothEnterprise.External;
using SlothEnterprise.ProductApplication.Applications;
using SlothEnterprise.ProductApplication.Mappers;
using SlothEnterprise.ProductApplication.Products;
using Xunit;

namespace SlothEnterprise.ProductApplication.Tests.Mappers
{
    public class LoansRequestMapperTests
    {
        [Fact]
        public void ShouldMapProperties()
        {
            //ARRANGE
            var expectedResult = new LoansRequest
            {
                InterestRatePerAnnum = 12.34m,
                LoanAmount = 56.78m
            };

            var productMock = new BusinessLoans
            {
                InterestRatePerAnnum = expectedResult.InterestRatePerAnnum,
                LoanAmount = expectedResult.LoanAmount
            };

            var src = new Mock<ISellerApplication>();
            src.Setup(p => p.Product).Returns(productMock);

            //ACT
            var result = new LoansRequestMapper().Map(src.Object);

            //ASSERT
            expectedResult.Should().BeEquivalentTo(result);
        }
    }
}
