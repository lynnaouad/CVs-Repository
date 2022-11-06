using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace CV_Project2022
{
    public class Skills
    {
        private List<SelectListItem> skills_list { get; set; }

        public Skills()
        {
            skills_list = new List<SelectListItem>
                    {
                       new SelectListItem{Value= "Java", Text="Java" , Selected=false},
                       new SelectListItem{Value= "C#", Text="C#" , Selected=false },
                       new SelectListItem{ Value = "Python", Text = "Python" , Selected=false},
                       new SelectListItem{ Value = "php", Text = "php" , Selected=false },
                       new SelectListItem{ Value = "SQL", Text = "SQL", Selected=false },
                    };

        }

        public void AddSkill(string skill)
        {
            skills_list.Add(new SelectListItem { Value = skill, Text = skill });
        }

        public List<SelectListItem> GetAllSkills()
        {
            return skills_list;
        }

        public void RemoveSkill(string skill)
        {
            var skillToDelete = skills_list.Find(x => x.Value == skill);
            skills_list.Remove(skillToDelete);
        }

    }
}
