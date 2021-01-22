using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DistribuidoraFabio.Helpers;
using DistribuidoraFabio.Models;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DistribuidoraFabio.Compra
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MostrarCompra : ContentPage
	{
		ObservableCollection<DetalleCompraNombre> detalleCompra_lista = new ObservableCollection<DetalleCompraNombre>();
		public ObservableCollection<DetalleCompraNombre> DetallesCompras { get { return detalleCompra_lista; } }
        private int facturacomp = 0;
		private int ID_COMPRA = 0;
		private DateTime FECHA;
		private int NUMERO_FACTURA = 0;
		private int PROVEEDOR;
		private decimal SALDO = 0;
		private decimal TOTAL = 0;
        string BusyReason = "Cargando...";
        public MostrarCompra(int id_compra, DateTime fecha_compra, int numero_factura, int id_proveedor, decimal saldo, decimal total)
		{
			InitializeComponent();
            PopupNavigation.Instance.PushAsync(new BusyPopup(BusyReason));
            idCompraEntry.Text = id_compra.ToString();
			facturaEntry.Text = numero_factura.ToString();
			facturacomp = numero_factura;
			fechaEntry.Text = fecha_compra.ToString("dd/MM/yyyy");
            GetDataProveedor();
            totalEntry.Text = total.ToString("#.##") + " Bs.";
			ID_COMPRA = id_compra;
			FECHA = fecha_compra;
			NUMERO_FACTURA = numero_factura;
			PROVEEDOR = id_proveedor;
			SALDO = saldo;
			TOTAL = total;
            MostrarDetalleVenta();
            PopupNavigation.Instance.PopAsync();
        }
        private async void GetDataProveedor()
        {
            try
            {
                HttpClient client = new HttpClient();
                var response = await client.GetStringAsync("https://dmrbolivia.com/api_distribuidora/proveedores/listaProveedor.php");
                var proveedores = JsonConvert.DeserializeObject<List<Models.Proveedor>>(response).ToList();

                foreach(var item in proveedores)
				{
                    if(PROVEEDOR == item.id_proveedor)
					{
                        proveedorEntry.Text = item.nombre;
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
            var response = await client.GetStringAsync("https://dmrbolivia.com/api_distribuidora/compras/listaDetalleCompraNombre.php");
            var Compra_lista = JsonConvert.DeserializeObject<List<DetalleCompraNombre>>(response);

            int cont = Compra_lista.Count;
            int numProd = 0;
            foreach (var item in Compra_lista)
            {
                if (facturacomp == item.numero_factura)
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
                    label2.Text = "Cantidad";
                    label2.FontSize = 23;
                    label2.TextColor = Color.FromHex("#4DCCE8");
                    label2.WidthRequest = 200;
                    stkP2.Children.Add(label2);
                    Label entCant = new Label();
                    entCant.Text = item.cantidad_compra.ToString();
                    entCant.FontSize = 23;
                    entCant.TextColor = Color.FromHex("#95B0B7");
                    entCant.HorizontalOptions = LayoutOptions.FillAndExpand;
                    stkP2.Children.Add(entCant);

                    StackLayout stkP3 = new StackLayout();
                    stkP3.Orientation = StackOrientation.Horizontal;
                    stkPrd.Children.Add(stkP3);

                    Label label3 = new Label();
                    label3.Text = "Precio";
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
                    label4.Text = "Descuento";
                    label4.FontSize = 23;
                    label4.TextColor = Color.FromHex("#4DCCE8");
                    label4.WidthRequest = 200;
                    stkP4.Children.Add(label4);
                    Label entdesc = new Label();
                    if(item.descuento_producto < 1)
					{
                        entdesc.Text = "0 Bs.";
					}
                    else
					{
                        entdesc.Text = item.descuento_producto.ToString("#.##") + " Bs.";
                    }
                    entdesc.FontSize = 23;
                    entdesc.TextColor = Color.FromHex("#95B0B7");
                    entdesc.HorizontalOptions = LayoutOptions.FillAndExpand;
                    stkP4.Children.Add(entdesc);
                }
            }
        }
    }
}