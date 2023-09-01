namespace Barberia_API.Entities
{
    public class ServiciosEntities
    {
        public long IdServicio { get; set; }
        public string ServicioNombre { get; set; } = string.Empty;
        public string ServicioDetalle { get; set; } = string.Empty;
        public string NombreTrabajador { get; set; } = string.Empty;
        public bool ServicioEstado { get; set; }
        public string ServicioImagen { get; set; } = string.Empty;
        public long IdTrabajador { get; set; }
    }

    public class RespuestaServicio
    {
        //usado para utilizar DropDowns
        public int Codigo { get; set; }
        public string Mensaje { get; set; } = string.Empty;
        public List<ServiciosEntities> RespuestaServicios { get; set; } = new List<ServiciosEntities>();
    }
}
