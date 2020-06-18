using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;//Bu satırda veritabanıyla ilgili işlem için eklememiz gereken kütüphane satırıdır.

/*   Altt görmüş oldüğunuz program bilgisayar programlama-II adlı dersin proje ödevidir.Burda form uygulaması
 *tabanında veritabanı(Microsoft Access) ilişkisel olarak anlık kullanıcıdan alınan verilerin bazı koşul
 * ve şartlar ile işlemler gerçekleşmiştir.
 * Umarım alttaki kod yapısının size katkısı olmuştur.Eğer kod yapımda hata bulursanız lütfen doğrusuyla beraber
 * beni bilgilendirirseniz sevinirim.
 * İyi Çalışmalar.
 * Hasan Hüseyin Aygar
 * 18.06.2020
 */
namespace Ogrenci_Bilgi_Sistemi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //Form kısmındaki butonlara eklemiş olduğum görseller "Properties" kısmından ekledim.
        //Formun üstteki iconunu  "Properties" kısmından ekledim.Eklerken ekliyeceğiniz görselin ".ico" uzantılı olmalıdır.Başka çözüm yolu muhakkak vardır ama ben ".ico" uzantılı görseli eklemeyi tercih ettim.
        //alttaki değişkenler global(genel) olarak tanımlanmış ki her buton tıklama işlemi için ayrı ayrı tekrardan tanımlamamak için.
        OleDbConnection baglanti = new OleDbConnection("Provider = Microsoft.ACE.OLEDB.12.0; Data Source = C:\\Users\\hasan\\Documents\\Ogrenci_Kayit.accdb");
        //Lütfen siz blgisayarınıza indirdiğiniz zaman  Microsoft Access olduğunu kontrol ediniz ve veritabanı dosyası nerde ise konumunu düzeltiniz.
        //Üstteki kod yapısında ilişki kuracağımız veri tabanının konumu belirleyip "baglanti" adında değişkene atadık.
        OleDbCommand komut; //Bu tanımlamada ise sql kod yapısını kullanmak için tanımladık.
        OleDbDataAdapter adtr; //Burda ise kullanıcıdan aldığımız işlem ve verileri dataset aracılığı ile veritabanına aktarmak için tanımladık.
        DataTable table = new DataTable();//Bu satırda ise kullanıcıdan alınıcak bilgileri veri tabanını taşırkenki aracı tablo olarak tanımladık.

        private void Ekle_Click(object sender, EventArgs e)
        {
            //Alt alta görmüş olduğunuz if else yapısında ise bazı alınması şart olan bilgileri kullanıcı daha veritabanına kontrol ettirmeden biz kontrol edip duruma göre bilgilendiriyoruz ve gereken müdahele dataset kısmına geçmeden kod yapsındaki şarta bağlı olarak nasıl isteniyorsa öyle bir veri girişi oluyor.
            if (textBox1.TextLength < 11)
            {
                MessageBox.Show("T.C. kimlik numarası 11 haneli olur.Lütfen 11 haneli sayısal veri giriniz.", "Uyarı!");
                textBox1.Text = "";
            }
            else
            {
                if (textBox3.TextLength < 6)
                {
                    MessageBox.Show("Lütfen 6 haneli (gün-ay-yıl örn:160520)olarak giriniz.", "Uyarı!");
                    textBox3.Text = "";
                }
                else {
                    if (textBox4.TextLength < 11)
                    {
                        MessageBox.Show("Telefon numarası 11 hanelidir(Örn:05554443322).", "Uyarı!");
                        textBox4.Text = "";
                    }
                    else {


                        baglanti.Open();//Bu satırda ise tanımladığımız adresteki veritabanını açtık.
                        komut = new OleDbCommand("INSERT INTO Kisisel_Bilgiler (TCKimlik,İsimSoyisim,DogumTarihi,TelefonNo,Bölüm) VALUES('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "')", baglanti);
                        //Üstteki kod yapısından "oleDbCommand()" yapsının içineki sql kodu ile ekleme işlemi yazıp textboxdaki verileri ilişkisel olarak veritabanı sütunlarına eşleştirdik.
                        komut.ExecuteNonQuery();//Bu satırda ise üstteki "komut" adlı değişkendeki sql kod yapısını "ExecuteNonQuery()" sayesinde çalıştırmak için.
                        baglanti.Close();//Bu satırda ise  "baglanti" adlı değişkende açık olan veritabanını kapattık. 
                        listele();//Bu satırda ise "listele()" sayesinde anlık değişikleri listeledik.
                        MessageBox.Show("Tebrikler ekleme işlemi başarıyla tamamladınız.", "Ekleme");
                        for (int a = 0; a < Controls.Count; a++) //Burdaki döngüde ise textbox ların içini temizlemek içindir.
                        {
                            if (Controls[a] is TextBox)
                            {

                                Controls[a].Text = "";
                            }


                        }
                    }
                }


            } 


        }





     







private void listele() {

            table.Clear();//Burda ise "table" yapısını temizlemiş olduk çünkü sürekli aynı verileri alt alta yazdırmamak için.
            baglanti.Open();//Bu satırda ise tanımladığımız adresteki veritabanını açtık.
            komut = new OleDbCommand("select * from Kisisel_Bilgiler", baglanti);
            //Üstteki kod yapısından "oleDbCommand()" yapısının içindeki sql kodu ile baglanti adlı veritabanında "Kisisel_Bilgiler" adlı tablonun tüm kayıtlarını göstermiş olduk.
            adtr.Fill(table); //Burda ise dataset aracılığı ile gösterilicek tabloyu "table" adlı aracı tabloya atadık.
            dataGridView1.DataSource = table; //Tabloyu göstereceğimiz  "dataGridView1" yapının içine "table" daki tabloyu aktardık.
            baglanti.Close();//Bu satırda ise  "baglanti" adlı değişkende açık olan veritabanını kapattık.


        }

        private void Sil_Click(object sender, EventArgs e)
        {
            baglanti.Open();//Bu satırda ise tanımladığımız adresteki veritabanını açtık.
            komut = new OleDbCommand("DELETE * FROM Kisisel_Bilgiler WHERE TCKimlik='" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'", baglanti);
            //Üstteki kod yapısından "oleDbCommand()" yapısının içindeki sql kodu ile "dataGridView1" yapısında seçili olan verinin  "baglanti" yapısındaki veritabanında "Kisisel_Bilgiler" adlı tablosundaki tüm kayıtlarını silmiş olduk.
            komut.ExecuteNonQuery();//Bu satırda ise üstteki "komut" adlı değişkendeki sql kod yapısını "ExecuteNonQuery()" sayesinde çalıştırmak için.
            baglanti.Close();//Bu satırda ise  "baglanti" adlı değişkende açık olan veritabanını kapattık.
            listele();//Bu satırda ise "listele()" sayesinde anlık değişikleri listeledik.
            MessageBox.Show("Silme işlemi başarlıyla tamamladınız.","Silme");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listele();//Form yüklenirken "listele()" yapısıyla ekrana tabloyu listelenmiştir.
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //Alltaki kod yapısıyla "dataGridView1" işaretli veriye çift tıklayarak ilişkisel olarak textbox lara yazdırdık.
            textBox1.Text = dataGridView1.CurrentRow.Cells["TCKimlik"].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells["İsimSoyisim"].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells["DogumTarihi"].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells["TelefonNo"].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells["Bölüm"].Value.ToString();
        }

        private void Güncelle_Click(object sender, EventArgs e)
        {
            //Alt alta görmüş olduğunuz if else yapısında ise bazı alınması şart olan bilgileri kullanıcı daha veritabanına kontrol ettirmeden biz kontrol edip duruma göre bilgilendiriyoruz ve gereken müdahele dataset kısmına geçmeden kod yapsındaki şarta bağlı olarak nasıl isteniyorsa öyle bir veri girişi oluyor.
            if (textBox1.TextLength < 11)
            {
                MessageBox.Show("T.C. kimlik numarası 11 haneli olur.Lütfen 11 haneli sayısal veri giriniz.", "Uyarı!");
                textBox1.Text = "";
            }
            else
            {
                if (textBox3.TextLength < 6)
                {
                    MessageBox.Show("Lütfen 6 haneli (gün-ay-yıl örn:160520)olarak giriniz.", "Uyarı!");
                    textBox3.Text = "";
                }
                else
                {
                    if (textBox4.TextLength < 11)
                    {
                        MessageBox.Show("Telefon numarası 11 hanelidir(Örn:05554443322).", "Uyarı!");
                        textBox4.Text = "";
                    }
                    else
                    {


                        baglanti.Open(); //Bu satırda ise tanımladığımız adresteki veritabanını açtık.
                        komut = new OleDbCommand("UPDATE Kisisel_Bilgiler SET İsimSoyisim='" + textBox2.Text + "',DogumTarihi='" + textBox3.Text + "',TelefonNo='" + textBox4.Text + "',Bölüm='" + textBox5.Text + "' where TCKimlik='" + textBox1.Text + "'", baglanti);
                        //Üstteki kod yapısından "oleDbCommand()" yapısının içindeki sql kodu ile "dataGridView1" yapısında seçili olan verinin  "baglanti" yapısındaki veritabanında "Kisisel_Bilgiler" adlı tablosundaki tüm kayıtlarını "TCKimlik" kısmını şartlarndırarak güncellemiş olduk.
                        //T.C. kimlik no güncellenemeyen bir veridir.Veritabanında primary key(birincil anahtar) olarak tanımlıdır.
                        komut.ExecuteNonQuery();//Bu satırda ise üstteki "komut" adlı değişkendeki sql kod yapısını "ExecuteNonQuery()" sayesinde çalıştırmak için.
                         baglanti.Close();//Bu satırda ise  "baglanti" adlı değişkende açık olan veritabanını kapattık.
                        listele();//Bu satırda ise "listele()" sayesinde anlık değişikleri listeledik.
                        MessageBox.Show("Tebrikler güncelleme işlemi başarıyla tamamladınız.", "Güncelleme");
                        for (int a = 0; a < Controls.Count; a++)
                        {
                            if (Controls[a] is TextBox)
                            {

                                Controls[a].Text = "";
                            }


                        }

                    }
                }


            }
        }
        //Alttaki kodlarda ise belirli textboxlarda(T.C.KİMLİK,TELEFON VE DOĞUM TARİHİ) veri girişlerinde sadece sayı girmesini sağladık.
        //Klavyeden girerken sağ tarafınızdaki sayılar basmanıza rağmen sayı girişi olmuyorsa sayı tuşlarınızın çalıştınızı kontrol ediniz.Eğer tuşlar çalışıyorsa lütfen bilgisayarınızdaki "num lock" tuşunuzun aktifliğini kontrol ediniz.
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}