using Barberia_API.Entities;

namespace Barberia_API.Interfeces
{
    public interface IUsuariosModel
    {
        //declaracion de metodos que son implementados en UsuariosModel
        public int RegistrarUsuario(UsuariosEntities usuario);
        public UsuariosEntities? ValidarCorreoElectronico(string CorreoElectronico);
        public UsuariosEntities? ValidarUsuario(UsuariosEntities usuario);
        public string GenerarToken(string CorreoElectronico);
        public UsuariosEntities? RecuperarContrasenna(UsuariosEntities usuario);
    }
}
