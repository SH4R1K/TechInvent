using System.ComponentModel.DataAnnotations;

namespace TechInvent.DM.Models
{
    public class TechRequest
    {
        public int IdRequest { get; set; }
        public int IdRequestType { get; set; }
        [Required(ErrorMessage = "Заголовок обязателен")]
        [StringLength(100, ErrorMessage = "Заголовок не может превышать 100 символов")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Описание обязательно")]
        [StringLength(1000, ErrorMessage = "Описание не может превышать 1000 символов")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Тип заявки обязателен")]
        public RequestType RequestType { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.UtcNow;
        public List<TechRequestWorkplace> AttachedWorkplaces { get; set; } = new List<TechRequestWorkplace>();
    }
}
