using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BibliotecaMetropoli.Models
{
    public class Recurso
    {
        [Key]
        public int IdRec { get; set; }

        [Required]
        public int IdPais { get; set; }

        [Required]
        public int IdTipoR { get; set; }

        [Required]
        public int IdEdit { get; set; }

        [Required]
        [StringLength(255)]
        public string Titulo { get; set; }

        public int? annopublic { get; set; }  // Cambiado a nullable

        [StringLength(50)]
        public string Edicion { get; set; }  // Sin required

        [StringLength(500)]
        public string PalabrasBusqueda { get; set; }  // Solo una propiedad

        [StringLength(1000)]
        public string Descripcion { get; set; }  // Sin required

        // Propiedades de navegación
        [ForeignKey("IdPais")]
        public virtual Pais Pais { get; set; }

        [ForeignKey("IdTipoR")]
        public virtual TipoRecurso TipoRecurso { get; set; }

        [ForeignKey("IdEdit")]
        public virtual Editorial Editorial { get; set; }

        public virtual ICollection<AutoresRecurso> AutoresRecursos { get; set; }
    }
}
