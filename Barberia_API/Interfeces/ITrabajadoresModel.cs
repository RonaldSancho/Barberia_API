using Barberia_API.Entities;

namespace Barberia_API.Interfeces
{
    public interface ITrabajadoresModel
    {
        public int RegistrarTrabajador(TrabajadoresEntities trabajador);
        public List<TrabajadoresEntities> ConsultaTrabajadores();
        public TrabajadoresEntities? ConsultaTrabajador(int IdTrabajador);
        public void EditarTrabajador(TrabajadoresEntities trabajador);
        public int EliminarTrabajador(int idTrabajador);
    }
}
