using System.ComponentModel.DataAnnotations;

namespace ConcertAPI.DAL
{
    public class Ticket
    {
        [Key]
        public Guid Id { get; set; }

        [Display(Name = "Fecha de uso")]
        public DateTime? UseDate { get; set; }

        [Display(Name = "¿Está ocupada?")]
        public bool IsUsed { get; set; }

        [Display(Name = "Puerta de entrada")]
        public string? EntranceGate { get; set; }
    }
}
