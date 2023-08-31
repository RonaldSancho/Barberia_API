namespace Barberia_API.Entities
{
    public class TrabajadoresEntities
    {
        //Propiedad de la entidad trabajador

        public long IdTrabajador { get; set; }
        public string BioTrabajador { get; set; } = string.Empty;
        public string NombreTrabajador { get; set; } = string.Empty;
        public string ApellidosTrabajador { get; set; } = string.Empty;
        public int TelefonoTrabajador { get; set; }
        public bool Estado { get; set; }
        public string ImagenTrabajador { get; set; } = string.Empty;
        public int TipoUsuario { get; set; }
    }

    public class RespuestaTrabajador
    {
        //usado para utilizar DropDowns
        public int Codigo { get; set; }
        public string Mensaje { get; set; } = string.Empty;
        public List<TrabajadoresEntities> RespuestaTrabajadores { get; set; } = new List<TrabajadoresEntities>();
    }
}
