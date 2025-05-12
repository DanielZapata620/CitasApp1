using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Windows.Input;
using CitasApp1.Models;
using GalaSoft.MvvmLight.Command;
using CitasApp1.Repositories;
using System.Collections.ObjectModel;

namespace CitasApp1.ViewModels
{
    public enum vistas { Principal, VerUsuarios, Agregar, Eliminar, Editar }
    public class CitasViewModel:INotifyPropertyChanged
    {
        CitaRepository repos = new();
        public vistas Vista { get; set; } = vistas.Principal;

        //Comandos
        
        public ICommand CambiarVistaCommand { get; set; }
        public ICommand AgregarUsuarioCommand { get; set; }
        public ICommand EliminarUsuarioCommand { get; set; }
        public ICommand EditarUsuarioCommand { get; set; }

        string error;

        public event PropertyChangedEventHandler? PropertyChanged;

        public string Error
        {
            get { return error; }
            set { error=value; }

        }


        public Usuarios Usuario { get; set; } = new();

        public ObservableCollection<Usuarios> Usuarios { get; set; } = new();
        public CitasViewModel()
        {
            EliminarUsuarioCommand=new RelayCommand(EliminarUsuario);
            EditarUsuarioCommand=new RelayCommand(EditarUsuario);
            AgregarUsuarioCommand=new RelayCommand(AgregarUsuario);
            CambiarVistaCommand=new RelayCommand<vistas> (CambiarVista);

            
        }

        private void EditarUsuario()
        {
            if (Usuario !=null && !repos.Validar(Usuario, out error))
            {
                var x = repos.GetById(Usuario.Id);
                if (x!=null)
                {
                    x.Nombre=Usuario.Nombre;
                    x.Id=Usuario.Id;
                    x.Correo=Usuario.Correo;
                    x.Telefono=Usuario.Telefono;
                    repos.Editar(x);
                    Vista=vistas.Principal;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));

                }
            }
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        }

  
        private void EliminarUsuario()
        {
            if (Usuario!=null)
            {
                repos.Eliminar(Usuario);
                Vista=vistas.Principal;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
            }
        }

        private void AgregarUsuario()
        {

            if (!repos.Validar(Usuario, out error))
            {
                repos.Agregar(Usuario);
                Vista=vistas.Principal;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));

            }

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        }



       
        private void CambiarVista(vistas vistas)
        {

            if (vistas==vistas.Agregar)
            {
                Error="";
                PropertyChanged?.Invoke(this, new(nameof(Error)));
                Usuario=new();
                PropertyChanged?.Invoke(this, new(nameof(Usuario)));
            }
            
            
            if (vistas==vistas.Editar)
            {
                if (Usuario!=null)
                {
                    Usuarios c = new Usuarios()
                    {
                        Nombre=Usuario.Nombre,
                        Id=Usuario.Id,
                        Correo=Usuario.Correo,
                        Telefono=Usuario.Telefono,
                    };
                    Usuario = c;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
                }
            }

            if (vistas==vistas.VerUsuarios)
            {
                Usuarios.Clear();
                foreach (var item in repos.GetAll())
                {
                    Usuarios.Add(item);
                }
            }

            Vista=vistas;
            PropertyChanged?.Invoke(this, new(nameof(Vista)));
        }

    }
}
