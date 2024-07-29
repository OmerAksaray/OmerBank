using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace OmerBank.Controllers
{
    public class BaseController : Controller
    {
        protected int GetCurrentUserId()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
            {
                return userId;
            }
            throw new InvalidOperationException("Kullanıcı ID'si bulunamadı");
        }

    }
}
