using CitasApp1.Models;

namespace CitasApp1.Repositories
{
    public class CitaRepository
    {
        NutriCitasContext context = new();

        public Usuarios GetById(int id)
        {
            return context.Usuarios.Find(id);
        }
        public bool Validar(Usuarios u, out string Error)
        {
            List<string> ListaErrores = new List<string>();

            if (string.IsNullOrWhiteSpace(u.Nombre))
            {
                ListaErrores.Add("Escriba el nombre del usuario");
            }
            if (string.IsNullOrWhiteSpace(u.Telefono))
            {
                ListaErrores.Add("Escriba el nombre del usuario");
            }
            if (string.IsNullOrWhiteSpace(u.Correo))
            {
                ListaErrores.Add("Escriba el nombre del usuario");
            }





            Error=string.Join(Environment.NewLine, ListaErrores);

            return ListaErrores.Count!=0;

        }

        public IEnumerable<Usuarios> GetAll()
        {
            return context.Usuarios.OrderBy(s => s.Id);
        }


        public void Eliminar(Usuarios u)
        {
            context.Usuarios.Remove(u);
            context.SaveChanges();
        }

        public void Agregar(Usuarios u)
        {
            context.Usuarios.Add(u);
            context.SaveChanges();
        }

        public void Editar(Usuarios u)
        {
            context.Usuarios.Update(u);
            context.SaveChanges();
        }



        public Usuarios GetSerieById(int id)
        {
            return context.Usuarios.Find(id);

        }

    }
}
