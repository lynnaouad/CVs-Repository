using System.ComponentModel.DataAnnotations;

namespace CV_Project2022.Models
{
    public class SkillModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Skill Name")]
        public string Name { get; set; }
    }
}
