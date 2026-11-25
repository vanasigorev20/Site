using System.ComponentModel.DataAnnotations;

namespace MySite.Models
{
    public class Item
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Введите заголовок")]
        public string Title { get; set; }

        public string Description { get; set; }

        public int UserId { get; set; }
    }
}