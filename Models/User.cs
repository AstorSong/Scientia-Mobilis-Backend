using System.ComponentModel.DataAnnotations;

public class User {
    [Key]
    public string Uid { get; set; }

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email address.")]
    [StringLength(100, ErrorMessage = "Email cannot be longer than 100 characters.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Display name is required.")]
    [StringLength(50, ErrorMessage = "Display name cannot be longer than 50 characters.")]
    public string DisplayName { get; set; }

    public bool EmailVerified { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    [MaxLength(20, ErrorMessage = "Password cannot be longer than 20 characters.")]
    public string password { get; set; }
}