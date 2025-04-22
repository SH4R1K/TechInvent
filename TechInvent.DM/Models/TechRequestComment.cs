using System.ComponentModel.DataAnnotations;

namespace TechInvent.DM.Models
{
    public class TechRequestComment
    {
        public int IdComment { get; set; }
        public int IdUser { get; set; }
        public int IdRequest { get; set; }
        public DateTime CommentDate { get; set; }
        [Required(ErrorMessage = "Текст обязателен")]
        [StringLength(2000, ErrorMessage = "Заголовок не может превышать 100 символов")]
        public string Message { get; set; }
        public virtual User Author { get; set; }
        public virtual TechRequest TechRequest { get; set; }
    }
}
