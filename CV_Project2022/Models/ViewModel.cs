using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace CV_Project2022.Models
{
    public class ViewModel
    {
        public CvModel CV { get; set; }

        public List<SelectListItem> nationality_list { get; set; } = new Nationality().GetAllNationalities();

        public List<string> gender_list { get; set; } = new List<string> { "Male" , "Female" };

        public List<SelectListItem> skills_list { get; set; }

        public List<CvModel> CV_list { get; set; }

    }
}
