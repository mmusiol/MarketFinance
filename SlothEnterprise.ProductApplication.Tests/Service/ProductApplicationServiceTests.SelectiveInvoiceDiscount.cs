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
        public void SubmitApplicationForShouldSubmitSelectiveInvoiceDiscount()
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

            const decimal invoiceAmount = 12.3m;
            const decimal advancePercentage = 45.6m;
            const int companyNumber = 12345;

            var application = new SellerApplication()
            {
                Product = new SelectiveInvoiceDiscount()
                {
                    InvoiceAmount = invoiceAmount,
                    AdvancePercentage = advancePercentage,
                },
                CompanyData = new SellerCompanyData()
                {
                    Number = companyNumber
                }

            };


            //ACT
            int result = productApplicationService.SubmitApplicationFor(application);

            //ASSERT
            expectedResult.Should().Be(result);

            selectInvoiceServiceMock.Verify(p => p.SubmitApplicationFor(
                    It.IsAny<string>(),
                    It.IsAny<decimal>(),
                    It.IsAny<decimal>()),
                Times.Once);

            selectInvoiceServiceMock.Verify(p => p.SubmitApplicationFor(
                    companyNumber.ToString(),
                    invoiceAmount,
                    advancePercentage),
                Times.Once);

            confidentialInvoiceWebService.Verify(
                p => p.SubmitApplicationFor(
                    It.IsAny<CompanyDataRequest>(), 
                    It.IsAny<decimal>(), 
                    It.IsAny<decimal>(),
                    It.IsAny<decimal>()), 
                Times.Never());

            businessLoansService.Verify(
                p => p.SubmitApplicationFor(
                    It.IsAny<CompanyDataRequest>(),
                    It.IsAny<LoansRequest>()),
                Times.Never);
        }
    }
}
