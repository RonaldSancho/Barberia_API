using Barberia_API.Entities;
using Barberia_API.Interfeces;
using Dapper;
using System.Data.SqlClient;

namespace Barberia_API.Models
{
    public class UsuariosModel : IUsuariosModel
    {
        private readonly IConfiguration _configuration;

        public UsuariosModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public int RegistrarUsuario(UsuariosEntities usuario)
        {
            using (var conexion = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                return conexion.Execute("RegistrarUsuario",
                    new
                    {
                        usuario.Nombre,
                        usuario.Apellidos,
                        usuario.CorreoElectronico,
                        usuario.Contraseña,
                        usuario.Telefono
                    }, commandType: System.Data.CommandType.StoredProcedure);
            }
        }
    }
}
