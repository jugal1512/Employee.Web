using AutoMapper;
using Employee.Domain.Employee;
using Employee.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Employee.Web.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeService _employeeService;
        private readonly IMapper _mapper;
        public EmployeeController(EmployeeService employeeService,IMapper mapper)
        {
            _employeeService = employeeService;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var Employees = await _employeeService.GetEmployees();
            var EmployeeMapper = _mapper.Map<List<EmployeeDto>>(Employees);
            return View(EmployeeMapper);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeModel employeeModel,IFormFile Image)
        {
            if (Image != null)
            {
                var newFileName = Guid.NewGuid().ToString() + "_" + Image.FileName;
                var path = Path.Combine("wwwroot/uploads/",newFileName);
                using (var filestream = new FileStream(path, FileMode.Create))
                {
                    Image.CopyTo(filestream);
                }
                employeeModel.Image = newFileName;
            }
            if (ModelState.IsValid)
            {
                var AddEmployee = await _employeeService.AddEmployee(employeeModel);
                var AddMapper = _mapper.Map<EmployeeDto>(AddEmployee);
                TempData["success"] = "Employee Create Successfully.";
                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var getEmployee = await _employeeService.GetEmployeeById(id);
            var employeeMapper = _mapper.Map<EmployeeDto>(getEmployee);
            return View(employeeMapper);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int? id,EmployeeModel employeeModel,IFormFile Image)
        {
            var getEmployee = await _employeeService.GetEmployeeById(id);
            var oldImage = getEmployee.Image;
            if (Image != null)
            {
                var oldImagepath = Path.Combine("wwwroot/uploads", getEmployee.Image);
                if (System.IO.File.Exists(oldImagepath))
                {
                    System.IO.File.Delete(oldImagepath);
                }
                var newFileName = Guid.NewGuid().ToString() + "_" + Image.FileName;
                var path = Path.Combine("wwwroot/uploads/", newFileName);
                using (var filestream = new FileStream(path, FileMode.Create))
                {
                    Image.CopyTo(filestream);
                }
                employeeModel.Image = newFileName;
            }
            else
            {
                employeeModel.Image = oldImage;
            }
            var updateEmployee = await _employeeService.UpdateEmployee(id,employeeModel);
            var updateMapper = _mapper.Map<EmployeeDto>(updateEmployee);
            TempData["success"] = "Employee Update Successfully.";
            return RedirectToAction("Index");
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int? id)
        {
            var deleteEmployee = await _employeeService.DeleteEmployee(id);
            var oldImagepath = Path.Combine("wwwroot/uploads",deleteEmployee.Image);
            if (System.IO.File.Exists(oldImagepath))
            {
                System.IO.File.Delete(oldImagepath);
            }
            var deleteMapper = _mapper.Map<EmployeeDto>(deleteEmployee);
            TempData["success"] = "Employee delete Successfully.";
            return RedirectToAction("Index");
        }
    }
}
