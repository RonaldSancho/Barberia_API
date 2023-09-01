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

        //metodo que consulta la lista de trabajadores
        public List<TrabajadoresEntities> ConsultaTrabajadores()
        {
            using (var conexion = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                return conexion.Query<TrabajadoresEntities>("ConsultaTrabajadores",
                    new {},
                    commandType: System.Data.CommandType.StoredProcedure).ToList();
            }
        }

        //metodo que consulta a un trabajador en específico
        public TrabajadoresEntities? ConsultaTrabajador(int IdTrabajador)
        {
            using (var conexion = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                return conexion.Query<TrabajadoresEntities>("ConsultaTrabajador",
                    new { IdTrabajador },
                    commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();
            }
        }

        //metodo que edita a un trabajador en específico
        public void EditarTrabajador(TrabajadoresEntities trabajador)
        {
            using (var conexion = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                conexion.Execute("EditarTrabajador",
                    new
                    {
                        trabajador.IdTrabajador,
                        trabajador.BioTrabajador,
                        trabajador.NombreTrabajador,
                        trabajador.ApellidosTrabajador,
                        trabajador.TelefonoTrabajador,
                        trabajador.EstadoTrabajador,
                        trabajador.ImagenTrabajador,
                        trabajador.TipoUsuario
                    }, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        //metodo que elimina a un trabajador en específico
        public int EliminarTrabajador(int idTrabajador)
        {
            using (var conexion = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                return conexion.Execute("EliminarTrabajador",
                    new { idTrabajador }, 
                    commandType: System.Data.CommandType.StoredProcedure);
            }
        }
    }
}
