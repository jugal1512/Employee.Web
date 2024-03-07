using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Domain.Employee
{
    public class SkillService : ISkillService
    {
        private readonly ISkillRepository _skillRepository;
        public SkillService(ISkillRepository skillRepository)
        {
            _skillRepository = skillRepository;
        }
        public async Task<List<Skill>> DeleteSkill(int? id)
        {
            return await _skillRepository.DeleteSkill(id);
        }

        public async Task<List<Skill>> GetSkillById(int? id)
        {
            return await _skillRepository.GetSkillById(id);
        }

        public async Task<List<Skill>> GetSkills()
        {
            return await _skillRepository.GetSkills();
        }
    }
}
