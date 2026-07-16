namespace CashFlow.Domain.Repositories.User;

public interface IUserReadOnlyrepository
{
    Task<bool> ExistActiveUserWithEmail(string email);
}