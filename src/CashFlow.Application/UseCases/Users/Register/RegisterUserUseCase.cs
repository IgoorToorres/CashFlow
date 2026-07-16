using AutoMapper;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.User;
using CashFlow.Domain.Security.Cryptography;
using CashFlow.Exception.ExceptionBase;
using CashFlow.Exception.Resources;

namespace CashFlow.Application.UseCases.Users.Register;

public class RegisterUserUseCase(
        IMapper mapper,
        IPasswordEncripter passwordEncripter,
        IUserReadOnlyrepository readOnlyrepository,
        IUserWriteOnlyRepository writeOnlyRepository,
        IUnitOfWork unitOfWork
    ) : IRegisterUserUseCase
{
    private readonly IMapper _mapper = mapper;
    private readonly IPasswordEncripter _passwordEncripter = passwordEncripter;
    private readonly IUserReadOnlyrepository _readOnlyrepository = readOnlyrepository;
    private readonly IUserWriteOnlyRepository _writeOnlyRepository = writeOnlyRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request)
    {
        await Validate(request);

        var user = _mapper.Map<Domain.Entities.User>(request);
        user.Password = _passwordEncripter.Encrypt(request.Password);
        user.UserIdentifier = Guid.NewGuid();
        
        await _writeOnlyRepository.Add(user);
        await _unitOfWork.Commit();

        return new ResponseRegisteredUserJson
        {
            Name = user.Name,
        };
    }

    private async Task Validate(RequestRegisterUserJson request)
    {
        var result = new RegisterUserValidator().Validate(request);

        var emailExist = await _readOnlyrepository.ExistActiveUserWithEmail(request.Email);
        if (emailExist)
        {
            result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, ResourcesErrorMessages.EMAIL_ALREADY_REGISTERED));
        }

        if(result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }  
    }
}
