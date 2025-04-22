using System.Security.Claims;

namespace WebMVC.Services
{
    public class UserService
    {
        IHttpContextAccessor _httpContextAccessor { get; set; }

        public UserService(IHttpContextAccessor httpContext)
        {
            _httpContextAccessor = httpContext;
        }

        public int? GetUserId()
        {
            var userId = _httpContextAccessor?.HttpContext?.User.FindFirstValue("id");
            if (userId == null) 
                return null;
            return int.Parse(userId);
        }
    }
}
