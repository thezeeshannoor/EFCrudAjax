using Entity_Framework_with_Ajax.Models;
using Microsoft.AspNetCore.Mvc;

namespace Entity_Framework_with_Ajax.Controllers
{
    public class HomeController : Controller
    {
        private readonly EmployeDbContext emp;
        public HomeController(EmployeDbContext con)
        {
            emp = con;

        }
        public IActionResult Index()
        {

            return View();
        }

        [HttpGet]
        public JsonResult Indexx()
        {
            var e = emp.Employes.ToList();
            return Json(e);
        }

        //public IActionResult AddEmp()
        //{
        //    return View();
        //}

        [HttpPost]
        public JsonResult AddEmp([FromBody] Employe obj)
        {
            emp.Employes.Add(obj);
            emp.SaveChanges();
            return Json(obj);

        }


        [HttpPost]
        public JsonResult Delete(int id)
        {
            var e = emp.Employes.Find(id);
            emp.Employes.Remove(e);
            emp.SaveChanges();
            return Json(e);
        }
        [HttpGet]
        public JsonResult EditView(int id)
        {
            var e = emp.Employes.Find(id);

            return Json(e);
        }

        [HttpPost]
        public JsonResult UpdateEmp([FromBody] Employe e)
        {

            emp.Employes.Update(e);
            emp.SaveChanges();

            return Json(e);
        }
    }
}
