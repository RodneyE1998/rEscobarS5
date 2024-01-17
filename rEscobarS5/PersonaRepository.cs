using rEscobarS5.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rEscobarS5
{
    public class PersonaRepository
    {
        string dbPath;
        private SQLiteConnection conn;
        public string StatusMessage { get; set; }

        public void Init()
        {
            if (conn is not null)
                return;
            conn = new(dbPath);
            conn.CreateTable<Persona>();
        }

        public PersonaRepository(string dbPath1)
        {
            dbPath = dbPath1;
        }

        public void AddNewPerson(string nombre)
        {
            int result = 0;

            try
            {
                Init();
                if (string.IsNullOrEmpty(nombre))
                    throw new Exception("Nombre Requerido");
                Persona persona = new Persona() { Name = nombre };
                result = conn.Insert(persona);
                StatusMessage = string.Format("Filas agregadas", result, nombre);
            }
            catch(Exception ex)
            {
                StatusMessage = string.Format("Error al agregar", nombre, ex.Message);
            }
        }

        public List<Persona> GetAllPeorle()
        {
            try
            {
                Init();
                return conn.Table<Persona>().ToList();
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Error al mostrar los datos",  ex.Message);
            }
            return new List<Persona>();
        }

        public void UpdatePerson(Persona updatePerson)
        {
            try
            {
                Init();
                if (updatePerson == null)
                    throw new Exception("Persona no puede ser nula");

                int result = conn.Update(updatePerson);
                StatusMessage = string.Format("Filas actualizadas: {0} ", result);

            }catch (Exception ex)
            {
                StatusMessage = string.Format("Error al actualizar la persona: {0}", ex.Message);
            }
        }

        public void DeletePerson (int personId)
        {
            try
            {
                Init();
                int result = conn.Delete<Persona>(personId);
                StatusMessage = string.Format("Filas Eliminadas: {0}", result);
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Error al eliminar la persona con Id {0}: {1}", personId, ex.Message);
            }
        }

    }
}
