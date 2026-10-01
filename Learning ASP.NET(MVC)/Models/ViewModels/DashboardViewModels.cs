namespace Learning_ASP.NET_MVC_.Models.ViewModels
{
    public class DashboardViewModels
    {
        public List<UserViewModels> Users { get; set; } = new List<UserViewModels>();
        public int TotalUsers { get; set; }
    }
}

