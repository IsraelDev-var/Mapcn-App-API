using NetTopologySuite.Geometries;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace TransformadorWebAPI.Models
{
    [Table("transformadores")]
    public class Transformador
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Required]
        [Column("codigo")]
        [MaxLength(50)]
        public string Codigo { get; set; } = null!;

        [Required]
        [Column("estado")]
        [MaxLength(20)]
        public string Estado { get; set; } = null!;
        // DISPONIBLE | CONDICIONADA | CRITICA

        [Required]
        [Column("ubicacion", TypeName = "geography (point,4326)")]
        public required Point Ubicacion { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
