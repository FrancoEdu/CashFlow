using CashFlow.Communication.Requests.Login;
using CashFlow.Communication.Response.Login;
using CashFlow.Domain.Repositories.User;
using CashFlow.Domain.Security.Criptography;
using CashFlow.Domain.Security.Tokens;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;

namespace CashFlow.Application.UseCases.Login;
public class LoginUseCase : ILoginUseCase
{
    private readonly IPasswordEncripter _encripter;
    private readonly ITokenGenerator _tokenGenerator;
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;

    public LoginUseCase(ITokenGenerator tokenGenerator, IUserReadOnlyRepository userReadOnlyRepository, IPasswordEncripter encripter)
    {
        _encripter = encripter;
        _tokenGenerator = tokenGenerator;
        _userReadOnlyRepository = userReadOnlyRepository;
    }

    public async Task<LoginResponseJson> Execute(LoginRequestJson loginRequest)
    {
        Validate(loginRequest);

        var user = await _userReadOnlyRepository.FindByEmail(loginRequest.Email) ?? throw new InvalidLoginException();

        var correctPwd = _encripter.VerifyEncrypt(loginRequest.Password, user.Password);

        if (!correctPwd) throw new InvalidLoginException();

        return new LoginResponseJson
        {
            Token = _tokenGenerator.GenerateToken(user),
            Expires = _tokenGenerator.ExpiresTime()
        };
    }

    private void Validate(LoginRequestJson request)
    {
        var validator = new LoginValidator();
        var result = validator.Validate(request);
        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
