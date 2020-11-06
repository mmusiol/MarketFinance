using FluentAssertions;
using Moq;
using SlothEnterprise.External.V1;
using SlothEnterprise.ProductApplication.Applications;
using SlothEnterprise.ProductApplication.Products;
using SlothEnterprise.ProductApplication.Submitters;
using Xunit;

namespace SlothEnterprise.ProductApplication.Tests.Submitters
{
    public class SelectiveInvoiceDiscountApplicationSubmitterSubmitterTests
    {
        [Fact]
        public void SubmitShouldReturnExpectedValue()
        {
            //ARRANGE
            const int serviceResult = 123;

            var selectInvoiceServiceMock = new Mock<ISelectInvoiceService>();
            selectInvoiceServiceMock
                .Setup(p => p.SubmitApplicationFor(It.IsAny<string>(), It.IsAny<decimal>(), It.IsAny<decimal>()))
                .Returns(serviceResult);

            var submitter = new SelectiveInvoiceDiscountApplicationSubmitter(selectInvoiceServiceMock.Object);

            var companyDataMock = new Mock<ISellerCompanyData>();
            companyDataMock.Setup(p => p.Number).Returns(123);

            var product = new SelectiveInvoiceDiscount
            {
                InvoiceAmount = 12.34m,
                AdvancePercentage = 56.78m,
            };

            var applicationMock = new Mock<ISellerApplication>();
            applicationMock.Setup(p => p.CompanyData).Returns(companyDataMock.Object);
            applicationMock.Setup(p => p.Product).Returns(product);

            var expectedResult = new SubmitApplicationResult(serviceResult);

            //ACT
            SubmitApplicationResult result = submitter.Submit(applicationMock.Object);

            //ASSERT
            ((int)expectedResult).Should().Be((int)result);

        }
    }
}
