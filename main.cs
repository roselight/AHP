using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using System.Collections;
using System.Text.RegularExpressions;

namespace AHP
{
    public partial class main : DevExpress.XtraEditors.XtraForm
    {
        KararMatrisi k;
        public string[] items;
        int sayac ,nesne= 0;
        int kmatrisSayac = 0;
        public main()
        {
            InitializeComponent();
           
            if (sayac == 0)
            {
                items = new string[] { "Onkoloji Hastanesi Sayısı", "Yaşlı Nüfusu", "Sigara Kullanımı", 
                "Alkol Kullanımı", "Kötü Beslenme Alışkanlığı", "Hava Kirliliği" };
                sayac++;
            }
            checkedComboBoxEdit1.EditValueChanged += checkedComboBoxEdit1_EditValueChanged;
           
        }
        
        /// <summary>
        /// checkedComboBoxEdit'lerin tamamına aynı liste eklendi.
        /// checkedComboBoxEditlerin herhangi birinden seçim işlemi yapıldığında, diğer combobox içinde seçilen item ın görünmemesi sağlandı.
        /// Eğer seçilen item bırakılırsa tekrar combolara eklendi.
        /// </summary>
        /// <param name="sender">CheckedComboBoxlar aynı metoda bağlandı</param>
        public void setCb(object sender)
        {
            
            var edit = sender as CheckedComboBoxEdit;
            List<string> rm = new List<string>();
            foreach (var pb in this.Controls.OfType<CheckedComboBoxEdit>())
            {

                rm.Add(pb.Properties.GetCheckedItems().ToString());
                foreach (var item in items)
                {

                    if (pb.Properties.Items.IndexOf(item) < 0)
                    {
                        pb.Properties.Items.Add(item);
                    }

                }
            }

            foreach (var pb in this.Controls.OfType<CheckedComboBoxEdit>())
            {
                foreach (var item in rm)
                {
                    foreach (var itemSplit in item.Split(','))
                    {
                        {
                            if (pb.Properties.Items.Count > 0 && pb.Properties.Items.IndexOf(itemSplit.Trim()) > -1)
                            {
                                if (pb.Properties.Items[itemSplit.Trim()].CheckState == CheckState.Unchecked)
                                {
                                    pb.Properties.Items.RemoveAt(pb.Properties.Items.IndexOf(itemSplit.Trim()));
                                }

                            }
                        }
                    }

                }

            }

        }


   
        
