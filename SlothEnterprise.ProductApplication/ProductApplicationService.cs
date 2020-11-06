﻿using System;
using SlothEnterprise.External.V1;
using SlothEnterprise.ProductApplication.Applications;
using SlothEnterprise.ProductApplication.Mappers;
using SlothEnterprise.ProductApplication.Products;

namespace SlothEnterprise.ProductApplication
{
    public class ProductApplicationService
    {
        private readonly ISelectInvoiceService _selectInvoiceService;
        private readonly IConfidentialInvoiceService _confidentialInvoiceWebService;
        private readonly IBusinessLoansService _businessLoansService;

        public ProductApplicationService(ISelectInvoiceService selectInvoiceService, IConfidentialInvoiceService confidentialInvoiceWebService, IBusinessLoansService businessLoansService)
        {
            _selectInvoiceService = selectInvoiceService;
            _confidentialInvoiceWebService = confidentialInvoiceWebService;
            _businessLoansService = businessLoansService;
        }

        public int SubmitApplicationFor(ISellerApplication application)
        {
            switch (application.Product)
            {
                case SelectiveInvoiceDiscount selectiveInvoiceDiscount:
                    return SubmitSelectiveInvoiceDiscountApplicationFor(application);
                case ConfidentialInvoiceDiscount confidentialInvoiceDiscount:
                    return SubmitConfidentialInvoiceDiscountApplicationFor(application);
                case BusinessLoans businessLoans:
                    return SubmitBusinessLoansApplicationFor(application);
                default:
                    throw new InvalidOperationException();
            }
        }

        private int SubmitSelectiveInvoiceDiscountApplicationFor(ISellerApplication application)
        {
            var product = (SelectiveInvoiceDiscount)application.Product;
            return _selectInvoiceService.SubmitApplicationFor(application.CompanyData.Number.ToString(), product.InvoiceAmount, product.AdvancePercentage);
        }

        private int SubmitConfidentialInvoiceDiscountApplicationFor(ISellerApplication application)
        {
            var product = (ConfidentialInvoiceDiscount) application.Product;
            var result = _confidentialInvoiceWebService.SubmitApplicationFor(
                new CompanyDataRequestMapper().Map(application),
                product.TotalLedgerNetworth,
                product.AdvancePercentage,
                product.VatRate);

            return (result.Success) ? result.ApplicationId ?? -1 : -1;
        }

        private int SubmitBusinessLoansApplicationFor(ISellerApplication application)
        {
            var result = _businessLoansService.SubmitApplicationFor(
                new CompanyDataRequestMapper().Map(application),
                new LoansRequestMapper().Map(application));
            return (result.Success) ? result.ApplicationId ?? -1 : -1;
        }
    }
}
