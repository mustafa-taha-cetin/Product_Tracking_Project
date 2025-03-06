using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Entitiy_Projesi
{
    public partial class FrmIstatistik : Form
    {
        public FrmIstatistik()
        {
            InitializeComponent();
        }

        Entitiy_ProjesiEntities db = new Entitiy_ProjesiEntities();

        private void FrmIstatistik_Load(object sender, EventArgs e)
        {
            // ----------------- Linq Sorguları ---------------
            // ( => işareti lamda gibi çalışıyor )
            lblToplamKategori.Text = db.Tbl_Kategoriler.Count().ToString();
            lblToplamUrun.Text = db.Tbl_Urunler.Count().ToString();
            lblAktifMusteri.Text = db.Tbl_Musteriler.Count(x=>x.Musteri_Durum == true).ToString();
            lblPasifMusteri.Text = db.Tbl_Musteriler.Count(x=>x.Musteri_Durum == false).ToString();
            lblBeyazEsya.Text = db.Tbl_Urunler.Count(x=>x.Urun_Kategori.Value ==1).ToString();
            lblToplamStok.Text = db.Tbl_Urunler.Sum(x=>x.Urun_Stok).ToString();
            lblKasa.Text = db.Tbl_Satıs.Sum(x=>x.Satıs_Fiyat).ToString() + " TL";
            lblEnYuksekFiyat.Text = (from x in  db.Tbl_Urunler orderby x.Urun_Fiyat descending select x.Urun_Ad).FirstOrDefault();
            lblEnDusukFiyat.Text = (from x in db.Tbl_Urunler orderby x.Urun_Fiyat ascending select x.Urun_Ad).FirstOrDefault();
            lblBuz.Text = db.Tbl_Urunler.Count(x=>x.Urun_Ad=="Buzdolabı").ToString();
            lblSehir.Text = (from x in db.Tbl_Musteriler select x.Musteri_Sehir).Distinct().Count().ToString();
            lblEnCokUrunluMarka.Text = db.MarkaGetir().FirstOrDefault();



        }
    }
}
