using System.ComponentModel.DataAnnotations;

namespace MySite.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Введите Email")]
        [EmailAddress(ErrorMessage = "Некорректный Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Введите имя")]
        public string Name { get; set; }

        public string PasswordHash { get; set; }
        public string Role { get; set; } = "User";
        public DateTime RegistrationDate { get; set; }
    }
}