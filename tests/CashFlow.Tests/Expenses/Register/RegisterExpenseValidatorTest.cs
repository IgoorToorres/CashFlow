using CashFlow.Application.UseCases.Expenses.Register;
using CashFlow.Communication.Enums;
using CashFlow.Communication.Requests;
using CashFlow.Exception.Resources;
using CommonTestUtilities.Requests;
using FluentAssertions;
using Xunit.Sdk;

namespace CashFlow.Tests.Expenses.Register;

public class RegisterExpenseValidatorTest
{
    [Fact]
    public void Success()
    {
        //Arrange
        var validator = new RegisterExpenseValidator();
        var request = RequestRegisterExpenseJsonBuilder.Build();

        //Act
        var result = validator.Validate(request);

        //Assert
        result.IsValid.Should().BeTrue();
    }

    [Theory]
    [InlineData("")]
    [InlineData("    ")]
    public void ErrorTitleEmpty(string title)
    {
        //Arrange
        var validator = new RegisterExpenseValidator();
        var request = RequestRegisterExpenseJsonBuilder.Build();
        request.Title = title;

        //Act
        var result = validator.Validate(request);

        //Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourcesErrorMessages.TITLE_REQUIRED));
    }

    [Fact]
    public void ErrorDateFuture()
    {
        //Arrange
        var validator = new RegisterExpenseValidator();
        var request = RequestRegisterExpenseJsonBuilder.Build();
        request.Date = DateTime.UtcNow.AddDays(1);

        //Act
        var result = validator.Validate(request);

        //Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourcesErrorMessages.EXPENSES_CANNOT_FOR_THE_FUTURE));
    }

    [Fact]
    public void ErrorPaymentTypeInvalid()
    {
        //Arrange
        var validator = new RegisterExpenseValidator();
        var request = RequestRegisterExpenseJsonBuilder.Build();
        request.PaymentType = (PaymentType)700;

        //Act
        var result = validator.Validate(request);

        //Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourcesErrorMessages.PAYMENT_TYPE_INVALID));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-2)]
    public void ErrorAmountInvalid(decimal amount)
    {
        //Arrange
        var validator = new RegisterExpenseValidator();
        var request = RequestRegisterExpenseJsonBuilder.Build();
        request.Amount = amount;

        //Act
        var result = validator.Validate(request);

        //Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourcesErrorMessages.AMOUNT_MUST_BE_GREATER_THAN_ZERO));
    }
}