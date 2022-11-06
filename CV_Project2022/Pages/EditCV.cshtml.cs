using CV_Project2022.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CV_Project2022.Pages
{
    public class EditCVModel : PageModel
    {
        private readonly DBService _service;
        private readonly OtherService _otherService;

        public EditCVModel(DBService service, OtherService otherService)
        {
            _service = service;
            _otherService = otherService;
        }


        [BindProperty]
        public CvModel Input { get; set; } = new CvModel();

        public ViewModel viewModel { get; set; } = new ViewModel();

        public async Task OnGetAsync(int id)
        {
            // Get cv having this id from database
            Input = await _service.GetCvById(id);

            List<SelectListItem> selected_skills = Input.Skills;

            // Get the all skills from database
            List<SkillModel> skillModel_list = await _service.GetAllSkills();

            // fill the viewModel skills list
            viewModel.skills_list = _otherService.GetSkills(skillModel_list);

            // fill the input with all the skills from database
            Input.Skills = viewModel.skills_list;

            if (selected_skills.Count == 0)
            {
                for (int i = 0; i < viewModel.skills_list.Count; i++)
                {
                    Input.Skills[i].Selected = false;
                }
            }
            else
            {
                // get the skills selected by the user to be checked
                for (int i = 0; i < viewModel.skills_list.Count; i++)
                {
                    for (int j = 0; j < selected_skills.Count; j++)
                    {
                        if (Input.Skills[i].Text.Equals(selected_skills[j].Text))
                        {
                            Input.Skills[i].Selected = true;
                            break;
                        }
                    }
                }

            }
     
        }

        public async Task<IActionResult> OnPost(int id)
        {
            if (!ModelState.IsValid)
            {
                // Get the skills from database
                List<SkillModel> skillModel_list = await _service.GetAllSkills();

                // fill the viewModel skills list
                viewModel.skills_list = _otherService.GetSkills(skillModel_list);

                return Page();
            }

            Input.Id = id;

            // Update Database
            await _service.UpdateCv(Input);

            return RedirectToPage("ViewCv",new {id=id});
        }

        public IActionResult OnPostReturn(int id)
        {
            return RedirectToPage("BrowseCv");
        }
    }
}
