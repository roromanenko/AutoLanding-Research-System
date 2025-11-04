using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
	public sealed class SimulationResult
	{
		public List<double> Time { get; }
		public List<double> De { get; }
		public List<double> Dn { get; }
		public List<double> Gamma { get; }
		public List<double> Phi_g { get; }
		public List<double> Dzps { get; }
		public List<double> Z { get; }
		public List<double> Ik { get; }

		public SimulationResult(List<double> time, List<double> de, 
			List<double> dn, List<double> gamma,
			List<double> phi_g, List<double> dzps,
			List<double> z, List<double> ik)
		{
			Time = time;
			De = de;
			Dn = dn;
			Gamma = gamma;
			Phi_g = phi_g;
			Dzps = dzps;
			Z = z;
			Ik = ik;
		}
	}

	public class Simulation

	{
		private readonly CoefficientsCalculator _coeff;
		private readonly Params _parameters;

		public Simulation(CoefficientsCalculator coeff, Params parameters)
		{
			_coeff = coeff;
			_parameters = parameters;
		}

		public SimulationResult Run(double dt, double Z0, double phi_g0, double Sk, )
		{
			//Result lists
			var timeResult = new List<double>();
			var deResult = new List<double>();
			var dnResult = new List<double>();
			var gammaResult = new List<double>();
			var phi_gResult = new List<double>();
			var dzpsResult = new List<double>();
			var zResult = new List<double>();
			var ikResult = new List<double>();

			//Var arrays
			double[] x = new double[15];
			double[] y = new double[15];

			//Var additional
			double DN, DE;

			//Main loop
			while (true)
			{

			}

			return new SimulationResult(timeResult, deResult, dnResult, gammaResult, phi_gResult, dzpsResult, zResult, ikResult);
		}
	}
}
