namespace CashFlow.Communication.Response.Login;
public class LoginResponseJson
{
    public string Token { get; set; } = string.Empty;
    public DateTime Expires { get; set; }
}
