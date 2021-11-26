using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using System.Collections.ObjectModel;
using Tutoria_S6_app.Models;

namespace Tutoria_S6_app
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConsultaRegistro : ContentPage
    {
        private SQLiteAsyncConnection con;
        private ObservableCollection<Estudiante> TablaEstudiante;
        public ConsultaRegistro()
        {
            InitializeComponent();
            con = DependencyService.Get<DataBase>().GetConnection();
            get();
        }

        public async void get()
        {
            try
            {
                var resultado = await con.Table<Estudiante>().ToListAsync();
                TablaEstudiante = new ObservableCollection<Estudiante>(resultado);
                listaUsuarios.ItemsSource = TablaEstudiante;
            }
            catch(Exception ex)
            {
                DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private void listaUsuarios_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                var Obj = (Estudiante)e.SelectedItem;
                var item = Obj.Id.ToString();
                var nombre = Obj.Nombre.ToString();
                int id = Convert.ToInt32(item);

                Navigation.PushAsync(new Elemento(id, nombre));
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }
}