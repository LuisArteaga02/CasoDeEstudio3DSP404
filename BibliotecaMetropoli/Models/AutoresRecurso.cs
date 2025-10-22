using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BibliotecaMetropoli.Models
{
    public class AutoresRecurso
    {
        [ForeignKey("IdRec")]
        public int IdRec { get; set; }

        public virtual Recurso Recurso { get; set; } = null!;

        [ForeignKey("IdAutor")]
        public int idAutor { get; set; }
        public virtual Autor Autor { get; set; } = null!;

        public bool EsPrincipal { get; set; }

    }
}
