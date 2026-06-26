using Bogus;
using CashFlow.Communication.Enums;
using CashFlow.Communication.Requests;

namespace CommonTestUtilities.Requests;

public class RequestRegisterExpenseJsonBuilder
{
    public static RequestRegisterExpenseJson Build()
    {
        var faker = new Faker();

        return new RequestRegisterExpenseJson
        {
            Title = faker.Commerce.Product(),
            Description = faker.Commerce.ProductDescription(),
            Date = faker.Date.Past(),
            Amount = faker.Random.Decimal(1, 1000),
            PaymentType = faker.PickRandom<PaymentType>()
        };
    }
}
