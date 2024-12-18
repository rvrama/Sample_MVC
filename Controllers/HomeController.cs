using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Sample_MVC.Models;

namespace Sample_MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly NotesContext _notesContext;

        public HomeController(ILogger<HomeController> logger, NotesContext notesContext) 
        {
            _logger = logger;
            _notesContext = notesContext;
        }

        public IActionResult Index()
        {
            _notesContext.Database.EnsureCreated();
            FillDatabaseValues(_notesContext);
            //return ViewComponent("Index", new {istest="true", role="admin" });
            ViewData["role"] = "admin";
            return View(_notesContext.Notes);
        }

        public void FillDatabaseValues(NotesContext nc)
        {
            nc.Add(new Notes()
            { //NotesId = 1,
                NotesTitle = "Wedding Plan",
                NotesDescription = "Wedding plan description",
                NotesCreatedAt = DateTime.Now,
            });

            nc.Add(new Notes()
            {
                //NotesId = 2,
                NotesTitle = "New trip plan",
                NotesDescription = "New Trip description",
                NotesCreatedAt = DateTime.Now.AddDays(-1)
            });
            nc.SaveChanges();
        }
        public IActionResult Notes()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
