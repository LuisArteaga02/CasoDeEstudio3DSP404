using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BibliotecaMetropoli.Models
{
    public class Pais
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPais { get; set; }

        [Required(ErrorMessage = "El nombre del país es obligatorio.")]
        public string Nombre { get; set; } = string.Empty;

        public virtual ICollection<Recurso> Recursos { get; set; } = new HashSet<Recurso>();
    }
}
