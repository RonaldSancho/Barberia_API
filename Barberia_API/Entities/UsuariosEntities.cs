namespace Barberia_API.Entities
{
    public class UsuariosEntities
    {
        public long IdUsuario { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellidos { get; set; } = string.Empty;
        public string CorreoElectronico { get; set; } = string.Empty;
        public string Contraseña { get; set; } = string.Empty;
        public int Telefono { get; set; } = 0;
        public int TipoUsuario { get; set; } = 0;
    }
}