        private void hesapla_Click(object sender, EventArgs e)
        {

            k = KararMatrisi.getNesne();
            double[] faktörler = new double[items.Length];
            string[] dizi=new string[2];
            var edit = sender as CheckedComboBoxEdit;

            foreach (var ch in this.Controls.OfType<CheckedComboBoxEdit>())
            {
                if (ch.Properties.Items.GetCheckedValues().Count > 0)
                {
                    foreach (string item in ch.Properties.Items.GetCheckedValues())
                    {
                        
                        dizi = ch.Name.Split('t');
                        if (item == "Onkoloji Hastanesi Sayısı")
                           faktörler[0]=(Convert.ToDouble(dizi[1]));
                        else if (item == "Yaşlı Nüfusu")
                            faktörler[1] = Convert.ToDouble(dizi[1]);
                        else if (item == "Sigara Kullanımı")
                            faktörler[2] = Convert.ToDouble(dizi[1]);
                        else if (item == "Alkol Kullanımı")
                            faktörler[3] = Convert.ToDouble(dizi[1]);
                        else if (item == "Kötü Beslenme Alışkanlığı")
                            faktörler[4] = Convert.ToDouble(dizi[1]);
                        else if (item == "Hava Kirliliği")
                            faktörler[5] = Convert.ToDouble(dizi[1]);
                        else if(item=="Ankara")
                            faktörler[0] = Convert.ToDouble(dizi[1]);
                        else if (item == "Antalya")
                            faktörler[1] = Convert.ToDouble(dizi[1]);
                        else if (item == "İstanbul")
                            faktörler[2] = Convert.ToDouble(dizi[1]);
                        else if (item == "İzmir")
                            faktörler[3] = Convert.ToDouble(dizi[1]);
                        else if (item == "Konya")
                            faktörler[4] = Convert.ToDouble(dizi[1]);

                       
                    }
                }
                

                    
            }
            //if (sayac != 0)
            //{
              
            //    items = new string[] { "Ankara", "Antalya", "İstanbul", "İzmir", "Konya" };
            //}
         
            //foreach (var cmb in this.Controls.OfType<CheckedComboBoxEdit>())
            //{

            //    cmb.Properties.Items.Clear();
                
            //    foreach (var item in items)
            //    {

            //        if (cmb.Properties.Items.IndexOf(item) < 0)
            //        {
            //            cmb.Properties.Items.Add(item);
            //        }

            //    }


            //}
            switch (sayac)
            {
                case 1:
                case 8:
                    {
                        lbltext.Text = "Onkoloji Hastanesi Sayısı Kriteri İçin Şehirler Arası Önem Dereceleri";
                        sayac++; break;
                    }
                case 2:
                case 9:
                    {
                        lbltext.Text = "Yaşlı Nüfusu Kriteri İçin Şehirler Arası Önem Dereceleri";
                        sayac++; break;
                    }
                case 3:
                case 10:
                    {
                        lbltext.Text = "Sigara Kullanımı Kriteri İçin Şehirler Arası Önem Dereceleri";
                        sayac++; break;
                    }
                case 4:
                case 11:

                    {
                        lbltext.Text = "Alkol Kullanımı Kriteri İçin Şehirler Arası Önem Dereceleri";
                        sayac++; break;
                    }
                case 5:
                case 12:
                    {
                        lbltext.Text = "Kötü Beslenme Alışkanlığı Kriteri İçin Şehirler Arası Önem Dereceleri";
                        sayac++; break;
                    }
                case 6 :
                case 13:
                    {
                        lbltext.Text = "Hava Kirliliği Kriteri İçin Şehirler Arası Önem Dereceleri";
                        sayac++; break;
                    }
                case 7:
                    {
                        lbltext.Text = "Kriterler Arası Önem Dereceleri";
                        sayac++;  break;
                    }
            }
            ///items.lenght ile kaç faktör gönderildiği bilgisi gidiyor, faktörler ile de giden faktörün karar vericiden aldığı önem derecesi 
            ///bilgisi gönderiliyor. Örn: Ankara : 9 önem derecesine sahipse faktörler[0] indisinde 9 değeri gönderilmiş olur.
            hesapla hs = new hesapla(faktörler);
            hs.Amatrisiolus();
            hs.yazdır(textBox1);
            hs.Cmatrisi();
            hs.Wmatrisi(k,kmatrisSayac); 
            hs.Dmatrisi();
           hs.Ematrisi();
            hs.tutarlılık();
          hs.Kmatrisi(kmatrisSayac,k);
        k.Veri= hs.yuzdeOnemAgirliklari(k, kmatrisSayac, txtsonuc, items);
            kmatrisSayac++;
           if (kmatrisSayac == 7)
               kmatrisSayac = 0;
           if (sayac == 7)
           { sda(k.Veri); }

          
        }
        public void sda(int say)
        {
             
            if (sayac>0 && sayac<8)
            {
                   
                    items = new string[] { "Ankara", "Antalya", "İstanbul", "İzmir", "Konya" };
                
                 
            }
            if(sayac>7)
            {
                if(sayac==8)
                {
                    items = new string[] { "Onkoloji Hastanesi Sayısı", "Yaşlı Nüfusu", "Sigara Kullanımı", 
                "Alkol Kullanımı", "Kötü Beslenme Alışkanlığı", "Hava Kirliliği" };
                }
                if(sayac>8)
                {
                    ///index seçimine göre hangi il olduğu belirlenmiş kabul edilir.İl sıralaması aşağıdaki şekildedir.
                    ///Ankara,Antalya,İstanbul,İzmir,Konya
                    ///indexe göre ilçe seçimi yapılır.
                    switch (say)
                    {
                        case 0:
                           items = new string[] { "Çankaya", "Keçiören", "Etimesgut" };
                            break;
                        case 1:
                           items = new string[] { "Lara", "KonyaAltı", "Kepez" };
                            break;
                        case 2:
                           items= new string[] { "Beşiktaş", "Kadıköy", "Etiler" };
                            break;
                        case 3:
                            items = new string[] { "Karşıyaka", "Bornova", "Buca" };
                            break;
                        case 4:
                            items = new string[] { "Meram", "Karatay", "Selçuklu" };
                            break;

                    }
                }
            }

            foreach (var cmb in this.Controls.OfType<CheckedComboBoxEdit>())
            {

                cmb.Properties.Items.Clear();

                foreach (var item in items)
                {

                    if (cmb.Properties.Items.IndexOf(item) < 0)
                    {
                        cmb.Properties.Items.Add(item);
                    }

                }


            }
        }
        private void main_Load(object sender, EventArgs e)
        {
           
           
        }

    
        /// <summary>
        /// checkedComboBoxEditlerin herhangi birine tıklandığında item ekleme, güncelleme işlemleri gerçekleşir.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkedComboBoxEdit1_EditValueChanged(object sender, EventArgs e)
        {
            setCb(sender);
        }

        private void checkedComboBoxEdit2_EditValueChanged(object sender, EventArgs e)
        {
            setCb(sender);
        }

        private void checkedComboBoxEdit3_EditValueChanged(object sender, EventArgs e)
        {
            setCb(sender);
        }

        private void checkedComboBoxEdit4_EditValueChanged(object sender, EventArgs e)
        {
            setCb(sender);
        }

        private void checkedComboBoxEdit5_EditValueChanged(object sender, EventArgs e)
        {
            setCb(sender);
        }

        private void checkedComboBoxEdit8_EditValueChanged(object sender, EventArgs e)
        {
            setCb(sender);
        }

        private void checkedComboBoxEdit6_EditValueChanged(object sender, EventArgs e)
        {
            setCb(sender);
        }

        private void checkedComboBoxEdit7_EditValueChanged(object sender, EventArgs e)
        {
            setCb(sender);
        }

        private void checkedComboBoxEdit9_EditValueChanged(object sender, EventArgs e)
        {
            setCb(sender);
        }

  
        

    }
}