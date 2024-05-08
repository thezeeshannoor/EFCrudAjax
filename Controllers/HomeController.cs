using Entity_Framework_with_Ajax.Models;
using Microsoft.AspNetCore.Mvc;
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
            int totalRecord = 0;
            int filterRecord = 0;

            var draw = Request.Form["draw"].FirstOrDefault();
            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            int pageSize = Convert.ToInt32(Request.Form["length"].FirstOrDefault() ?? "0");
            int skip = Convert.ToInt32(Request.Form["start"].FirstOrDefault() ?? "0");
            var data = emp.Set<Employe>().AsQueryable();

            // Get total count of data in table
            totalRecord = data.Count();

            // Search data when search value found
            if (!string.IsNullOrEmpty(searchValue))
            {
                data = data.Where(x =>
                    x.Name.ToLower().Contains(searchValue.ToLower())
                    || x.Depaertment.ToLower().Contains(searchValue.ToLower())
                    || x.Sallary.ToString().ToLower().Contains(searchValue.ToLower())
                );
            }

            // Get total count of records after search
            filterRecord = data.Count();

            // Sort data
            if (!string.IsNullOrEmpty(sortColumn) && !string.IsNullOrEmpty(sortColumnDirection))
            {
                if (sortColumnDirection == "asc")
                {
                    data = data.OrderBy(x => EF.Property<object>(x, sortColumn));
                }
                else
                {
                    data = data.OrderByDescending(x => EF.Property<object>(x, sortColumn));
                }
            }

            // Pagination
            var empList = data.Skip(skip).Take(pageSize).ToList();

            var returnObj = new { draw = draw, recordsTotal = totalRecord, recordsFiltered = filterRecord, data = empList };
            return Json(returnObj);
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
