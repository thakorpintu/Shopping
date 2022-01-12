using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

using System.Web;

namespace CrudOperationMVC.Models
{
    public class RegistrationModel
    {
        [Required(ErrorMessage = "Please Enter the Value")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Please Enter the Value")]

        public string name { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9+_.-]+@[a-zA-Z0-9.-]+$", ErrorMessage = "Invalid Email... ")]
        [Required(ErrorMessage = "Please Enter the Value")]

        public string Email { get; set; }
       
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$", ErrorMessage = "Invalid Password Minimum 8 Characters ")]
        [Required(ErrorMessage = "Please Enter the Value")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please Enter the Value")]
        [Compare("Password", ErrorMessage = "Invalid Password Doent Not Matche Password")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Please Enter the Value")]
        public string Gender { get; set; }

        [Range(0, 6, ErrorMessage = "Enter the valid Age... ")]
        [Required(ErrorMessage = "Please Enter the Value")]
        public int Age { get; set; }
        
        public string Date { get; set; }
        [Required(ErrorMessage = "Please Enter the Value")]

        public string Month { get; set; }
        [Required(ErrorMessage = "Please Enter the Value")]

        public string Year { get; set; }
        [Required(ErrorMessage = "Please Enter the Value")]

        public string Listadata { get; set; }


        public string Dob { get; set; }
        [Required(ErrorMessage = "Please Enter the Value")]

        public HttpPostedFileBase img { get; set; }
        [Required(ErrorMessage = "Please Select Images...")]

        public string showimg { get; set; }
        public string Rdate { get; set; }


    }
}