using Barberia_API.Entities;

namespace Barberia_API.Interfeces
{
    public interface IServiciosModel
    {
        public int RegistrarServicio(ServiciosEntities servicio);
        public List<ServiciosEntities> ConsultaServicios();
        public ServiciosEntities? ConsultaServicio(int IdServicio);
    }
}
