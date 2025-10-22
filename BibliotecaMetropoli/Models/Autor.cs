using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BibliotecaMetropoli.Models
{
    public class Autor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idAutor { get; set; }

        [Required(ErrorMessage = "Los nombres del autor son obligatorios. Por favor ingresa los nombres completos")]
        public string Nombres { get; set; } = string.Empty;

        [Required(ErrorMessage = "Los apellidos del autor son obligatorios. Por favor ingresa los apellidos completos")]
        public string Apellidos { get; set; } = string.Empty;

        public virtual ICollection<AutoresRecurso> AutoresRecursos { get; set; } = new HashSet<AutoresRecurso>();
        public virtual ICollection<Recurso> Recursos { get; set; }
    }
}
