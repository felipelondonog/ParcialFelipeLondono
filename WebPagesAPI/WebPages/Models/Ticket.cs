﻿using System.ComponentModel.DataAnnotations;

namespace WebPages.Models
{
    public class Ticket
    {
        [Key]
        public Guid Id { get; set; }

        [Display(Name = "Fecha de uso")]
        public DateTime? UseDate { get; set; }

        [Display(Name = "Ocupada?")]
        public bool IsUsed { get; set; }

        [Display(Name = "Portería de entrada")]
        public string? EntranceGate { get; set; }
    }
}
