using CV_Project2022.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CV_Project2022.Pages
{
    public class BrowseCvModel : PageModel
    {

        private readonly DBService _service;

        public BrowseCvModel(DBService service)
        {
            _service = service;
        }

        public ViewModel viewModel { get; set; } = new ViewModel();
         public List<CvModel> CV_list { get; private set; }

        public async Task OnGet()
        {
            viewModel.CV_list = await _service.GetAllCVs();
        }

        public async Task<IActionResult> OnPostDelete(int id)
        {

            await _service.DeleteCv(id);

            return RedirectToPage("BrowseCv");
        }
    }
}
