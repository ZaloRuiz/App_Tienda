using DistribuidoraFabio.Models;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DistribuidoraFabio
{
	public partial class App : Application
	{
		public static NavigationPage NavigationPage { get; private set; }
		public App()
		{
			InitializeComponent();
			MainPage = new Menu();
			//MainPage = new NavigationPage(new Menu());
		}
		public static ObservableCollection<DetalleVenta_previo> _detalleVData = new ObservableCollection<DetalleVenta_previo>();
		public static ObservableCollection<DetalleVenta_previo> _DetalleVentaData { get { return _detalleVData; } }
		public static ObservableCollection<DetalleCompra_previo> _detalleCData = new ObservableCollection<DetalleCompra_previo>();
		public static ObservableCollection<DetalleCompra_previo> _DetalleCompraData { get { return _detalleCData; } }
		protected override void OnStart()
		{
		}

		protected override void OnSleep()
		{
		}

		protected override void OnResume()
		{
		}
	}
}
