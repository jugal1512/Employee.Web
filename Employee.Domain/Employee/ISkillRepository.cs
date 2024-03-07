using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Domain.Employee
{
    public interface ISkillRepository
    {
        public Task<List<Skill>> GetSkills();
        public Task<List<Skill>> GetSkillById(int? id);
        public Task<List<Skill>> DeleteSkill(int? id);
    }
}
