using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using testSqlite.DBModels;
using testSqlite.Models;
using testSqlite.Services;

namespace testSqlite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MemberService _memberService;
 

        public HomeController( MemberService memberService , ILogger<HomeController> logger)
        {
            _memberService = memberService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<Member> members = new List<Member>();
            _logger.LogInformation("index page : show all users.");
            try
            {
                //List<Member> members = new List<Member>();
               members = this._memberService.GetAllMembers();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }
            
            ViewBag.Members = members;
            return View();
        }

       

        [HttpPost]
        public IActionResult AddMember(Member member)
        {
            _logger.LogInformation("AddMember");
            this._memberService.add(member);
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}