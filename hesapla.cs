using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
 

namespace AHP
{   
    public class hesapla
    {
        public int boyut=0;
        public List<kriterler> krlist = new List<kriterler>();
        kriterler kr; private double[,] _Kmatrisi;
        /// <summary>
        /// kriter sınıfında oluşturulacak olan matrisler için boyutların gönderilmesi sağlanır.
        /// </summary>
        /// <param name="deger">main de faktörler dizisinden gelen değer</param>
        public hesapla(params double[] deger)
        {
            boyut = deger.Count();
            if (kr == null)
            { kr = new kriterler(boyut); }
            for (int i = 0; i < deger.Count(); i++)
            {
                kr.kriterdeger[i] = deger[i];
            }

            if (_Kmatrisi == null)
                _Kmatrisi = new double[boyut, boyut];
        }
       
        public double topla = 0;
        /// <summary>
        /// Kullanıcıdan alınan verilere göre yapılan karşılaştırma işlemleri sonucu oluşan Amatrisi
        /// </summary>
        
        public void Amatrisiolus()
        {
            kr.matrisolustur();
            for (int i = 0; i < boyut; i++)
			{
                for (int j = 0; j < boyut; j++)
			{
                 if(i==j)
                kr.Amatris[i, j] = 1;
                 else if (i < j)
                 {
                     if (kr.kriterdeger[i] == kr.kriterdeger[j])
                         kr.Amatris[i, j] = 1;
                     else if (kr.kriterdeger[i] > kr.kriterdeger[j])
                         kr.Amatris[i, j] = kr.kriterdeger[i] - kr.kriterdeger[j];
                     else
                         kr.Amatris[i, j] = Math.Round(1 / Math.Abs(kr.kriterdeger[i] - kr.kriterdeger[j]),2);

                 }
                 else
                 {
                     kr.Amatris[i, j] = 1 / kr.Amatris[j, i];
                 }
                 
			}
			}
             
                
        }

        public void yazdır(TextBox a)
        {
            int b = 0;
           
            for (int i = 0; i <boyut; i++)
              {
                  a.Text += "";
            for (int j = 0; j < boyut; j++)
            {
                String s = String.Format("{0:0.##}", kr.Amatris[i,j]);
                a.Text += s;
                a.Text += "\t";

            } a.Text += "\r\n";
              }
            a.Text += "------------------------------------------\r\n";
          

           
        }


        /// <summary>
        /// A matrisinde her bir hücrenin bulunduğu sütundaki tüm verilerin toplamına bölünmesi ile bulunan B matrisleri,
        /// birleşerek C matrisini oluşturur.
        /// </summary>
        public void Cmatrisi()
        {
            for (int i = 0; i < boyut; i++)
            {
                for (int j = 0; j < boyut; j++)
                {
                    topla += kr.Amatris[j, i];

                }
                for (int j = 0; j < boyut; j++)
                {
                    kr.cmatrisi[j, i] =  kr.Amatris[j, i] / topla;
                }
                topla = 0;
            }
        }
        /// <summary>
        /// Her bir faktör için m karar noktasındaki yüzde önem dağılımları matrisi
        /// C matrisinde her bir satırın aritmetik ortalaması bulunarak oluşturulan nx1 boyutlu W ağırlık matrisi 
        /// </summary>
        public void Wmatrisi(KararMatrisi k, int sayac)
        {
           
            for (int i = 0; i < boyut; i++)
            {
                for (int j = 0; j < boyut; j++)
                {
                    topla += kr.cmatrisi[i, j];

                }

                kr.wmatrisi[i, 0] = topla / boyut;
                topla = 0; 
                if (sayac == 0)
                {
                    k.W_Vektor[i, 0] = kr.wmatrisi[i, 0];
                }
            }
        }
        /// <summary>
        /// W matrisinden sonra yapılan hesaplamalar sonuçların tutarlılık değerini hesaplayarak,
        /// veri girişlerinin tutarlı olup olmadığını bildirir.
        /// D=AxW matrislerinin çarpımı sonucu oluşur.
        /// </summary>
        public void Dmatrisi()
        {

            for (int i = 0; i < boyut; i++)
            {
                kr.dmatrisi[i, 0] = 0;
                for (int k = 0; k < boyut; k++)
                    kr.dmatrisi[i, 0] += kr.Amatris[i, k] * kr.wmatrisi[k, 0];
            }
        }
        /// <summary>
        /// E=D/W işlemi sonucu oluşur.
        /// </summary>
        public void Ematrisi()
        {
            for (int i = 0; i < boyut; i++)
            {
                kr.ematrisi[i, 0] = kr.dmatrisi[i, 0] / kr.wmatrisi[i, 0];
            }
        }

