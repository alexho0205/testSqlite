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

        public HomeController(ILogger<HomeController> logger , MemberService memberService )
        {
            _logger = logger;
            _memberService = memberService;
        }

        public IActionResult Index()
        {
            List<Member> members = this._memberService.GetAllMembers();
            ViewBag.Members = members;
            return View();
        }

       

        [HttpPost]
        public IActionResult AddMember(Member member)
        {
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