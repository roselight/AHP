using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHP
{
    public class KararMatrisi
    {

        private double[,] _kmatris, w_Vektor;
        private int veri;

        public int Veri
        {
            get { if(veri==null)
                veri = 0;
                return veri;
            }
            set { veri = value; }
        }

    
        public double[,] W_Vektor
        {
            get
            {
                if (w_Vektor == null)
                    w_Vektor = new double[6,1]; 
                return w_Vektor;
            }
            set { w_Vektor = value; }
        }
        public double[,] Kmatris
        {
          get {   if(_kmatris==null)
             _kmatris= new double[5, 6];

            return _kmatris; }
     
        }
        private static KararMatrisi nesne;
        public static KararMatrisi getNesne()
        {
            if (nesne == null)
                nesne = new KararMatrisi();
            return nesne;

        }
        private KararMatrisi()
        {

        }
        public void kmatrisi()
        {

        }

    }
}
