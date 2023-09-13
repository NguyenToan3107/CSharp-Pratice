using BTH1.Models;
using BTH1.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BTH1.Controllers
{
    public class StudentController : Controller
    {
        readonly BufferedFileUploadService _bufferedFileUploadService;
        private List<Student> listStudents = new List<Student>();
        public StudentController(BufferedFileUploadService bufferedFileUploadService)
        {
            _bufferedFileUploadService = bufferedFileUploadService;

            listStudents = new List<Student>()
            {
                new Student() { Id = 101, Name = "Toan", Branch = Branch.IT,
                                Gender = Gender.Male, IsRegular = true,
                                Address = "A1-2018", Email = "toan@g.com"},
                new Student()
                {
                    Id = 102,
                    Name = "Minh Tu",
                    Branch = Branch.BE,
                    Gender = Gender.Female,
                    IsRegular = true,
                    Address = "A1-2019",
                    Email = "tu@g.com"
                },
                new Student()
                {
                    Id = 102,
                    Name = "Hoang Phong",
                    Branch = Branch.CE,
                    Gender = Gender.Male,
                    IsRegular = true,
                    Address = "A1-2020",
                    Email = "phong@g.com"
                },
                new Student()
                {
                    Id = 102,
                    Name = "Xuan Mai",
                    Branch = Branch.EE,
                    Gender = Gender.Female,
                    IsRegular = false,
                    Address = "A1-2021",
                    Email = "mai@g.com"
                },
            };
        }

        [Route("Admin/Student/List")]
        public IActionResult Index()
        {
            return View(listStudents);
        }

        [HttpGet]
        [Route("Admin/Student/Add")]
        public IActionResult Create()
        {
            ViewBag.AllGenders = Enum.GetValues(typeof(Gender)).Cast<Gender>().ToList();
            ViewBag.AllBranches = new List<SelectListItem>()
            {
            new SelectListItem { Text = "IT", Value = "1" },
            new SelectListItem { Text = "BE", Value = "2" },
            new SelectListItem { Text = "CE", Value = "3" },
            new SelectListItem { Text = "EE", Value = "4" }
            };
            return View();
        }
        [Route("Admin/Student/Add")]
        [HttpPost]
        public async Task<ActionResult> Create(Student obj, IFormFile file)
        {
            obj.Id = listStudents.Last<Student>().Id + 1;
            listStudents.Add(obj);

            try
            {
                if (await _bufferedFileUploadService.UploadFile(file))
                {
                    ViewBag.Message = "File Upload Successful";
                }
                else
                {
                    ViewBag.Message = "File Upload Failed";
                }
            }
            catch (Exception ex)
            {
                //Log ex
                ViewBag.Message = "File Upload Failed";
            }

            return View("Index", listStudents);
        }

    }
}

