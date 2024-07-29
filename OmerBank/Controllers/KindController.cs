using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OmerBank.Models.Context;
using OmerBank.Models.Entities;
using OmerBank.Repository;

namespace OmerBank.Controllers
{
    [Authorize]
    public class KindController : BaseController
    {
        private readonly IKindRepository _kindRepository;
        private readonly MyContext _context;

        public KindController(IKindRepository kindRepository, MyContext context)
        {
            _kindRepository = kindRepository;
            _context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {   
            return View();
        }
        [HttpPost]
        public IActionResult Index( string name,  int accountId)
        {
            accountId = GetCurrentUserId();
            var control=_context.Kinds.Where(a=>a.Name == name && a.AccountID==accountId).FirstOrDefault();
            if (control != null) {

                throw new Exception("Zaten böyle bir döviz hesabınız var!");
            
            }
            Kind myKind = new Kind(name, 0, accountId);
            _kindRepository.AddAsync(myKind);
            _context.SaveChanges();
            return Redirect("account/index");
        }
    }
}
