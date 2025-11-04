using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
	public class Params
	{
		//Aircraft geometry and inertia
		public double S { get; init; } = 201.45;        //S, m^2
		public double l { get; init; } = 37.55;         //l, m
		public double G0 { get; init; } = 80000;         //G, kg
		public double Ix { get; init; } = 250000;       //Ix, kg m s^2
		public double Iy { get; init; } = 900000;       //Iy, kg m s^2

		// Flight conditions
		public double V { get; init; } = 78;          // V0, m/s
		public double H { get; init; } = 500;           // H0, m
		public double Rho { get; init; } = 0.119;      // ρ, кг·с^2/м^4
		public double g { get; init; } = 9.81;          // g, m/s^2
		public double Alpha_bal { get; init; } = 7.1;        // alpha, degrees
		public double Theta0 { get; init; } = 0;        // Theta, degrees

		// Pitching moment
		public double m_y_omega_y { get; init; } = -0.21;
		public double m_y_beta { get; init; } = -0.2;
		public double m_y_delta_n { get; init; } = -0.0716;
		public double m_x_delta_n { get; init; } = -0.0206;
		public double m_x_omega_y { get; init; } = -0.31;
		public double m_x_omega_x { get; init; } = -0.583;
		public double m_x_beta { get; init; } = -0.186;
		public double m_x_delta_e { get; init; } = -0.0688;
		public double m_y_delta_e { get; init; } = 0.0;
		public double m_y_omega_x { get; init; } = -0.006;

		// Aerodynamic coefficients
		public double C_z_beta { get; init; } = -1.0715;
		public double C_z_delta_n { get; init; } = -0.1759;

		// Control law params general
		public double k_gamma { get; init; } = 2;
		public double k_omega_x { get; init; } = 1.5;
		public double k_omega_y { get; init; } = 2.5;

		// Control law params 1
		public double k_gamma_set { get; init; } = 0.7;

		// Control law params 3
		public double k_z { get; init; } = 0.02;
		public double k_zDot { get; init; } = 0.7;
	}
}
