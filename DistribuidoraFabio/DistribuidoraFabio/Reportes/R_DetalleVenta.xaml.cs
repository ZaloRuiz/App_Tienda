using DistribuidoraFabio.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DistribuidoraFabio.Reportes
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class R_DetalleVenta : ContentPage
	{
		public R_DetalleVenta()
		{
			InitializeComponent();
			BindingContext = new R_DetalleVentaVM();
		}
	}
}