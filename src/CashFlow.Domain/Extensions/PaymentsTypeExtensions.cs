using CashFlow.Domain.Enums;
using CashFlow.Domain.Resources;

public static class PaymentTypeExtensions
{
    public static string PaymentTypeToString(this PaymentType paymentType)
    {
        return paymentType switch
        {
            PaymentType.Cash => ResourceReportGenerationMessages.PAYMENT_TYPE_CASH,
            PaymentType.CreditCard => ResourceReportGenerationMessages.PAYMENT_TYPE_CREDIT_CARD,
            PaymentType.DebitCard => ResourceReportGenerationMessages.PAYMENT_TYPE_DEBIT_CARD,
            PaymentType.EletronicTransfer => ResourceReportGenerationMessages.PAYMENT_TYPE_ELECTRONIC_TRANSFER,
            _ => string.Empty,
        };
    }
}