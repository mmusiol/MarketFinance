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
        [Theory]
        [InlineData(true, 123)]
        [InlineData(true, null)]
        [InlineData(false, 123)]
        [InlineData(false, null)]
        public void SubmitApplicationForShouldSubmitConfidentialInvoiceDiscount(bool success, int? applicationId)
        {
            //ARRANGE
            var selectInvoiceServiceMock = new Mock<ISelectInvoiceService>();
            var confidentialInvoiceWebService = new Mock<IConfidentialInvoiceService>();
            var businessLoansService = new Mock<IBusinessLoansService>();

            var resultMock = new Mock<IApplicationResult>();
            resultMock.Setup(p => p.Success).Returns(success);
            resultMock.Setup(p => p.ApplicationId).Returns(applicationId);

            var expectedResult = success ? applicationId ?? -1 : -1;

            businessLoansService.Setup(
                p => p.SubmitApplicationFor(
                    It.IsAny<CompanyDataRequest>(),
                    It.IsAny<LoansRequest>()))
                .Returns(resultMock.Object);           

            var productApplicationService = new ProductApplication.ProductApplicationService(
                selectInvoiceServiceMock.Object, 
                confidentialInvoiceWebService.Object, 
                businessLoansService.Object);

            var companyFounded = DateTime.Now;
            const int companyNumber = 12345;
            const string companyName = "Company Name";
            const string companyDirectorName = "Director Name";
            const decimal interestRatePerAnnum = 123.45m;
            const decimal loanAmount = 56.78m;

            var application = new SellerApplication()
            {
                Product = new BusinessLoans()
                {
                    InterestRatePerAnnum = interestRatePerAnnum,
                    LoanAmount = loanAmount
                },
                CompanyData = new SellerCompanyData()
                {
                    Founded = companyFounded,
                    Number = companyNumber,
                    Name = companyName,
                    DirectorName = companyDirectorName
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
                Times.Once);

            businessLoansService.Verify(
                p => p.SubmitApplicationFor(
                    It.Is<CompanyDataRequest>(q =>
                        (q.CompanyFounded == companyFounded) &&
                        (q.CompanyNumber == companyNumber) &&
                        (q.CompanyName == companyName) &&
                        (q.DirectorName == companyDirectorName)),
                    It.Is<LoansRequest>(q => 
                        (q.InterestRatePerAnnum == interestRatePerAnnum) &&
                        (q.LoanAmount == loanAmount))),
                Times.Once);
        }
    }
}
