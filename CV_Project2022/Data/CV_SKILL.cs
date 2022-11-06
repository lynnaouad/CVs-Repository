using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CV_Project2022.Data
{
    public class CV_SKILL
    {
        [Key]
        [Column(Order=1)]
        public int CvId { get; set; }

        [Key]
        [Column(Order = 2)]
        public int SkillId { get; set; }

        public virtual CV CV { get; set; }
        public virtual SKILL SKILL { get; set; }
    }
}
