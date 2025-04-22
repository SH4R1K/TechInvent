using System.ComponentModel.DataAnnotations;

namespace TechInvent.DM.Models
{
    public class TechRequest
    {
        public int IdRequest { get; set; }
        public int IdRequestType { get; set; }
        public int? IdCabinet { get; set; } = null!;
        [Required(ErrorMessage = "Заголовок обязателен")]
        [StringLength(100, ErrorMessage = "Заголовок не может превышать 100 символов")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Описание обязательно")]
        [StringLength(1000, ErrorMessage = "Описание не может превышать 1000 символов")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Тип заявки обязателен")]
        public RequestType RequestType { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreationDate { get; set; } = DateTime.UtcNow;
        public virtual List<TechRequestWorkplace> AttachedWorkplaces { get; set; } = new List<TechRequestWorkplace>();
        public virtual Cabinet? Cabinet { get; set; }
        public virtual List<TechRequestComment> Comments { get; set; } = new List<TechRequestComment>();
    }
}
