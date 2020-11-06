using System;
using FluentAssertions;
using Moq;
using SlothEnterprise.External.V1;
using SlothEnterprise.ProductApplication.Applications;
using SlothEnterprise.ProductApplication.Products;
using SlothEnterprise.ProductApplication.Submitters;
using Xunit;

namespace SlothEnterprise.ProductApplication.Tests.Submitters
{
    public class SubmitterFactoryTests
    {
        private readonly SubmitterFactory _submitterFactory;

        public SubmitterFactoryTests()
        {
            _submitterFactory = new SubmitterFactory(
                new Mock<ISelectInvoiceService>().Object,
                new Mock<IConfidentialInvoiceService>().Object,
                new Mock<IBusinessLoansService>().Object);
        }

        [Theory]
        [InlineData(typeof(SelectiveInvoiceDiscount), typeof(SelectiveInvoiceDiscountApplicationSubmitter))]
        [InlineData(typeof(ConfidentialInvoiceDiscount), typeof(ConfidentialInvoiceDiscountApplicationSubmitter))]
        [InlineData(typeof(BusinessLoans), typeof(BusinessLoansApplicationSubmitter))]
        public void CreateForApplicationShouldCreateCorrectSubmitter(Type productType, Type submitterType)
        {
            //ARRANGE
            var application = new Mock<ISellerApplication>();
            application.Setup(p => p.Product).Returns((IProduct)Activator.CreateInstance(productType));

            //ACT
            IApplicationSubmitter submitter = _submitterFactory.CreateForApplication(application.Object);

            //ASSERT
            submitter.Should().BeOfType(submitterType);
        }

        [Fact]
        public void CreateForApplicationShouldInvalidOperationException()
        {
            //ARRANGE
            var application = new Mock<ISellerApplication>();
            application.Setup(p => p.Product).Returns(new Mock<IProduct>().Object);

            //ACT & ASSERT
            Action act = () => _submitterFactory.CreateForApplication(application.Object);

            act.Should().Throw<InvalidOperationException>();
        }        
    }
}
