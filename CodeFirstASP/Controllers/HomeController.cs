using CodeFirstASP.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CodeFirstASP.Controllers
{
    public class HomeController : Controller
    {
        private readonly studentDBContext studentDB;

        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}
        public HomeController(studentDBContext studentDB)
        {
            this.studentDB = studentDB;
        }

        public IActionResult Index()
        {
            var stdData=studentDB.students.ToList();
            return View(stdData);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(student std)
        {
            if (ModelState.IsValid)
            {
                studentDB.students.Add(std);
                studentDB.SaveChanges();
                return RedirectToAction("Index","Home");
            }
            return View(std);//if it occured any error also then it will run application by printig that 
        }
        public IActionResult Details(int id)
        {
            if (id == null || studentDB.students ==null)
            {
                return NotFound();
            }
            else
            {
               var stdData = studentDB.students.FirstOrDefault(x => x.id == id);
                if (stdData == null)
                {
                    return NotFound();
                }
                return View(stdData);
            }
        }

        public IActionResult Edit(int id)
        {
            if (id == null || studentDB.students == null)
            {
                return NotFound();
            }
            else
            {
                var stdData = studentDB.students.Find(id);
                if (stdData == null)
                {
                    return NotFound();
                }
                return View(stdData);
            }
        }

        [HttpPost]
        public IActionResult Edit(int? id, student std)
        {
            if (id != std.id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                studentDB.students.Update(std);
                studentDB.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View(std);
        }
        public IActionResult Delete(int? id)
        {
            var stdData = studentDB.students.FirstOrDefault(x => x.id == id);
            return View(stdData);

        }
        [HttpPost,ActionName("Delete")]
        public IActionResult DeleteConfirmed(int? id)
        {
            var stdData = studentDB.students.Find(id);
            if(stdData!=null)
            {
                studentDB.students.Remove(stdData);
            }
            studentDB.SaveChanges();
            return RedirectToAction("Index", "Home");

        }
        public IActionResult Privacy()
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
