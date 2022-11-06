using CV_Project2022.FileUploadService;
using CV_Project2022.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace CV_Project2022.Pages
{
    public class ViewCVModel : PageModel
    {
        private readonly DBService _service;
        private readonly OtherService _otherService;
        private readonly IFileUploadService _fileUploadService;

        public ViewCVModel(DBService service, OtherService otherService, IFileUploadService fileUploadService)
        {
            _service = service;
            _otherService = otherService;
            _fileUploadService = fileUploadService;
        }

        public ViewModel viewModel { get; set; } = new ViewModel();
        public int id;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            viewModel.CV = await _service.GetCvById(id);

            return Page();
        }

       

        
    }
}
