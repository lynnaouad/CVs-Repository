using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace CV_Project2022
{
    public class Nationality
    {
        private List<SelectListItem> nationality_list { get; set; }

        public Nationality()
        {
            nationality_list = new List<SelectListItem>
                    {
                       new SelectListItem{Value= "Lebanon", Text="Lebanon"},
                       new SelectListItem{Value= "France", Text="France" },
                       new SelectListItem{ Value = "Italy", Text = "Italy" },
                       new SelectListItem{ Value = "Germany", Text = "Germany" },
                       new SelectListItem{ Value = "Spain", Text = "Spain" },
                    };
        }

        public void AddNationality(string nationality)
        {
            nationality_list.Add(new SelectListItem { Value = nationality, Text = nationality });
        }

        public List<SelectListItem> GetAllNationalities()
        {
            return nationality_list;
        }

        public void RemoveNationality(string nationality)
        {
            var nationalityToDelete = nationality_list.Find(x => x.Value == nationality);
            nationality_list.Remove(nationalityToDelete);
        }
    }
}
