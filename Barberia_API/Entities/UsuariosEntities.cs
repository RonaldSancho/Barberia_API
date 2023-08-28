namespace Barberia_API.Entities
{
    public class UsuariosEntities
    {
        //Propiedad de la entidad usuario
        public long IdUsuario { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellidos { get; set; } = string.Empty;
        public string CorreoElectronico { get; set; } = string.Empty;
        public string Contraseña { get; set; } = string.Empty;
        public int Telefono { get; set; } = 0;
        public int TipoUsuario { get; set; } = 0;
        public string Token { get; set; } = string.Empty;
    }
}
