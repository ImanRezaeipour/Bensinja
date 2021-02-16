namespace BestInja.Model.UserManagment
{
    public class CreateUserCommand
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public CreateUserCommand(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
    }
}