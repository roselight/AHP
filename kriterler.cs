using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHP
{
    public class kriterler
    {
        public kriterler(int boyut)
        {
            kriterdeger = new double[boyut];
            x = boyut;
        }
        public double[] kriterdeger, RI,sonucMatrisi;
        public double[,] Amatris, cmatrisi, wmatrisi, dmatrisi, ematrisi, kmatrisi;
        public string[] ilce;
        public double lamda, CI= 0;
        private double CR = 0;
        public int x = 0;
        public double CR1
        {
            get { return CR; }
            set
            {
                if (CR > 0.10)
                { throw new Exception("Tutarsız Veri Girişi"); }
                else
                    CR = value;
            }
        }

        public void matrisolustur()
        {
            Amatris = new double[x, x];
            cmatrisi = new double[x, x];
            wmatrisi = new double[x, 1];
            dmatrisi = new double[x, 1];
            ematrisi = new double[x, 1];
            sonucMatrisi = new double[x];
            RI = new double[] { 0, 0, 0.58, 0.90, 1.12, 1.24, 1.41, 1.45, 1.49, 1.51, 1.48, 1.56 };
           
        }
        
    }
}
