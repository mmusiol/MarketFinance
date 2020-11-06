using System;
using FluentAssertions;
using Moq;
using SlothEnterprise.External;
using SlothEnterprise.External.V1;
using SlothEnterprise.ProductApplication.Applications;
using SlothEnterprise.ProductApplication.Products;
using SlothEnterprise.ProductApplication.Submitters;
using Xunit;

namespace SlothEnterprise.ProductApplication.Tests.Submitters
{
    public class ConfidentialInvoiceDiscountApplicationSubmitterTests
    {
        [Theory]
        [InlineData(true, 123)]
        [InlineData(true, null)]
        [InlineData(false, 123)]
        [InlineData(false, null)]
        public void SubmitShouldReturnExpectedValue(bool success, int? applicationId)
        {
            //ARRANGE
            var serviceResult = new Mock<IApplicationResult>();
            serviceResult.Setup(p => p.Success).Returns(success);
            serviceResult.Setup(p => p.ApplicationId).Returns(applicationId);

            var confidentialInvoiceWebServiceMock = new Mock<IConfidentialInvoiceService>();
            confidentialInvoiceWebServiceMock
                .Setup(p => p.SubmitApplicationFor(It.IsAny<CompanyDataRequest>(), It.IsAny<decimal>(), It.IsAny<decimal>(), It.IsAny<decimal>()))
                .Returns(serviceResult.Object);

            var submitter = new ConfidentialInvoiceDiscountApplicationSubmitter(confidentialInvoiceWebServiceMock.Object);

            var companyDataMock = new Mock<ISellerCompanyData>();
            companyDataMock.Setup(p => p.Founded).Returns(DateTime.Now);
            companyDataMock.Setup(p => p.Number).Returns(123);
            companyDataMock.Setup(p => p.Name).Returns("Company Name");
            companyDataMock.Setup(p => p.DirectorName).Returns("Company Director Name");

            var product = new ConfidentialInvoiceDiscount
            {
                TotalLedgerNetworth = 12.34m,
                AdvancePercentage = 56.78m,
                VatRate = 90.12m
            };

            var applicationMock = new Mock<ISellerApplication>();
            applicationMock.Setup(p => p.CompanyData).Returns(companyDataMock.Object);
            applicationMock.Setup(p => p.Product).Returns(product);

            var expectedResult = new SubmitApplicationResult(serviceResult.Object);

            //ACT
            SubmitApplicationResult result = submitter.Submit(applicationMock.Object);

            //ASSERT
            ((int)expectedResult).Should().Be((int)result);

        }
    }
}
