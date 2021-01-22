using DistribuidoraFabio.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DistribuidoraFabio.Producto
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ListaSubProducto : ContentPage
	{
		public ListaSubProducto()
		{
			InitializeComponent();
		}
        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AgregarSubProducto());
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync("https://dmrbolivia.com/api_distribuidora/subproductos/listaSubproducto.php");
            var subproducto = JsonConvert.DeserializeObject<List<Sub_producto>>(response);

            listaSubP.ItemsSource = subproducto;
        }

        private async void OnItemSelected(object sender, ItemTappedEventArgs e)
        {
            var detalles = e.Item as Sub_producto;
            await Navigation.PushAsync(new EditarBorrarSubProducto(detalles.id_subproducto, detalles.nombre_sub_producto));
        }
    }
}