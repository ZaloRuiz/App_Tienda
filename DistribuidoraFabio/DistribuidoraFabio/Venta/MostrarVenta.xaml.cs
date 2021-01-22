using DistribuidoraFabio.Helpers;
using DistribuidoraFabio.Models;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DistribuidoraFabio.Venta
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MostrarVenta : ContentPage
	{
		ObservableCollection<DetalleVentaNombre> detalleVenta_lista = new ObservableCollection<DetalleVentaNombre>();
		public ObservableCollection<DetalleVentaNombre> DetallesVentas { get { return detalleVenta_lista; } }
        private int facturacomp = 0;
		private int ID_VENTA = 0;
		private DateTime FECHA;
		private int NUMERO_FACTURA = 0;
		private int CLIENTE;
		private int VENDEDOR;
		private string TIPO_VENTA;
        private decimal TOTAL = 0;
        public MostrarVenta(int id_venta, DateTime fecha, int numero_factura, int id_cliente, int id_vendedor, string tipo_venta, 
			decimal saldo, decimal total)
		{
            string BusyReason = "Cargando...";
            PopupNavigation.Instance.PushAsync(new BusyPopup(BusyReason));
            InitializeComponent();
			idVentaEntry.Text = Id.ToString();
			facturaEntry.Text = numero_factura.ToString();
			facturacomp = numero_factura;
            fechaEntry.Text = fecha.ToString("dd/MM/yyyy");
			totalEntry.Text = total.ToString("#.##") + " Bs.";
            GetDataCliente();
            GetDataVendedor();
			ID_VENTA = id_venta;
			FECHA = fecha;
			NUMERO_FACTURA = numero_factura;
			CLIENTE = id_cliente;
			VENDEDOR = id_vendedor;
			TIPO_VENTA = tipo_venta;
			TOTAL = total;
            if(TIPO_VENTA == "Credito")
			{
                tipoVentaEntry.Text = tipo_venta + " Saldo: " + saldo.ToString("#.##") + " Bs.";
            }
            else if(TIPO_VENTA == "Contado")
			{
                tipoVentaEntry.Text = tipo_venta;
            }
            MostrarDetalleVenta();
            PopupNavigation.Instance.PopAsync();
        }
        private async void GetDataCliente()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync("https://dmrbolivia.com/api_distribuidora/clientes/listaCliente.php");
            var clientes = JsonConvert.DeserializeObject<List<Models.Cliente>>(response).ToList();
            foreach (var item in clientes)
            {
                if(CLIENTE == item.id_cliente)
				{
                    clienteEntry.Text = item.nombre;
				}
            }
        }
        private async void GetDataVendedor()
        {
            try
            {
                HttpClient client = new HttpClient();
                var response = await client.GetStringAsync("https://dmrbolivia.com/api_distribuidora/vendedores/listaVendedores.php");
                var vendedores = JsonConvert.DeserializeObject<List<Vendedores>>(response).ToList();
                foreach (var item in vendedores)
                {
                    if(item.id_vendedor == VENDEDOR)
					{
                        vendedorEntry.Text = item.nombre;
					}
                }
            }
            catch (Exception error)
            {
                await DisplayAlert("Erro", error.ToString(), "OK");
            }
        }
        private async void MostrarDetalleVenta()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync("https://dmrbolivia.com/api_distribuidora/ventas/listaDetalleVentaNombre.php");
            var detalleVentaLista = JsonConvert.DeserializeObject<List<DetalleVentaNombre>>(response);

            int cont = detalleVentaLista.Count;
            int numProd = 0;
            
            foreach (var item in detalleVentaLista)
            {
                if (facturacomp == item.factura)
                {
                    BoxView boxView = new BoxView();
                    boxView.HeightRequest = 1;
                    boxView.BackgroundColor = Color.FromHex("#95B0B7");
                    stkPrd.Children.Add(boxView);

                    numProd = numProd + 1;
                    StackLayout stkP1 = new StackLayout();
                    stkP1.Orientation = StackOrientation.Horizontal;
                    stkPrd.Children.Add(stkP1);

                    Label label1 = new Label();
                    label1.Text = "Producto " + numProd.ToString() + ":";
                    label1.FontSize = 23;
                    label1.TextColor = Color.FromHex("#4DCCE8");
                    label1.WidthRequest = 200;
                    stkP1.Children.Add(label1);
                    Label entNomProd = new Label();
                    entNomProd.Text = item.display_text_nombre;
                    entNomProd.FontSize = 23;
                    entNomProd.TextColor = Color.FromHex("#95B0B7");
                    entNomProd.HorizontalOptions = LayoutOptions.FillAndExpand;
                    stkP1.Children.Add(entNomProd);

                    StackLayout stkP2 = new StackLayout();
                    stkP2.Orientation = StackOrientation.Horizontal;
                    stkPrd.Children.Add(stkP2);

                    Label label2 = new Label();
                    label2.Text = "Cantidad:";
                    label2.FontSize = 23;
                    label2.TextColor = Color.FromHex("#4DCCE8");
                    label2.WidthRequest = 200;
                    stkP2.Children.Add(label2);
                    Label entCant = new Label();
                    entCant.Text = item.cantidad.ToString();
                    entCant.FontSize = 23;
                    entCant.TextColor = Color.FromHex("#95B0B7");
                    entCant.HorizontalOptions = LayoutOptions.FillAndExpand;
                    stkP2.Children.Add(entCant);

                    StackLayout stkP3 = new StackLayout();
                    stkP3.Orientation = StackOrientation.Horizontal;
                    stkPrd.Children.Add(stkP3);

                    Label label3 = new Label();
                    label3.Text = "Precio:";
                    label3.FontSize = 23;
                    label3.TextColor = Color.FromHex("#4DCCE8");
                    label3.WidthRequest = 200;
                    stkP3.Children.Add(label3);
                    Label entPrec = new Label();
                    entPrec.Text = item.precio_producto.ToString("#.##") + " Bs.";
                    entPrec.FontSize = 23;
                    entPrec.TextColor = Color.FromHex("#95B0B7");
                    entPrec.HorizontalOptions = LayoutOptions.FillAndExpand;
                    stkP3.Children.Add(entPrec);

                    StackLayout stkP4 = new StackLayout();
                    stkP4.Orientation = StackOrientation.Horizontal;
                    stkPrd.Children.Add(stkP4);

                    Label label4 = new Label();
                    label4.Text = "Subtotal:";
                    label4.FontSize = 23;
                    label4.TextColor = Color.FromHex("#4DCCE8");
                    label4.WidthRequest = 200;
                    stkP4.Children.Add(label4);
                    Label entdesc = new Label();
                    entdesc.Text = item.sub_total.ToString("#.##") + " Bs.";
                    entdesc.FontSize = 23;
                    entdesc.TextColor = Color.FromHex("#95B0B7");
                    entdesc.HorizontalOptions = LayoutOptions.FillAndExpand;
                    stkP4.Children.Add(entdesc);
                }
            }
        }
    }
}