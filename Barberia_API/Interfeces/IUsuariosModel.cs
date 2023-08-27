using Barberia_API.Entities;

namespace Barberia_API.Interfeces
{
    public interface IUsuariosModel
    {
        public int RegistrarUsuario(UsuariosEntities usuario);

        public UsuariosEntities? ValidarCorreoElectronico(string CorreoElectronico);
    }
}
