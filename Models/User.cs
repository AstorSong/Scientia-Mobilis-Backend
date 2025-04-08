using System.ComponentModel.DataAnnotations;

public class User {
    [Key]
    public string Uid { get; set; }

    public string Email { get; set; }
    
    public string DisplayName { get; set; }

    public bool EmailVerified { get; set; }

    public string Password { get; set; }
}