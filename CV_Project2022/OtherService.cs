using CV_Project2022.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace CV_Project2022
{
    public class OtherService
    {

        public string TransformToString(List<SelectListItem> list)
        {
            string skills = null;

            // Get the checked skills
            var SkillsSelected = list.Where(x => x.Selected).Select(y => y.Value);

            // transform List<string> to String
            if (SkillsSelected.Any())
                skills = string.Join(",", SkillsSelected);

            return skills;
        }

        public List<SelectListItem> TransformToList(string str)
        {
            List<SelectListItem> List_of_skills = new List<SelectListItem>();

            List<string> skills = str.Split(",").ToList<string>();

            foreach (var s in skills)
            {
                List_of_skills.Add(new SelectListItem { Value = s, Text = s });
            }

            return List_of_skills;

        }

        public int CalculateGrade(CvModel cv)
        {
            int grade = 0;

            // Get the checked skills
            var SkillsSelected = cv.Skills.Where(x => x.Selected).Select(y => y.Value);

            if (!SkillsSelected.Any())
                return grade;

            foreach (var skill in SkillsSelected)
            {
                if (cv.Gender.Equals("Male"))
                    grade += 5;
                else
                    grade += 10;
            }

            return grade;
        }
    
        public List<SelectListItem> GetSkills(List<SkillModel> skillModel)
        {
            List<SelectListItem> skill_list = new List<SelectListItem>();

            foreach(var s in skillModel)
            {
                skill_list.Add(new SelectListItem { Value = s.Id.ToString() , Text = s.Name , Selected = false });
            }

            return skill_list;
        }

        public string TransformSkillsToString(List<SelectListItem> list)
        {
            string skills;

            if (list.Any())
            {
                skills = "** No Skill Selected **";
                return skills;
            }

            // transform List<string> to String
            skills = string.Join(",", list);

            return skills;
        }
    }
}
