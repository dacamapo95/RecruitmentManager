using RecruitmentManager.Shared;
using System.ComponentModel.DataAnnotations;

namespace RecruitmentManager.Web.ViewModels;

public class EditCandidateViewModel
{
    public Guid Id { get; set; }

    [Display(Name = "First Name")]
    [Required(ErrorMessage = "First name is required")]
    [StringLength(50, ErrorMessage = "First name cannot exceed 100 characters")]
    public string FirstName { get; set; } = string.Empty;

    [Display(Name = "Last Name")]
    [Required(ErrorMessage = "Last name is required")]
    [StringLength(maximumLength: 150, ErrorMessage = "Last name cannot exceed 100 characters")]
    public string SurName { get; set; } = string.Empty;

    [Display(Name = "Date of Birth")]
    [Required(ErrorMessage = "Date of birth is required")]
    [DataType(DataType.Date)]
    public DateTime DateOfBirth { get; set; } = DateTime.Now.AddYears(-20);

    [Required(ErrorMessage = "Email is required")]
    [StringLength(250, ErrorMessage = "Email MaximumLength 250 characters")]
    [EmailAddress(ErrorMessage = "A valid email address is required")]
    public string Email { get; set; } = string.Empty;

    [Display(Name = "Phone Number")]
    [StringLength(15, ErrorMessage = "Phone number cannot exceed 15 characters")]
    public string? PhoneNumber { get; set; }

    [Required(ErrorMessage = "Country is required")]
    [Display(Name = "Country")]
    public Guid CountryId { get; set; }

    [Required(ErrorMessage = "City is required")]
    [Display(Name = "City")]
    public Guid CityId { get; set; }

    [Display(Name = "ZIP Code")]
    [StringLength(20, ErrorMessage = "Zip code cannot exceed 20 characters")]
    public string? ZipCode { get; set; }

    [Display(Name = "Street Address")]
    [StringLength(200, ErrorMessage = "Street cannot exceed 200 characters")]
    public string? Street { get; set; }

    [Required(ErrorMessage = "State is required")]
    [Display(Name = "State")]
    public int StateId { get; set; }

    public List<ExperienceViewModel> Experiences { get; set; } = new();

    public IEnumerable<MasterEntityResponse<int>>? States { get; set; }

    public IEnumerable<MasterEntityResponse<Guid>>? Countries { get; set; }

    public IEnumerable<MasterEntityResponse<Guid>>? Cities { get; set; }
}

public class ExperienceViewModel
{
    public Guid? Id { get; set; }

    [Required(ErrorMessage = "Company name is required")]
    [StringLength(100, ErrorMessage = "Company name cannot exceed 100 characters")]
    public string Company { get; set; } = string.Empty;

    [Required(ErrorMessage = "Job title is required")]
    [StringLength(100, ErrorMessage = "Job title cannot exceed 100 characters")]
    public string Job { get; set; } = string.Empty;

    [StringLength(4000, ErrorMessage = "Description cannot exceed 4000 characters")]
    public string Description { get; set; } = string.Empty;

    [Display(Name = "Start Date")]
    [Required(ErrorMessage = "Start date is required")]
    [DataType(DataType.Date)]
    public DateTime StartDate { get; set; }

    [Display(Name = "End Date")]
    [DataType(DataType.Date)]
    public DateTime? EndDate { get; set; }

    [Required(ErrorMessage = "Salary must be greater than 0")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Salary must be greater than 0")]
    [DataType(DataType.Currency)]
    public decimal Salary { get; set; }

    [Required(ErrorMessage = "Currency is required")]
    [StringLength(3, MinimumLength = 3, ErrorMessage = "Currency must be exactly 3 characters")]
    public string Currency { get; set; } = string.Empty;
}