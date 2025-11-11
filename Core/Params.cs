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
		public double C_z_delta_n { get; init; } = -0.183;

		// Control law params general
		public double k_gamma { get; init; } = 2;
		public double k_omega_x { get; init; } = 1.5;
		public double k_omega_y { get; init; } = 2.5;
		public double kdot_omega_y { get; init; } = 2.0;
		public double k3 { get; init; } = 1.3;
		public double k6 { get; init; } = 1.3;
		public double k10 { get; init; } = 8.0;
		public double k15 { get; init; } = 1.0;
		public double k5 { get; set; } = 0.0;
		public double k17 { get; set; } = 0.0;
		public double T_omega_x { get; init; } = 1.6;
		public double T_omega_y { get; init; } = 2.5;
		public double T5 { get; init; } = 2.3;
		public double T15 { get; init; } = 0.85;
		public double T17 { get; init; } = 2.3;
		public double Skn { get; init; } = 167;
	}
}
