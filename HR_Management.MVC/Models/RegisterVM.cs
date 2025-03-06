using System.ComponentModel.DataAnnotations;

namespace HR_Management.MVC.Models
{
    public class RegisterVM : LoginVM
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        [Required]
        public string UserName { get; set; }


    }
}
