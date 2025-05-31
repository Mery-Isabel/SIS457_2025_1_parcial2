using CadParcial2miah;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClnParcial2miah
{
    public class SerieCln
    {
        // metodo para insertar una serie
        public static int insertar(Serie serie)
        {
            using (var context = new Parcial2miahEntities())
            {
                context.Serie.Add(serie); // agrega la serie a la base de datos
                if (serie.FechaEstreno.HasValue &&
                (serie.FechaEstreno.Value < new DateTime(1753, 1, 1) || serie.FechaEstreno.Value > new DateTime(9999, 12, 31)))
                {
                    throw new ArgumentOutOfRangeException("FechaEstreno", "La fecha debe estar entre 01/01/1753 y 31/12/9999.");
                }
                context.SaveChanges(); // guarda los cambios en la base de datos
                return serie.Id; // devuelve el id de la serie insertada

            }
        }

        // metodo para actualizar una serie
        public static int actualizar(Serie serie)
        {
            using (var context = new Parcial2miahEntities())
            {
                // este es como decir select * from Serie where Id = serie.Id
                var existente = context.Serie.Find(serie.Id); // busca la serie por su id
                //aqui se valida si la serie existe
                if (existente != null) // si existe la serie
                {
                    //actualizar los valores de la serie
                    existente.Titulo = serie.Titulo;
                    existente.Sinopsis = serie.Sinopsis;
                    existente.Director = serie.Director;
                    existente.Episodio = serie.Episodio;
                    existente.FechaEstreno = serie.FechaEstreno;
                    existente.Estado = serie.Estado;
                }

                return context.SaveChanges(); // guarda los cambios en la base de datos
            }
        }

        // metodo para eliminar una serie
        public static int eliminar(int id, string usuario) // escribe de parametro el id
        {
            using (var context = new Parcial2miahEntities())
            {
                var serie = context.Serie.Find(id); // busca la serie por su id
                serie.Estado = -1; // cambia el estado de la serie a -1 (eliminada)
                return context.SaveChanges(); // guarda los cambios en la base de datos
            }
        }

        // metodo para leer una serie por su id
        public static Serie obtenerUno(int id) // recibe de parametro un id
        {
            using (var context = new Parcial2miahEntities()) // crea una instancia del contexto de la base de datos
            {
                // este es como decir select * from Serie where Id = id
                return context.Serie.Find(id); // busca la serie por su id y la devuelve
            }
        }

        // metodo para listar o leer con el procedimiento almacenado
        public static List<paSerieListar_Result> leerPa(string parametro)
        {
            using (var context = new Parcial2miahEntities()) // crea una instancia del contexto de la base de datos
            {
                var result = context.paSerieListar(parametro).ToList(); // llama al procedimiento almacenado paSerieListar con el parametro dado
                Console.WriteLine(result.Count);
                // este es como decir exec paSerieListar @parametro
                return result;
            }
        }
    }
}
