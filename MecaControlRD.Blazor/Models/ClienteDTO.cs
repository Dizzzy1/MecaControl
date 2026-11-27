namespace MecaControlRD.Blazor.Models
{
    public class ClienteDTO
    {
        public int ClienteId { get; set; }

        public string? Nombre { get; set; }

        public string? Telefono { get; set; }

        public string? Direccion { get; set; }

        public DateOnly? Fecharegistro { get; set; }

        public string? Apellido { get; set; }
    }

}
