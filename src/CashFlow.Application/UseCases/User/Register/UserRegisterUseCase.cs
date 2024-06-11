using AutoMapper;
using CashFlow.Communication.Requests.User;
using CashFlow.Communication.Response.User;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.User;
using CashFlow.Domain.Security.Criptography;
using CashFlow.Domain.Security.Tokens;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;
using FluentValidation.Results;

namespace CashFlow.Application.UseCases.User.Register;
public class UserRegisterUseCase : IUserRegisterUserCase
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITokenGenerator _tokenGenerator;
    private readonly IPasswordEncripter _passwordEncripter;
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;
    private readonly IUserWriteOnlyRepository _userWriteOnlyRepository;

    public UserRegisterUseCase(IMapper mapper, IPasswordEncripter passwordEncripter, IUserWriteOnlyRepository userWriteOnlyRepository, IUserReadOnlyRepository userReadOnlyRepository, IUnitOfWork unitOfWork, ITokenGenerator tokenGenerator)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _tokenGenerator = tokenGenerator;
        _passwordEncripter = passwordEncripter;
        _userReadOnlyRepository = userReadOnlyRepository;
        _userWriteOnlyRepository = userWriteOnlyRepository;
    }

    public async Task<ResponseRegisterUserJson> Execute(RequestRegisterUserJson requestRegisterUserJson)
    {
        await Validate(requestRegisterUserJson);

        var user = _mapper.Map<Domain.Entities.User>(requestRegisterUserJson);
        user.Password = _passwordEncripter.Encrypt(requestRegisterUserJson.Password);

        await _userWriteOnlyRepository.Add(user);

        await _unitOfWork.Commit();

        return new ResponseRegisterUserJson
        {
            Name = user.Name,
            Token = _tokenGenerator.GenerateToken(user),
        };
    }

    private async Task Validate(RequestRegisterUserJson requestRegisterUserJson)
    {
        var validator = new RegisterUserValidator();

        var result = validator.Validate(requestRegisterUserJson);
        
        var existEmail = await _userReadOnlyRepository.ExistsActiveUserWithEmail(requestRegisterUserJson.Email);

        if(existEmail)
        {
            result.Errors.Add(new ValidationFailure(string.Empty, ResourceErrorMessage.EMAIL_ALREADY_REGISTERED));
        }

        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
