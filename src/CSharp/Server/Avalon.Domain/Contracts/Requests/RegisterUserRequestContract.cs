namespace Avalon.Contracts.Requests
{
    public class RegisterUserRequestContract
    {
        public string UserName { get; set; }
        public string HashedPassword { get; set; }
    }
}
