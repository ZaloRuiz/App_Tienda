using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace DistribuidoraFabio.ViewModels
{
	class R_DetalleVentaVM : INotifyPropertyChanged
	{
		DateTime fecha_inicio;
		DateTime fecha_final;
		public event PropertyChangedEventHandler PropertyChanged;
		private void OnPropertyChanged(string property)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(property));
		}
		private bool _isRefreshing;
		ObservableCollection<Models._RDetalleVenta> _reporteDV = new ObservableCollection<Models._RDetalleVenta>();
		public ObservableCollection<Models._RDetalleVenta> ReportesDVs
		{
			get { return _reporteDV; }
			set
			{
				if (_reporteDV != value)
				{
					_reporteDV = value;
					OnPropertyChanged("ReportesDVs");
				}
			}
		}
		public bool IsRefreshing
		{
			get
			{
				return _isRefreshing;
			}
			set
			{
				_isRefreshing = value;
				OnPropertyChanged(nameof(IsRefreshing));
			}
		}
		public ICommand RefreshCommand { get; set; }
		private async void CmdRefresh()
		{
			IsRefreshing = true;
			await Task.Delay(1500);
			IsRefreshing = false;
		}
		public R_DetalleVentaVM(DateTime _fechaInicio, DateTime _fechaFinal)
		{
			fecha_inicio = _fechaInicio;
			fecha_final = _fechaFinal;
			_reporteDV = new ObservableCollection<Models._RDetalleVenta>();
			GetReporte();
			RefreshCommand = new Command(CmdRefresh);
		}
		public async void GetReporte()
		{
			try
			{
				HttpClient client = new HttpClient();
				var response = await client.GetStringAsync("https://dmrbolivia.com/api_distribuidora/reportes/ReporteDetalleVenta.php");
				var _dataRDV = JsonConvert.DeserializeObject<List<Models._RDetalleVenta>>(response);

				foreach (var item in _dataRDV)
				{
					if(item.fecha.Ticks > fecha_inicio.Ticks && item.fecha.Ticks < fecha_final.Ticks)
					{
						_reporteDV.Add(new Models._RDetalleVenta
						{
							id_venta = item.id_venta,
							nombre = item.nombre,
							fecha = item.fecha,
							codigo_c = item.codigo_c,
							nombre_cliente = item.nombre_cliente,
							razon_social = item.razon_social,
							nit = item.nit,
							telefono = item.telefono,
							direccion_cliente = item.direccion_cliente,
							geolocalizacion = item.geolocalizacion,
							nombre_producto = item.nombre_producto,
							precio_producto = item.precio_producto,
							cantidad = item.cantidad,
							sub_total = item.sub_total,
							envases = item.envases,
							tipo_venta = item.tipo_venta,
							estado = item.estado
						});
					}
				}
			}
			catch (Exception err)
			{
				Console.WriteLine("###################################################" + err.ToString());
			}
		}
	}
}
