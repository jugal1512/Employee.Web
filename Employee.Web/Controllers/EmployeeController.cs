using AutoMapper;
using Employee.Domain.Employee;
using Employee.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Linq.Expressions;

namespace Employee.Web.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeService _employeeService;
        private readonly SkillService _skillService;
        private readonly IMapper _mapper;
        public EmployeeController(EmployeeService employeeService,SkillService skillService,IMapper mapper)
        {
            _employeeService = employeeService;
            _skillService = skillService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> getAllEmployees(string? searchString, string sortOrder)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                var searchEmployee = await _employeeService.SearchEmployee(searchString);
                var searchMapper = _mapper.Map<List<EmployeeDto>>(searchEmployee);
                return Json(searchMapper);
            }
            else
            {
                var Employees = await _employeeService.GetEmployees(sortOrder);
                var EmployeeMapper = _mapper.Map<List<EmployeeDto>>(Employees);
                return Json(EmployeeMapper);
            }
        }

        public IActionResult Insert()
        {
            return PartialView("_InsertModelView");
        }

        [HttpPost]
        public async Task<IActionResult> Insert(EmployeeDto employeeDto,IFormFile Image)
        {
            if (Image != null)
            {
                employeeDto.Image = await UploadImage(Image);
            }
            List<Skill> SkillItems = new List<Skill>();
            if (!string.IsNullOrEmpty(employeeDto.SkillName))
            {
                var skillArray = employeeDto.SkillName.Split(",");
                if (skillArray.Length > 0)
                {
                    foreach (var item in skillArray)
                    {
                        Skill skill = new Skill();
                        skill.SkillName = item;
                        SkillItems.Add(skill);
                    }
                }
            }
            var employeeModel = _mapper.Map<EmployeeModel>(employeeDto);
            employeeModel.Skills = SkillItems;
            var AddEmployee = await _employeeService.AddEmployee(employeeModel);
            var AddEmployeeMapper = _mapper.Map<EmployeeDto>(AddEmployee);
            TempData["success"] = "Employee Create Successfully.";
            //return PartialView("_InsertModelView");
            return Json(AddEmployeeMapper);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeDto employeeDto,IFormFile Image)
        {
            if (Image != null)
            {
                employeeDto.Image = await UploadImage(Image);
            }
            List<Skill> SkillItems = new List<Skill>();
            if (!string.IsNullOrEmpty(employeeDto.SkillName))
            {
                var skillArray = employeeDto.SkillName.Split(",");
                if (skillArray.Length > 0)
                {
                    foreach (var item in skillArray)
                    {
                        Skill skill = new Skill();
                        skill.SkillName = item;
                        SkillItems.Add(skill);
                    }
                }
            }
            var employeeModel = _mapper.Map<EmployeeModel>(employeeDto);
            employeeModel.Skills = SkillItems;
            var AddEmployee = await _employeeService.AddEmployee(employeeModel);
            _mapper.Map<EmployeeDto>(AddEmployee);
            TempData["success"] = "Employee Create Successfully.";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var getEmployee = await _employeeService.GetEmployeeById(id);
            var employeeMapper = _mapper.Map<EmployeeDto>(getEmployee);
            return PartialView("_UpdateModelView", employeeMapper);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int? id, EmployeeDto employeeDto, IFormFile Image)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var getEmployee = await _employeeService.GetEmployeeById(id);
            var getEmployeeMapper = _mapper.Map<EmployeeDto>(getEmployee);
            var oldImage = getEmployeeMapper.Image;
            if (Image != null)
            {
                await DeleteImagePath(getEmployeeMapper);
                employeeDto.Image = await UploadImage(Image);
            }
            else
            {
                employeeDto.Image = oldImage;
            }
            if (!string.IsNullOrEmpty(getEmployeeMapper.SkillName))
            {
                await _skillService.DeleteSkill(id);
            }
            List<Skill> SkillItems = new List<Skill>();
            if (!string.IsNullOrEmpty(employeeDto.SkillName))
            {
                var skillArray = employeeDto.SkillName.Split(",");
                if (skillArray.Length > 0)
                {
                    foreach (var item in skillArray)
                    {
                        Skill skill = new Skill();
                        skill.SkillName = item;
                        SkillItems.Add(skill);
                    }
                }
            }
            var employeeModel = _mapper.Map<EmployeeModel>(employeeDto);
            employeeModel.Skills = SkillItems;
            var updateEmployee = await _employeeService.UpdateEmployee(id, employeeModel);
            var updateEmployeeMapper = _mapper.Map<EmployeeDto>(updateEmployee);
            TempData["success"] = "Employee Update Successfully.";
            //return PartialView("_UpdateModelView");
            return Json(updateEmployeeMapper);
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
        public async Task<IActionResult> Update(int? id,EmployeeDto employeeDto,IFormFile Image)
        {
            var getEmployee = await _employeeService.GetEmployeeById(id);
            var getEmployeeMapper = _mapper.Map<EmployeeDto>(getEmployee); 
            var oldImage = getEmployeeMapper.Image;
            if (Image != null)
            {
                await DeleteImagePath(getEmployeeMapper);
                employeeDto.Image = await UploadImage(Image);
            }
            else
            {
                employeeDto.Image = oldImage;
            }
            if (!string.IsNullOrEmpty(getEmployeeMapper.SkillName))
            {
                await _skillService.DeleteSkill(id);
            }
            List<Skill> SkillItems = new List<Skill>();
            if (!string.IsNullOrEmpty(employeeDto.SkillName))
            {
                var skillArray = employeeDto.SkillName.Split(",");
                if (skillArray.Length > 0)
                {
                    foreach (var item in skillArray)
                    {
                        Skill skill = new Skill();
                        skill.SkillName = item;
                        SkillItems.Add(skill);
                    }
                }
            }
            var employeeModel = _mapper.Map<EmployeeModel>(employeeDto);
            employeeModel.Skills = SkillItems;
            var updateEmployee = await _employeeService.UpdateEmployee(id,employeeModel);
            _mapper.Map<EmployeeDto>(updateEmployee);
            TempData["success"] = "Employee Update Successfully.";
            return RedirectToAction("Index");
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int? id)
        {
            var deleteEmployee = await _employeeService.DeleteEmployee(id);
            var deleteMapper = _mapper.Map<EmployeeDto>(deleteEmployee);
            await DeleteImagePath(deleteMapper);
            TempData["success"] = "Employee delete Successfully.";
            return Json(deleteMapper);
        }

        private async Task<string> UploadImage(IFormFile Image)
        {
            var newFileName = Guid.NewGuid().ToString() + "_" + Image.FileName;
            var path = Path.Combine("wwwroot/uploads/", newFileName);
            using (var filestream = new FileStream(path, FileMode.Create))
            {
                Image.CopyTo(filestream);
            }
            return newFileName;
        }

        private async Task DeleteImagePath(EmployeeDto deleteEmployee)
        {
            var oldImagepath = Path.Combine("wwwroot/uploads", deleteEmployee.Image);
            if (System.IO.File.Exists(oldImagepath))
            {
                System.IO.File.Delete(oldImagepath);
            }
        }
    }
}
