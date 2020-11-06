using System;
using FluentAssertions;
using Moq;
using SlothEnterprise.External;
using SlothEnterprise.ProductApplication.Applications;
using SlothEnterprise.ProductApplication.Mappers;
using Xunit;

namespace SlothEnterprise.ProductApplication.Tests.Mappers
{
    public class CompanyDataRequestMapperTests
    {
        [Fact]
        public void ShouldMapProperties()
        {
            //ARRANGE
            var expectedResult = new CompanyDataRequest
            {
                CompanyFounded = DateTime.Now,
                CompanyNumber = 123, 
                CompanyName = "Company name",
                DirectorName = "Director Name"
            };

            var companyDataMock = new Mock<ISellerCompanyData>();
            companyDataMock.Setup(p => p.Founded).Returns(expectedResult.CompanyFounded);
            companyDataMock.Setup(p => p.Number).Returns(expectedResult.CompanyNumber);
            companyDataMock.Setup(p => p.Name).Returns(expectedResult.CompanyName);
            companyDataMock.Setup(p => p.DirectorName).Returns(expectedResult.DirectorName);

            var src = new Mock<ISellerApplication>();
            src.Setup(p => p.CompanyData).Returns(companyDataMock.Object);

            //ACT
            var result = new CompanyDataRequestMapper().Map(src.Object);

            //ASSERT
            expectedResult.Should().BeEquivalentTo(result);
        }
    }
}
