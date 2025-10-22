using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BibliotecaMetropoli.Models
{
    public class Editorial
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdEdit { get; set; }

        [Required(ErrorMessage = "El nombre de la editorial es obligatorio.")]
        public string Nombre { get; set; } = string.Empty;
        [StringLength(500)]
        public string Description { get; set; }
        public virtual ICollection<Recurso> Recursos { get; set; } = new HashSet<Recurso>();
    }
}