        /// <summary>
        /// Karar noktalarındaki sonuç dağılımının bulunması
        /// n adet mx1 boyutlu S sütun vektörlerinin birleşmesiyle oluşan mxn boyutlu K karar matrisi
        /// </summary>

        public void Kmatrisi(int sayac, KararMatrisi k)
        {

            if (sayac != 0)
            {
                if (sayac > 7)
                    sayac = sayac - 7;
                for (int i = sayac - 1; i < sayac; i++)
                {
                    for (int j = 0; j < boyut; j++)
                    {
                        k.Kmatris[j, i] = kr.wmatrisi[j, 0];
                        //_Kmatrisi[j, i] = kr.wmatrisi[j, 0];

                    }
                }

            }

        }

        /// <summary>
        /// D, E matrisleri oluşturulduktan sonra lamda, CI ve CR değerleri hesaplanarak tutarlılık değeri bulunur.
        /// Lamda E matrisinin tüm verilerinin toplamı sonucu bulunur.
        /// CI= lamda-faktörsayısı/faktörsayısı-1, formülü kullanılarak tutarlılık göstergesi(CI) bulunur. 
        /// CR=CI/RI formülü ile tutarlılık degeri(CR) bulunur.
        /// RI (random gösterge) faktör sayısına göre sabit bir degere sahiptir. Bu deger kullanılarak hesaplama yapılır.
        /// örn:6 faktörlü bir problem  için RI degeri 1,24 tür.
        /// </summary>
        public void tutarlılık()
        {
            
            for (int i = 0; i < boyut; i++)
            {
                kr.lamda += kr.ematrisi[i, 0];

            }
            kr.lamda = kr.lamda / boyut;
            kr.CI = (kr.lamda - boyut) / (boyut - 1);
            kr.CR1 = kr.CI/ kr.RI[boyut];

        }

        public int yuzdeOnemAgirliklari(KararMatrisi karar, int sayac,TextBox t,params string[] items)
        { 
            int index = -1;
            kr.ilce = new string[3];
            ///sayac 6 olduğunda her faktör için hesaplama tamamlanmış olup karar matrisi doldurulmuştur
            ///sonuc vektörü hesaplanır.
            if (sayac ==6)
            {
                for (int i = 0; i < boyut; i++)
                {
                    kr.sonucMatrisi[i] = 0;
                    for (int k = 0; k < boyut; k++)
                        kr.sonucMatrisi[i] +=karar.Kmatris[i, k] * karar.W_Vektor[k, 0];
                }
                ///sonuçları ekrana yazdırmak için 
                for (int i = 0; i < boyut; i++)
                {
                      string result = string.Format("{0:0.0%}",
            kr.sonucMatrisi[i]);
                    t.Text += items[i] + " :" + result+"\r\n";
                }

                double secim = 0;
               
                ///sonuc matrisindeki en büyük yüzde önem derecesine ait index bulunur.
                for (int i = 0; i < boyut; i++)
                {
                      if(kr.sonucMatrisi[i]>secim)
                      {
                          secim = kr.sonucMatrisi[i];
                          index = i;
                      }

                }
                /////index seçimine göre hangi il olduğu belirlenmiş kabul edilir.İl sıralaması aşağıdaki şekildedir.
                /////Ankara,Antalya,İstanbul,İzmir,Konya
                /////indexe göre ilçe seçimi yapılır.
                //switch (index)
                //{
                //    case 0:
                //        kr.ilce = new string[] { "Çankaya", "Keçiören", "Etimesgut" };
                //        break;
                //    case 1:
                //        kr.ilce = new string[] { "Lara", "KonyaAltı", "Kepez" };
                //        break;
                //    case 2:
                //        kr.ilce = new string[] { "Beşiktaş", "Kadıköy", "Etiler" };
                //        break;
                //    case 3:
                //        kr.ilce = new string[] { "Karşıyaka", "Bornova", "Buca" };
                //        break;
                //    case 4:
                //        kr.ilce = new string[] { "Meram", "Karatay", "Selçuklu" };
                //        break;

                //}
            }
            return index;
        }
        
       
    }



}

