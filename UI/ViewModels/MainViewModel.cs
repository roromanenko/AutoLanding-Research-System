using Core;
using OxyPlot;
using OxyPlot.Series;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using UI.Models;

namespace UI.ViewModels
{
	public class MainViewModel : BaseViewModel
	{
		private readonly Simulation _simulation;

		// параметры
		private double _dt = 0.1;
		public double Dt { get => _dt; set => SetField(ref _dt, value); }

		private double _z0 = 0;
		public double Z0 { get => _z0; set => SetField(ref _z0, value); }

		private double _phiG0 = 0;
		public double PhiG0 { get => _phiG0; set => SetField(ref _phiG0, value); }

		private double _sk = 1;
		public double Sk { get => _sk; set => SetField(ref _sk, value); }

		// график
		private PlotModel _plotModel;
		public PlotModel PlotModel { get => _plotModel; set => SetField(ref _plotModel, value); }

		// таблица
		private ObservableCollection<TableRow> _tableData = new();
		public ObservableCollection<TableRow> TableData { get => _tableData; set => SetField(ref _tableData, value); }

		public RelayCommand RunCommand { get; }

		public MainViewModel()
		{
			var coeff = new CoefficientsCalculator();
			var parameters = new Params();
			_simulation = new Simulation(coeff, parameters);

			RunCommand = new RelayCommand(RunSimulation);

			PlotModel = new PlotModel { Title = "Z(Dzps)" };
		}

		private void RunSimulation()
		{
			try
			{
				var result = _simulation.Run(Dt, Z0, PhiG0, Sk);

				// график
				var series = new LineSeries
				{
					Color = OxyColors.Blue,
					StrokeThickness = 3,
					LineStyle = LineStyle.Solid
				};

				int step = Math.Max(1, result.Z.Count / 10000); // ограничим до 10к точек
				for (int i = 0; i < result.Z.Count; i += step)
				{
					series.Points.Add(new DataPoint(result.Dzps[i], result.Z[i]));
				}

				var model = new PlotModel { Title = "Z(Dzps)" };
				model.Axes.Add(new OxyPlot.Axes.LinearAxis
				{
					Position = OxyPlot.Axes.AxisPosition.Bottom,
					Title = "Dzps",
					StartPosition = 1,
					EndPosition = 0
				});
				model.Axes.Add(new OxyPlot.Axes.LinearAxis
				{
					Position = OxyPlot.Axes.AxisPosition.Left,
					Title = "Z"
				});
				model.Series.Add(series);
				PlotModel = model;

				// таблица (каждые 5 секунд)
				TableData = new ObservableCollection<TableRow>(
					result.Time
					.Select((t, i) => new { t, i })
					.Where(x => x.t % 5 < Dt) // выборка каждые ~5 секунд
					.Select(x => new TableRow
					{
						Time = result.Time[x.i],
						De = result.De[x.i],
						Dn = result.Dn[x.i],
						Gamma = result.Gamma[x.i],
						Phi_g = result.Phi_g[x.i],
						Dzps = result.Dzps[x.i],
						Z = result.Z[x.i],
						Ik = result.Ik[x.i]
					})
				);

				OnPropertyChanged(nameof(PlotModel));
				OnPropertyChanged(nameof(TableData));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Simulation Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}
	}
}
