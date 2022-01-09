using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WePing.Service.Spid
{
    public interface ICalculateurPoints
    {
        (double, double) Calculate(double pointsA, double pointsB, bool joueurAGagne, double coef);
    }
    internal class CalculateurPoints: ICalculateurPoints
    {
        SortedList<int, double> _vn = new SortedList<int, double>() { {24,6 },{ 49, 5.5 },{99,5 },{ 149, 4 },{ 199,3},{299,2 },{399,1 },{499,0.5 },{Int32.MaxValue,0 } };
        SortedList<int, double> _dn = new SortedList<int, double>() { { 24, -5 }, { 49, -4.5 }, { 99, -4 }, { 149, -3 }, { 199, -2}, { 299, -1 }, { 399, -0.5 }, { Int32.MaxValue, 0 } };

        SortedList<int, double> _va = new SortedList<int, double>() { { 24, 6 }, { 49, 7}, { 99, 8 }, { 149,10 }, { 199, 13 }, { 299, 17 }, { 399, 22}, { 499,28 }, { Int32.MaxValue, 40 } };
        SortedList<int, double> _da = new SortedList<int, double>() { { 24, -5 }, { 49, -6 }, { 99, -7 }, { 149, -8 }, { 199, -10 }, { 299, -12.5 }, { 399, -16 }, { 499, -20 }, { Int32.MaxValue, -29 } };
        public CalculateurPoints()
        {
            
        }

        public (double,double) Calculate(double pointsA,double pointsB,bool joueurAGagne,double coef)
        {
            var ecart=Math.Abs( pointsA-pointsB);
            double pointsVictoire=0,pointsDefaite=0;

            if (joueurAGagne)
            {
                if (pointsA > pointsB)
                {
                    pointsVictoire= _vn.Where(s => s.Key > ecart).First().Value;
                    pointsDefaite=_dn.Where(s => s.Key > ecart).First().Value;
                }
                else
                {
                    pointsVictoire = _va.Where(s => s.Key > ecart).First().Value;
                    pointsDefaite = _da.Where(s => s.Key > ecart).First().Value;
                }
                return (pointsVictoire * coef, pointsDefaite * coef);
            }
            else
            {
                if (pointsA < pointsB)
                {
                    pointsVictoire = _vn.Where(s => s.Key > ecart).First().Value;
                    pointsDefaite = _dn.Where(s => s.Key > ecart).First().Value;
                }
                else
                {
                    pointsVictoire = _va.Where(s => s.Key > ecart).First().Value;
                    pointsDefaite = _da.Where(s => s.Key > ecart).First().Value;
                }
                return (pointsDefaite * coef, pointsVictoire * coef);
            }
        }
    }
}
