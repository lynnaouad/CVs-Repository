using System.Collections.Generic;

namespace CV_Project2022.Data
{
    public class CV
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Date { get; set; }
        public string Nationality { get; set; }
        public string Gender { get; set; }
        public string Photo { get; set; }
        public string Email { get; set; }
        public int Grade { get; set; }

        //Navigation Propreties   (Many to Many Relationship between CV and SKILL)
        public ICollection<CV_SKILL> CV_SKILL { get; set; }
    }
}
