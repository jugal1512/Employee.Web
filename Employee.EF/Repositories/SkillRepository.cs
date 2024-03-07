using Employee.Domain.Employee;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.EF.Repositories
{
    public class SkillRepository : ISkillRepository
    {
        private readonly EmployeeDbContext _employeeDbContext;
        public SkillRepository(EmployeeDbContext employeeDbContext)
        {
            _employeeDbContext = employeeDbContext;
        }
        public async Task<List<Skill>> GetSkillById(int? id)
        {
            return await _employeeDbContext.Skills.AsNoTracking().Where(s => s.EmployeeId == id).ToListAsync();
        }

        public async Task<List<Skill>> GetSkills()
        {
            return await _employeeDbContext.Skills.AsNoTracking().ToListAsync();
        }
        public async Task<List<Skill>> DeleteSkill(int? id)
        {
            var skillId = await GetSkillById(id);
            _employeeDbContext.Skills.RemoveRange(skillId);
            await _employeeDbContext.SaveChangesAsync();
            return skillId;
        }

        Task<List<Skill>> ISkillRepository.GetSkillById(int? id)
        {
            throw new NotImplementedException();
        }
    }
}
