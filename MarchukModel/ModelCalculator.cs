using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarchukModel
{
    class ModelCalculator
    {
        public delegate double Funkcja(double x);
        public delegate double FunkcjaG(double x, double y);

        public double v0 = 0, c0 = 0, f0 = 0;
        public List<double> vt = new List<double>();
        public List<double> ct = new List<double>();
        public List<double> ft = new List<double>();


        double a = 0; //alfa (zmienne w czasie)
        //Przykładowe wartości z środków przedziałów
        double b = 0.8; //beta
        double g = 0.8; //gamma
        double tt = 50; //tau
        double r = 2.0; //rho
        double n = 8; //eta
        //Jeszcze nieznane więc ustawione jako 1
        double mc = 1; //mi c
        double mf = 1; //mi f
        double mm = 1; //mi m

        double maxt = 160; //koniec przedziału


        public ModelCalculator()
        {

        }

        public bool Simulate()
        {
            vt.Clear(); ct.Clear(); ft.Clear();

            vt.Add(v0);
            ct.Add(c0);
            f0 = (r * c0) / mf;
            ft.Add(f0);

            for (int t = 1; t <= maxt; t++)
            {
                double x1=V(t), x2=C(t), x3=F(t);
                vt.Add(x1);
                ct.Add(x2);
                ft.Add(x3);
                Console.WriteLine("V(" + t + ")=" + x1 + " C(" + t + ")=" + x2 + " F(" + t + ")=" + x3);
            }
            return true;
        }


        double V(double t)
        {
            if (t == 0) return vt[0];
            else if (t < 0) return 0;
            if (vt.Count - 1 >= t) return vt[(int)t];

            double result = 0;
            result = v0 * Math.Exp(b * t - g * Integral(0, t-1, F)); //do t-1 ponieważ patrzymy dla kroku wcześniejszego
            return result;
        }

        double C(double t)
        {
            if (t == 0) return ct[0];
            else if (t < 0) return ct[0];
            if (ct.Count - 1 >= t) return ct[(int)t];

            double result = 0;
            result = c0 + IntegralG(0, t-1, G);
            return result;
        }

        double F(double t)
        {
            if (t == 0) return ft[0];
            else if (t < 0) return ft[0];
            if (ft.Count - 1 >= t) return ft[(int)t];

            double result = 0;
            result = f0 + n * (V(t - 1) - (v0 * Math.Exp(-mf * t))) + ((r * Math.Exp(-mf + t)) * Integral(0, t - 1, CF)) - ((n * (mf + b) * Math.Exp(-mf * t)) * Integral(0, t - 1, CVF));
            return result;
        }


        double CF(double t)
        {
            double result = 0;
            result = (a / g) * ( (mc+b)*Math.Exp(-mc*t) * Integral(0,t, CCF) - V(t) + (v0*Math.Exp(-mc*t)));
            result *= Math.Exp(mf * t);
            return result;
        }

        double CCF(double s)
        {
            double result = 0;
            result = Math.Exp(mc * s) * V(s);
            return result;
        }

        double CVF(double t)
        {
            double result = 0;
            result = Math.Exp(mf * t) * V(t);
            return result;
        }

        double G(double s, double t)
        {
            if (s < 0) return 0;

            double result = 0;
            result = (V(s - tt) * F(s - tt)) * Math.Exp(-mc * (t - s));
            return result;
        }


        //Całka
        double Integral(double a, double b, Funkcja f)
        {
            double result = 0;
            double n = b - a;
            if (n <= 0) return 0;
            double h = (b - a) / n; //dzielimy na przedziały o długości 1
            for (int i = 1; i < n; i++)
            {
                result += f(a + ((i - 1) * h)) + f(a + (i * h)); //F(xi)+F(xi+1)
            }
            result *= h / 2;
            return result;
        }
        double IntegralG(double a, double b, FunkcjaG f)
        {
            double result = 0;
            double n = b - a;
            if (n <= 0) return 0;
            double h = (b - a) / n; //dzielimy na przedziały o długości 1
            for (int i = 1; i < n; i++)
            {
                result += f(a + ((i - 1) * h), b) + f(a + (i * h), b); //F(xi)+F(xi+1)
            }
            result *= h / 2;
            return result;
        }

        double A(double t)
        {
            return 1000 * (1 + (0.9 * Math.Sin(((2 * Math.PI * t) / 1) * (Math.PI / 180))));
        }

    }
}
