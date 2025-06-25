using System.ComponentModel.DataAnnotations;

namespace MantenimientoTrabajadores.Models.ViewModels
{
    public class TrabajadorViewModel
    {
        [Required]
        [Display(Name= "IdTrabajador")]
        public int Id { get; set; }

        [Required]
        public string? Nombres { get; set; }


        public string? TipoDocumento { get; set; }

        public string? NumeroDocumento { get; set; }

        public string? Sexo { get; set; }

        [Required]
        public int? IdDepartamento { get; set; }

        [Required]
        public int? IdProvincia { get; set; }

        [Required]
        public int? IdDistrito { get; set; }

    }
}
