using Entity_Framework_with_Ajax.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic;

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

        [HttpPost]
        public JsonResult Indexx()
        {
            var start = Convert.ToInt32(Request.Form["start"]);
            var pageSize = Convert.ToInt32(Request.Form["length"]);
            var pageNumber = start / pageSize + 1; ;
            string searchKeyword = Request.Form["search[value]"];

            var employees = emp.Database.SqlQueryRaw<Employe>("EXEC GetEmployee @pageNumber, @pageSize,@searchKeyword",
                new SqlParameter("PageNumber", pageNumber),
                new SqlParameter("PageSize", pageSize),
                 new SqlParameter("SearchKeyword", searchKeyword)
            ).ToList();

            var totalRecords = emp.Employes.Count();

            return Json(new
            {  
                draw = Convert.ToInt32(Request.Form["draw"]),
                recordsTotal = totalRecords,
                recordsFiltered = totalRecords,
                data = employees
            });
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
