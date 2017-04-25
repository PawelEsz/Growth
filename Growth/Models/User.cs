using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Growth.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        [Required(ErrorMessage = "Prosze podac email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Prosze podac haslo")]
        public string Password { get; set; }
        public string Description { get; set; }
        public DateTime? BirthDate { get; set; }
        public bool Confirmed { get; set; }
        public string Photo { get; set; }
        public string Code { get; set; } // dodanie kodu
    }
}