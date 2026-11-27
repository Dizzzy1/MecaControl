namespace MecaControlRD.Blazor.Models
{
    public class RespuestaApi<T>
    {
        public T? Value { get; set; }

        public bool IsSuccess { get; set; }

        public bool IsFailure { get; set; }
    }
}