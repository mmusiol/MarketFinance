using System;
using FluentAssertions;
using Moq;
using SlothEnterprise.External;
using SlothEnterprise.External.V1;
using SlothEnterprise.ProductApplication.Applications;
using SlothEnterprise.ProductApplication.Products;
using Xunit;

namespace SlothEnterprise.ProductApplication.Tests.Service
{
    public partial class ProductApplicationServiceTests
    {
        [Fact]
        public void SubmitApplicationForShouldThrowInvalidOperationException()
        {
            //ARRANGE
            var selectInvoiceServiceMock = new Mock<ISelectInvoiceService>();
            var confidentialInvoiceWebService = new Mock<IConfidentialInvoiceService>();
            var businessLoansService = new Mock<IBusinessLoansService>();

            const int expectedResult = -123;
            selectInvoiceServiceMock.Setup(p => p.SubmitApplicationFor(
                    It.IsAny<string>(),
                    It.IsAny<decimal>(),
                    It.IsAny<decimal>()))
                .Returns(expectedResult);

            var productApplicationService = new ProductApplicationService(
                selectInvoiceServiceMock.Object,
                confidentialInvoiceWebService.Object,
                businessLoansService.Object);

            var application = new SellerApplication { Product = new Mock<IProduct>().Object };

            //ACT && ASSERT
            Action act = () => productApplicationService.SubmitApplicationFor(application);
            act.Should().Throw<InvalidOperationException>();

            //ASSERT
            selectInvoiceServiceMock.Verify(p => p.SubmitApplicationFor(
                    It.IsAny<string>(),
                    It.IsAny<decimal>(),
                    It.IsAny<decimal>()),
                Times.Never);

            confidentialInvoiceWebService.Verify(
                p => p.SubmitApplicationFor(
                    It.IsAny<CompanyDataRequest>(),
                    It.IsAny<decimal>(),
                    It.IsAny<decimal>(),
                    It.IsAny<decimal>()),
                Times.Never);

            businessLoansService.Verify(
                p => p.SubmitApplicationFor(
                    It.IsAny<CompanyDataRequest>(),
                    It.IsAny<LoansRequest>()),
                Times.Never);
        }
    }
}
