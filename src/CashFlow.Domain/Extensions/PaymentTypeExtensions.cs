using CashFlow.Domain.Enums;
using CashFlow.Domain.ResourcesMessages;

namespace CashFlow.Domain.Extensions;
public static class PaymentTypeExtensions
{
    public static string PaymentTypeToString(this PaymentType paymentType)
    {
        return paymentType switch
        {
            PaymentType.Cash => ResourceReportGenerateMessages.CASH,
            PaymentType.CreditCard => ResourceReportGenerateMessages.CREDIT_CARD,
            PaymentType.DebitCard => ResourceReportGenerateMessages.DEBIT_CARD,
            PaymentType.EletronicTransfer => ResourceReportGenerateMessages.ELETRONIC_TRANSFER,
            _ => string.Empty
        };
    }
}