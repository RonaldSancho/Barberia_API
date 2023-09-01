using Barberia_API.Entities;
using Barberia_API.Interfeces;
using Dapper;
using System.Data.SqlClient;

namespace Barberia_API.Models
{
    public class ServiciosModel : IServiciosModel
    {

        private readonly IConfiguration _configuration;

        public ServiciosModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //metodo que registra a los servicios
        public int RegistrarServicio(ServiciosEntities servicio)
        {
            using (var conexion = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                return conexion.Execute("RegistrarServicio",
                    new
                    {
                        servicio.ServicioNombre,
                        servicio.ServicioDetalle,
                        servicio.ServicioImagen,
                        servicio.IdTrabajador
                    }, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        //metodo que consulta la lista de servicios
        public List<ServiciosEntities> ConsultaServicios()
        {
            using (var conexion = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                return conexion.Query<ServiciosEntities>("ConsultaServicios",
                    new { },
                    commandType: System.Data.CommandType.StoredProcedure).ToList();
            }
        }

        //metodo que consulta un servicio en específico
        public ServiciosEntities? ConsultaServicio(int IdServicio)
        {
            using (var conexion = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                return conexion.Query<ServiciosEntities>("ConsultaServicio",
                    new { IdServicio },
                    commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();
            }
        }
    }
}
