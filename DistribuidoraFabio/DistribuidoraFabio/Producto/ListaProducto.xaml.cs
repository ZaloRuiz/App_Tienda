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
	public partial class ListaProducto : ContentPage
	{
		public ListaProducto()
		{
			InitializeComponent();
		}
		private void ToolbarItem_Clicked(object sender, EventArgs e)
		{
			Navigation.PushAsync(new AgregarProducto());
		}
		protected async override void OnAppearing()
		{
			base.OnAppearing();

			HttpClient client = new HttpClient();
			var response = await client.GetStringAsync("https://dmrbolivia.com/api_distribuidora/productos/listaProducto.php");
			var productos = JsonConvert.DeserializeObject<List<Models.Producto>>(response);

			listaProd.ItemsSource = productos;
		}
		private async void OnItemSelected(object sender, ItemTappedEventArgs e)
		{
			var detalles = e.Item as Models.Producto;
			await Navigation.PushAsync(new EditarBorrarProducto(detalles.id_producto, detalles.nombre, detalles.id_tipo_producto, detalles.id_sub_producto, detalles.stock,
				detalles.stock_valorado, detalles.promedio, detalles.precio_venta, detalles.producto_alerta));
		}
		private void btnFlotante_Clicked(object sender, EventArgs e)
		{
			if (stkSubProducto.IsVisible == false)
			{
				stkSubProducto.IsVisible = true;
			}
			else
			{
				stkSubProducto.IsVisible = false;
			}
			if (stkTipoProducto.IsVisible == false)
			{
				stkTipoProducto.IsVisible = true;
			}
			else
			{
				stkTipoProducto.IsVisible = false;
			}
		}

		private async void btnRedirect_TP_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new ListaTipoProducto());
		}

		private async void btnRedirect_SP_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new ListaSubProducto());
		}
	}
}