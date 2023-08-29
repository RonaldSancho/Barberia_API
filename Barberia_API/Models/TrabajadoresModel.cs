using Barberia_API.Entities;
using Barberia_API.Interfeces;
using Dapper;
using System.Data.SqlClient;

namespace Barberia_API.Models
{
    public class TrabajadoresModel : ITrabajadoresModel
    {

        private readonly IConfiguration _configuration;

        public TrabajadoresModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //metodo que registra a los trabajadores
        public int RegistrarTrabajador(TrabajadoresEntities trabajador)
        {
            using (var conexion = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                return conexion.Execute("RegistrarTrabajador",
                    new
                    {
                        trabajador.BioTrabajador,
                        trabajador.NombreTrabajador,
                        trabajador.ApellidosTrabajador,
                        trabajador.TelefonoTrabajador,
                        trabajador.ImagenTrabajador,
                        trabajador.TipoUsuario
                    }, commandType: System.Data.CommandType.StoredProcedure);
            }
        }
    }
}
