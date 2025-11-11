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

		public SimulationResult Run(double dt, double Z0, double phi_g0, double Sk)
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

			x[5] = phi_g0;
			y[6] = Z0;
			y[7] = 18000;

			//Var additional
			double DN, DE;

			var coeffs = _coeff.ComputeCoefficients(_parameters);
			var a = coeffs.A;
			var b = coeffs.B;

			double t = 0;

			//Var control law
			double Dg = 57.3 * 500 / 2.67 - 300;
			double Ek;
			double EkStar;
			double IkStar;

			double deFunc;
			double dnFunc;
			double kFunc;
			double Func1Var;
			double Func2Var;
			double Func1;
			double Func2;

			//Main loop
			while (y[7] > 0)
			{
				//Calculate ControlLaw
				if (y[7] > Dg)
				{
					_parameters.k5 = 2;
					_parameters.k17 = 170;
				}
				else
				{
					_parameters.k5 = 3;
					_parameters.k17 = 120;
				}

				EkStar = 57.3 * Math.Atan(y[6] / (y[7] + 4000));
				IkStar = Sk * EkStar;
				Ek = y[13] / _parameters.Skn;

				kFunc = Math.Clamp(IkStar, -250, 250);

				Func1Var = (-_parameters.k6 * x[5] + _parameters.k10 * Ek);
				Func1 = Math.Clamp(Func1Var, -25, 25);

				Func2Var = _parameters.k6 * x[5] + x[8] + x[9] + Func1;
				Func2 = Math.Clamp(Func2Var, -20, 20);

				deFunc = _parameters.k_gamma * (y[2] - y[10]) + y[11];
				DE = Math.Clamp(deFunc, -12, 12);

				dnFunc = _parameters.k_omega_y * x[0] + y[12];
				DN = Math.Clamp(dnFunc, -10, 10);

				//Calculate derivatives
				x[0] = y[1];																	//phi`
				x[1] = -a[1] * x[0] - b[6] * x[2] - a[2] * y[4] - a[3] * DN - b[5] * DE;		//phi``
				x[2] = y[3];																	//gamma`
				x[3] = -a[6] * x[0] - b[1] * x[2] - b[2] * y[4] - a[5] * DN - b[3] * DE;		//gamma``
				x[4] = x[0] + b[7] * x[2] + b[4] * y[2] - a[4] * y[4] - a[7] * DN;				//beta`
				x[5] = -y[0];																	//phi_g
				x[6] = _parameters.V * Math.Sin((x[5] + y[4]) / 57.3);							//Z`
				x[7] = - _parameters.V * Math.Cos((x[5] + y[4]) / 57.3);						//Dzps`
				x[8] = (_parameters.k5 * x[5] - y[8]) / _parameters.T5;
				x[9] = (_parameters.k17 * Ek - y[9]) / _parameters.T17;
				x[10] = -(Func2 + y[10]) / _parameters.T15;
				x[11] = _parameters.k_omega_x * x[2] - y[11] / _parameters.T_omega_x;
				x[12] = _parameters.k_omega_y * x[0] - y[12] / _parameters.T_omega_y;
				x[13] = (kFunc - y[13]) / 0.2;

				y = EulerIntegration(x, y, dt);

				//Results
				timeResult.Add(t);
				deResult.Add(DE);
				dnResult.Add(DN);
				gammaResult.Add(y[2]);
				phi_gResult.Add(x[5]);
				dzpsResult.Add(y[7]);
				zResult.Add(y[6]);
				ikResult.Add(y[13]);

				t += dt;
			}

			return new SimulationResult(timeResult, deResult, dnResult, gammaResult, phi_gResult, dzpsResult, zResult, ikResult);
		}

		public static double[] EulerIntegration(double[] x, double[] y, double dt)
		{
			for (int k = 0; k < x.Length; k++)
			{
				y[k] += x[k] * dt;
			}

			return y;
		}
	}
}
