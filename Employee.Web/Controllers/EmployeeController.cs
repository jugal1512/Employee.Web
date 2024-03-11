using AutoMapper;
using Employee.Domain.Employee;
using Employee.Web.Models;
using Microsoft.AspNetCore.Mvc;
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

        public async Task<IActionResult> Index(string? searchString,string sortOrder,int pg=1)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            if (!string.IsNullOrEmpty(searchString))
            {
                var searchEmployee = await _employeeService.SearchEmployee(searchString);
                var searchMapper = _mapper.Map<List<EmployeeDto>>(searchEmployee);
                return View(searchMapper);
            }
            else
            {
                var Employees = await _employeeService.GetEmployees();
                var EmployeeMapper = _mapper.Map<List<EmployeeDto>>(Employees);
                const int pageSize = 5;
                if (pg < 1)
                {
                    pg = 1;
                }
                int recsCount = EmployeeMapper.Count();
                var pager = new Pager(recsCount, pg, pageSize);
                int recSkip = (pg - 1) * pageSize;
                var data = EmployeeMapper.Skip(recSkip).Take(pager.PageSize).ToList();
                this.ViewBag.Pager = pager;
                switch (sortOrder)
                {
                    case "name_desc":
                        EmployeeMapper = EmployeeMapper.OrderByDescending(x => x.FirstName).ToList();
                        break;
                    default:
                        EmployeeMapper = EmployeeMapper.OrderBy(x => x.FirstName).ToList();
                        break;
                }
                return View(data);
                //return View(EmployeeMapper);
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
                var newFileName = Guid.NewGuid().ToString() + "_" + Image.FileName;
                var path = Path.Combine("wwwroot/uploads/", newFileName);
                using (var filestream = new FileStream(path, FileMode.Create))
                {
                    Image.CopyTo(filestream);
                }
                employeeDto.Image = newFileName;
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
            return PartialView("_InsertModelView");
            //return RedirectToAction("Index");
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
                var newFileName = Guid.NewGuid().ToString() + "_" + Image.FileName;
                var path = Path.Combine("wwwroot/uploads/",newFileName);
                using (var filestream = new FileStream(path, FileMode.Create))
                {
                    Image.CopyTo(filestream);
                }
                employeeDto.Image = newFileName;
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
                var oldImagepath = Path.Combine("wwwroot/uploads", getEmployeeMapper.Image);
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
                employeeDto.Image = newFileName;
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
            _mapper.Map<EmployeeDto>(updateEmployee);
            TempData["success"] = "Employee Update Successfully.";
            return PartialView("_UpdateModelView");
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
                var oldImagepath = Path.Combine("wwwroot/uploads", getEmployeeMapper.Image);
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
                employeeDto.Image = newFileName;
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
