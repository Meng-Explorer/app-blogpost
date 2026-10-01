namespace Learning_ASP.NET_MVC_.Models.ViewModels
{
    public class UserViewModels
    {
        public string? Id { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public IList<string>? Roles { get; set; }
    }
}

