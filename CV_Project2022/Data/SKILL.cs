using System.Collections.Generic;

namespace CV_Project2022.Data
{
    public class SKILL
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //Navigation Propreties   (Many to Many Relationship between CV and SKILL)
        public ICollection<CV_SKILL> CV_SKILL { get; set; }
    }
}
