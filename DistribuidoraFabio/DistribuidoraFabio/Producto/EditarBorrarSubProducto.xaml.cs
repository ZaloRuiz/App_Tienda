using DistribuidoraFabio.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DistribuidoraFabio.Producto
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditarBorrarSubProducto : ContentPage
	{
		public EditarBorrarSubProducto(int id_subproducto, string nombre_sub_producto)
		{
			InitializeComponent();
			idsubproductoEntry.Text = id_subproducto.ToString();
			nombreSpEntry.Text = nombre_sub_producto;
		}
        private async void BtnEditarSP_Clicked(object sender, EventArgs e)
        {
            try
            {
                Sub_producto sub_Producto = new Sub_producto()
                {
                    id_subproducto = Convert.ToInt32(idsubproductoEntry.Text),
                    nombre_sub_producto = nombreSpEntry.Text
                };

                var json = JsonConvert.SerializeObject(sub_Producto);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpClient client = new HttpClient();
                var result = await client.PostAsync("http://dmrbolivia.com/api_distribuidora/subproductos/editarSubproducto.php", content);

                if (result.StatusCode == HttpStatusCode.OK)
                {
                    await DisplayAlert("EDITADO", "Se edito correctamente", "OK");
                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("ERROR", result.StatusCode.ToString(), "OK");
                    await Navigation.PopAsync();
                }
            }
            catch (Exception err)
            {
                await DisplayAlert("ERROR", err.ToString(), "OK");
            }
        }
        private async void BtnBorrarSP_Clicked(object sender, EventArgs e)
        {
            try
			{
                Sub_producto sub_Producto = new Sub_producto()
                {
                    id_subproducto = Convert.ToInt32(idsubproductoEntry.Text),
                    nombre_sub_producto = nombreSpEntry.Text
                };

                var json = JsonConvert.SerializeObject(sub_Producto);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpClient client = new HttpClient();
                var result = await client.PostAsync("https://dmrbolivia.com/api_distribuidora/subproductos/borrarSubproducto.php", content);

                if (result.StatusCode == HttpStatusCode.OK)
                {
                    await DisplayAlert("ELIMINADO", "Se elimino correctamente", "OK");
                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("ERROR", result.StatusCode.ToString(), "OK");
                    await Navigation.PopAsync();
                }
            }
            catch(Exception err)
			{
                await DisplayAlert("ERROR", err.ToString(), "OK");
			}
        }
    }
}