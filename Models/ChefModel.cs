#pragma warning disable CS8618
namespace ChefsNDishes.Models;
using System.ComponentModel.DataAnnotations;

public class Chef
{
    [Key]
    public int ChefId { get; set; }

    [Required(ErrorMessage = "First Name is required")]
    [Display(Name = "First Name: ")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Last Name is required")]
    [Display(Name = "Last Name: ")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "Date of Birth is required")]
    [DataType(DataType.Date)]
    [DOB_Over18]
    [Display(Name = "Date of Birth: ")]
    public DateTime? DOB {get; set;}

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    public List<Dish> Dishes { get; set; } = new List<Dish>();
}

public class DOB_Over18Attribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null)
        {
            return new ValidationResult("Date of Birth is required.");
        }
        DateTime birthdate = (DateTime)value;
        int age = DateTime.Today.Year - birthdate.Year;
        Console.WriteLine("1-"+age);
        Console.WriteLine(birthdate.Date.AddYears(age) > DateTime.Today);
        if (birthdate.Date.AddYears(age) > DateTime.Today){
            age--;
        } 
        Console.WriteLine("2-"+age);
            

        if (age < 18)
        {
            return new ValidationResult("Must be 18 years of age or older");
        }
        else
        {
            return ValidationResult.Success;
        }
    }
}

// public class DOBValidationAttribute : ValidationAttribute
// {
//     protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
//     {
//         if (value == null)
//         {
//             return new ValidationResult("Date of Birth is required.");
//         }

//         if ((DateTime)value > DateTime.Now)
//         {
//             return new ValidationResult("Invalid DOB");
//         }
//         else
//         {
//             return ValidationResult.Success;
//         }
//     }
// }