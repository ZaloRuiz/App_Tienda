using DistribuidoraFabio.Helpers;
using DistribuidoraFabio.Models;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DistribuidoraFabio.Venta
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ListaPedidos : TabbedPage
	{
		List<Ventas> _Entregado = new List<Ventas>();
		List<Ventas> _Pendiente = new List<Ventas>();
		public ListaPedidos()
		{
			InitializeComponent();
		}
		protected async override void OnAppearing()
		{
			base.OnAppearing();
			string BusyReason = "Cargando...";
			await PopupNavigation.Instance.PushAsync(new BusyPopup(BusyReason));
			HttpClient client = new HttpClient();
			var response = await client.GetStringAsync("https://dmrbolivia.com/api_distribuidora/ventas/listaVenta.php");
			var ventas = JsonConvert.DeserializeObject<List<Ventas>>(response);
			foreach(var item in ventas)
			{
				if(item.saldo == 0)
				{
					_Entregado.Add(item);
				}
				else if(item.saldo != 0)
				{
					_Pendiente.Add(item);
				}
			}
			listaEntregados.ItemsSource = _Entregado;
			listaPendientes.ItemsSource = _Pendiente;
			await PopupNavigation.Instance.PopAsync();
		}
		private void ToolbarItem_Clicked(object sender, EventArgs e)
		{
			Navigation.PushAsync(new AgregarVenta());
		}
		private async void OnItemSelectedE(object sender, ItemTappedEventArgs e)
		{
			var detalles = e.Item as Ventas;
			await Navigation.PushAsync(new MostrarVenta(detalles.id_venta, detalles.fecha, detalles.numero_factura, detalles.id_cliente,
														detalles.id_vendedor, detalles.tipo_venta, detalles.saldo, detalles.total));
		}
		private async void OnItemSelectedP(object sender, ItemTappedEventArgs e)
		{
			var detalles = e.Item as Ventas;
			await Navigation.PushAsync(new MostrarVenta(detalles.id_venta, detalles.fecha, detalles.numero_factura, detalles.id_cliente,
														detalles.id_vendedor, detalles.tipo_venta, detalles.saldo, detalles.total));
		}
	}
}