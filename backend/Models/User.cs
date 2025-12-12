namespace backend.Models
{
    public class User
    {
        public int Id { get; set; }
        public string MobileNo { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? City { get; set; }
        public string? Pincode { get; set; }
        public string? Password { get; set; }
        public DateTime? CreatedDate { get; set; }
    }

    public class LoginRequest
    {
        public string MobileNo { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class RegisterRequest
    {
        public string MobileNo { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? City { get; set; }
        public string? Pincode { get; set; }
    }

    public class UserResponse
    {
        public int Id { get; set; }
        public string MobileNo { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? City { get; set; }
        public string? Pincode { get; set; }
    }
}
