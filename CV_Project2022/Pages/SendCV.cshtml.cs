using CV_Project2022.Data;
using CV_Project2022.FileUploadService;
using CV_Project2022.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CV_Project2022.Pages
{
    public class SendCVModel : PageModel
    {
 
        private readonly DBService _service;
        private readonly OtherService _otherService;

        public SendCVModel(DBService service , OtherService otherService)
        {  
           _service = service;
           _otherService = otherService;
        }


        [BindProperty]
        public CvModel Input { get; set; } = new CvModel();

        public ViewModel viewModel { get; set; } = new ViewModel();

        public async void OnGet()
        {
            // Get the skills from database
            List<SkillModel> skillModel_list = await _service.GetAllSkills();

            // fill the viewModel skills list
            viewModel.skills_list = _otherService.GetSkills(skillModel_list);

            // Checkboxes first --> not checked 
            Input.Skills = viewModel.skills_list;

            // Calculate random numbers for verification
            Random rnd = new Random();
            int nb1 = rnd.Next(1, 21);   // 1 <= nb1 < 21
            int nb2 = rnd.Next(20, 51);  // 20 <= nb2 < 51

            Input.FirstNb = nb1;
            Input.SecondNb = nb2;
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                // Get the skills from database
                List<SkillModel> skillModel_list = await _service.GetAllSkills();

                // fill the viewModel skills list
                viewModel.skills_list = _otherService.GetSkills(skillModel_list);

                return Page();
            }

            int TrueResult = Input.FirstNb + Input.SecondNb;

            if (TrueResult != Input.Summation)
            {
                ModelState.AddModelError(String.Empty , "Incorrect answer! Please Try again...");

                // Get the skills from database
                List<SkillModel> skillModel_list = await _service.GetAllSkills();

                // fill the viewModel skills list
                viewModel.skills_list = _otherService.GetSkills(skillModel_list);

                return Page();
            }

            // Insert into Database
            await _service.AddNewCv(Input);

            return RedirectToPage("BrowseCv");
 
        }


    }
}
