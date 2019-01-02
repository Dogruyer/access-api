using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BartexAccess.Controllers
{
    public class IslemController : Controller
    {
        //string connect = @"Provider=Microsoft.Jet.OleDb.4.0;Data Source=\Inetpub\vhosts\testdogruyer.duckdns.org\httpdocs\bartex_aktarma1.mdb";
        //string connect = @"Provider=Microsoft.Jet.OleDb.4.0;Data Source=\Inetpub\vhosts\7houseburger.com\demo\bartex_aktarma1.mdb";
        string connect = @"Provider=Microsoft.Jet.OleDb.4.0;Data Source=C:\Users\Dogruyer_5\Desktop\bartex_aktarma1.mdb";
        string connectionString = @"Provider=Microsoft.Jet.OleDb.4.0;Data Source=C:\Users\Dogruyer_5\Desktop\Tiger.mdb";
        DataTable dt = new DataTable();




        #region DesenDijital
        //DESEN DİJİTAL
        [Route("DesenDigital/SipNo/{id}")]
        public ActionResult DesenDigitalSipNo(string id)
        {

            if (id != "")
            {


                var cevirID = id.Replace("-", "/");
                var tsql = "SELECT Mililitre From Desen_Digital Where SiparisNo =" + "'" + cevirID + "'" + " ";
                using (var conn = new OleDbConnection(connect))
                {
                    var cmd = new OleDbCommand(tsql, conn);
                    var da = new OleDbDataAdapter(cmd);
                    da.Fill(dt);
                }
                islem.LogEkle(dt);


            }
            string xml = System.IO.File.ReadAllText(Server.MapPath("~/kartno.xml"));
            return Content(xml, "xml");
        }
        #endregion

        #region Giriş Tablo
        // GİRİŞ TABLOSU
        [Route("Giris/PartiNo/{id}")]
        public ActionResult GirisPartiNo(string id)
        {
            if (id != "")
            {
                //,[Sipariş No] as SipNo,[Çeken Personel] as CekenPersonel,Kimlik,Örgü as Orgu,Dokuma,Kod1,Kod2,Alfa1,Alfa2,GGG
                var cevirID = id.Replace("-", "/");
                var tsql = "SELECT StokCinsi,[Parti No],KumasCesidiKodu,En,Gramaj,[Ö/D] as [OD],[Stok Adı] as StokAdi,OlcuBirimi,AmbarNo,[İrsaliye No] as IrsaliyeNo,[İrsaliye Tarihi] as IrsaliyeTarihi,Miktar,DovizBirimi,Fiyatı as Fiyati,Tarih,[Sipariş No] as SipNo,[Çeken Personel] as CekenPersonel,Kimlik,Örgü as Orgu,Dokuma,Kod1,Kod2,Alfa1,Alfa2,GGG  From Giriş Where [Parti No] =" + "'" + cevirID + "'" + " ";
                using (var conn = new OleDbConnection(connect))
                {
                    var cmd = new OleDbCommand(tsql, conn);
                    var da = new OleDbDataAdapter(cmd);
                    da.Fill(dt);
                }

                islem.LogEkle(dt);

            }
            string xml = System.IO.File.ReadAllText(Server.MapPath("~/kartno.xml"));
            return Content(xml, "xml");

        }
        #endregion

        #region GirişFiyatlar
        //GİRİŞ FİYATLAR
        [Route("GirisFiyatlar/PartiNo/{id}")]
        public ActionResult GirisFiyatlar(string id)
        {
            if (id != "")
            {

                var cevirID = id.Replace("-", "/");
                var tsql = "SELECT * From GirisFiyatlar Where PartiNo =" + "'" + cevirID + "'" + " ";
                using (var conn = new OleDbConnection(connect))
                {
                    var cmd = new OleDbCommand(tsql, conn);
                    var da = new OleDbDataAdapter(cmd);
                    da.Fill(dt);
                }

                islem.LogEkle(dt);

            }
            string xml = System.IO.File.ReadAllText(Server.MapPath("~/kartno.xml"));
            return Content(xml, "xml");

        }

        #endregion

        #region GMM
        //GMM Tablosundaki ' Sipariş No'ya göre verileri getirme
        [Route("GMMTablo/SipNo/{id}")]
        public ActionResult GMMSipNo(string id)
        {
            if (id != "")
            {

                var cevirID = id.Replace("-", "/");
                var tsql = "SELECT Kısım as Kisim , Makina , KartNo,Sipariş as SiparisNo , Miktar,Gün as Gun,Vardiya, Baslama , An , Personel , SiraNo,MakinaNo,pri,En,Gramaj From dbo_GMMTablo Where Sipariş =" + "'" + cevirID + "'" + " ";
                using (var conn = new OleDbConnection(connect))
                {
                    var cmd = new OleDbCommand(tsql, conn);
                    var da = new OleDbDataAdapter(cmd);
                    da.Fill(dt);
                }

                islem.LogEkle(dt);

            }
            string xml = System.IO.File.ReadAllText(Server.MapPath("~/kartno.xml"));
            return Content(xml, "xml");

        }

        #endregion

        #region  Malzeme Hareketi
        //MALZEME HAREKETİ
        [Route("MalzemeHareketi/Adi/{encodingType}")]
        public ActionResult MalzemeHareketiAdi(string encodingType)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(encodingType);
            string deger = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);


            //var cevirID = .Replace("_", " ");
            var tsql = "SELECT [Lot No], [Fiş Numarası] as FisNumarasi, FirmaAdi, [Kayıt Tarihi], Grubu, Adı as Adi, [Depoya Giren Miktar], [Çekilen Miktar] as CekilenMiktar, [Maliyet Merkezi], [Çeken Personel] as CekenPersonel, [Geliş Birim Fiyatı] as GelisBirimFiyati,[Toplam Fiyat], Açıklama as Acıklama, [Döviz Cinsi] as DovizCinsi, DM, ABD,SW  From [dbo_Malzeme Hareketi] Where Adı =" + "'" + deger + "'" + " ";
            using (var conn = new OleDbConnection(connect))
            {
                var cmd = new OleDbCommand(tsql, conn);
                var da = new OleDbDataAdapter(cmd);
                da.Fill(dt);
            }

            islem.LogEkle(dt);


            string xml = System.IO.File.ReadAllText(Server.MapPath("~/kartno.xml"));
            return Content(xml, "xml");
        }

        #endregion

        #region Malzeme Hareketi Stok İşlemleri

        [Route("Stok/Adi/{encodingType}/{tarih}")]
        public ActionResult StokAdi(string encodingType, string tarih)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(encodingType);
            string deger = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);

            var basTarihCevir = tarih.Replace("-", "/");
            //var cevirID = .Replace("_", " ");
            // 31 Aralık 2017 ' ye kadar olan stok Ambar 
            var tsql = "SELECT (Sum([Depoya Giren Miktar]) - Sum([Çekilen Miktar])) as StokAmbar From [dbo_Malzeme Hareketi]  Where Adı =" + "'" + deger + "' AND [Tarih] <= #" + basTarihCevir + "#  Group By Adı ";
            using (var conn = new OleDbConnection(connect))
            {
                var cmd = new OleDbCommand(tsql, conn);
                var da = new OleDbDataAdapter(cmd);
                da.Fill(dt);
            }

            islem.LogEkle(dt);


            string xml = System.IO.File.ReadAllText(Server.MapPath("~/kartno.xml"));
            return Content(xml, "xml");
        }

        [Route("StokDoviz/{sayi}/{encodingType}/{tarih}")]
        public ActionResult StokDovizAdi(int sayi, string encodingType, string tarih)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(encodingType);
            string deger = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);

            var basTarihCevir = tarih.Replace("-", "/");
            //var cevirID = .Replace("_", " ");
            // 31 Aralık 2017 ' ye kadar olan stok Ambar 
            var tsql = "SELECT TOP " + sayi + " [Döviz Cinsi] as DovizCinsi from [dbo_Malzeme Hareketi]  Where Adı =" + "'" + deger + "' AND [Depoya Giren Miktar] > 0 AND [Tarih] <= #" + basTarihCevir + "# Order By [Tarih] DESC";
            using (var conn = new OleDbConnection(connect))
            {
                var cmd = new OleDbCommand(tsql, conn);
                var da = new OleDbDataAdapter(cmd);
                da.Fill(dt);
            }

            islem.LogEkle(dt);


            string xml = System.IO.File.ReadAllText(Server.MapPath("~/kartno.xml"));
            return Content(xml, "xml");
        }

        [Route("StokGiren/Adi/{sayi}/{encodingType}/{tarih}")]
        public ActionResult StokGirenAdi(int sayi, string encodingType, string tarih)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(encodingType);
            string deger = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);

            var basTarihCevir = tarih.Replace("-", "/");
            //var cevirID = .Replace("_", " ");
            // 31 Aralık 2017 ' ye kadar olan stok Ambar 
            var tsql = "SELECT TOP " + sayi + " [Depoya Giren Miktar] as StokAmbar From [dbo_Malzeme Hareketi]   Where Adı =" + "'" + deger + "' AND [Depoya Giren Miktar] > 0 AND [Tarih] <= #" + basTarihCevir + "# Order By [Tarih] DESC";
            using (var conn = new OleDbConnection(connect))
            {
                var cmd = new OleDbCommand(tsql, conn);
                var da = new OleDbDataAdapter(cmd);
                da.Fill(dt);
            }

            islem.LogEkle(dt);


            string xml = System.IO.File.ReadAllText(Server.MapPath("~/kartno.xml"));
            return Content(xml, "xml");
        }

        [Route("StokBirimFiyat/{sayi}/{encodingType}/{tarih}")]
        public ActionResult StokBirimFiyatAdi(int sayi, string encodingType, string tarih)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(encodingType);
            string deger = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);

            var basTarihCevir = tarih.Replace("-", "/");
            //var cevirID = .Replace("_", " ");
            // 31 Aralık 2017 ' ye kadar olan stok Ambar 
            var tsql = "SELECT TOP " + sayi + " [Geliş Birim Fiyatı] as GelisBirimFiyat from [dbo_Malzeme Hareketi]   Where Adı =" + "'" + deger + "' AND [Depoya Giren Miktar] > 0 AND [Tarih] <= #" + basTarihCevir + "# Order By [Tarih] DESC";
            using (var conn = new OleDbConnection(connect))
            {
                var cmd = new OleDbCommand(tsql, conn);
                var da = new OleDbDataAdapter(cmd);
                da.Fill(dt);
            }

            islem.LogEkle(dt);


            string xml = System.IO.File.ReadAllText(Server.MapPath("~/kartno.xml"));
            return Content(xml, "xml");
        }


        [Route("StokTarih/{sayi}/{encodingType}/{tarih}")]
        public ActionResult StokTarihAdi(int sayi, string encodingType, string tarih)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(encodingType);
            string deger = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);

            var basTarihCevir = tarih.Replace("-", "/");
            //var cevirID = .Replace("_", " ");
            // 31 Aralık 2017 ' ye kadar olan stok Ambar 
            var tsql = "SELECT TOP " + sayi + " Tarih From [dbo_Malzeme Hareketi]   Where Adı =" + "'" + deger + "' AND [Depoya Giren Miktar] > 0 AND [Tarih] <= #" + basTarihCevir + "# Order By [Tarih] DESC";
            using (var conn = new OleDbConnection(connect))
            {
                var cmd = new OleDbCommand(tsql, conn);
                var da = new OleDbDataAdapter(cmd);
                da.Fill(dt);
            }

            islem.LogEkle(dt);


            string xml = System.IO.File.ReadAllText(Server.MapPath("~/kartno.xml"));
            return Content(xml, "xml");
        }

        [Route("StokToplamGiren/Adi/{encodingType}/{tarih}")]
        public ActionResult StokToplamGirenAdi(string encodingType, string tarih)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(encodingType);
            string deger = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);

            var basTarihCevir = tarih.Replace("-", "/");
            //var cevirID = .Replace("_", " ");
            // 31 Aralık 2017 ' ye kadar olan stok Ambar 
            var tsql = "SELECT SUM([Depoya Giren Miktar]) as ToplamDepoyaGiren From [dbo_Malzeme Hareketi]   Where Adı =" + "'" + deger + "' AND [Depoya Giren Miktar] > 0 AND [Tarih] <= #" + basTarihCevir + "# Group By Adı";
            using (var conn = new OleDbConnection(connect))
            {
                var cmd = new OleDbCommand(tsql, conn);
                var da = new OleDbDataAdapter(cmd);
                da.Fill(dt);
            }

            islem.LogEkle(dt);


            string xml = System.IO.File.ReadAllText(Server.MapPath("~/kartno.xml"));
            return Content(xml, "xml");
        }




        #endregion

        #region StoklarTablosu

        [Route("StokAdKod/{encodingType}")]
        public ActionResult Adi(string encodingType)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(encodingType);
            string deger = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);


            //var cevirID = .Replace("_", " ");
            // 31 Aralık 2017 ' ye kadar olan stok Ambar 
            var tsql = "SELECT Kodu From Stoklar   Where Adı =" + "'" + deger + "' ";
            using (var conn = new OleDbConnection(connect))
            {
                var cmd = new OleDbCommand(tsql, conn);
                var da = new OleDbDataAdapter(cmd);
                da.Fill(dt);
            }

            islem.LogEkle(dt);


            string xml = System.IO.File.ReadAllText(Server.MapPath("~/kartno.xml"));
            return Content(xml, "xml");
        }




        #endregion

        #region Ops
        [Route("Ops/SipNo/{id}")]
        public ActionResult OpSipNo(string id)
        {
            if (id != null)
            {
                using (var con = new OleDbConnection(connect))
                {
                    var cevirID = id.Replace("-", "/");
                    var tsql = "SELECT  OpSiraNo, Operasyon, Recete From dbo_Ops Where SipNo =" + "'" + cevirID + "'" + " ";
                    var command = new OleDbCommand(tsql, con);
                    var da = new OleDbDataAdapter(command);
                    da.Fill(dt);
                    islem.LogEkle(dt);
                }
            }
            string xml = System.IO.File.ReadAllText(Server.MapPath("~/kartno.xml"));
            return Content(xml, "xml");
        }

        #endregion

        #region PATREÇETE
        //PAT RECETE PAT
        [Route("PatRecete/Pat/{isim}")]
        public ActionResult Pat(string isim)
        {
            if (isim != "")
            {
                var base64EncodedBytes = Convert.FromBase64String(isim);
                string deger = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
                //var cevirID = isim.Replace("-", " ");
                var tsql = "SELECT * From PAT_RECETE Where PAT = '" + deger + "'";
                using (var conn = new OleDbConnection(connect))
                {
                    var cmd = new OleDbCommand(tsql, conn);
                    var da = new OleDbDataAdapter(cmd);
                    da.Fill(dt);
                }

                islem.LogEkle(dt);

            }
            string xml = System.IO.File.ReadAllText(Server.MapPath("~/kartno.xml"));
            return Content(xml, "xml");
        }

        //PAT REÇETE KOD
        [Route("PatReceteAdKod/{isim}")]
        public ActionResult PatKod(string isim)
        {
            if (isim != "")
            {
                var base64EncodedBytes = Convert.FromBase64String(isim);
                string deger = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
                //var cevirID = isim.Replace("-", " ");
                var tsql = "SELECT KOD From PAT_RECETE Where PAT = '" + deger + "'";
                using (var conn = new OleDbConnection(connect))
                {
                    var cmd = new OleDbCommand(tsql, conn);
                    var da = new OleDbDataAdapter(cmd);
                    da.Fill(dt);
                }

                islem.LogEkle(dt);

            }
            string xml = System.IO.File.ReadAllText(Server.MapPath("~/kartno.xml"));
            return Content(xml, "xml");
        }
        #endregion

        #region Planlar
        //PLANLAR SİP NO
        [Route("Planlar/SipNo/{id}")]
        public ActionResult PlanlarSipNo(string id)
        {

            if (id != "")
            {


                var cevirID = id.Replace("-", "/");
                var tsql = "SELECT Kartno,SipNo, ÇalışılacakMetraj as CalisilacakMetraj,İstenenEn as IstenenEn,[Termin Tarihi],İsletmeTarih as IsletmeTarih From Planlar Where SipNo =" + "'" + cevirID + "'" + " ";
                using (var conn = new OleDbConnection(connect))
                {
                    var cmd = new OleDbCommand(tsql, conn);
                    var da = new OleDbDataAdapter(cmd);
                    da.Fill(dt);
                }
                islem.LogEkle(dt);


            }
            string xml = System.IO.File.ReadAllText(Server.MapPath("~/kartno.xml"));
            return Content(xml, "xml");
        }



        [Route("Planlar/ToplamSiparis")]
        public ActionResult ToplamSayi()
        {

            var tsql = "SELECT COUNT(SipNo) as SiparisAdedi From (select distinct SipNo from Planlar)";
            using (var conn = new OleDbConnection(connect))
            {
                var cmd = new OleDbCommand(tsql, conn);
                var da = new OleDbDataAdapter(cmd);
                da.Fill(dt);
            }
            islem.LogEkle(dt);



            string xml = System.IO.File.ReadAllText(Server.MapPath("~/kartno.xml"));
            return Content(xml, "xml");
        }

      
        [Route("Planlar/ToplamKart")]
        public ActionResult ToplamKart()
        {

            var tsql = "SELECT COUNT(KartNo) as SiparisAdedi From (select distinct KartNo from Planlar)";
            using (var conn = new OleDbConnection(connect))
            {
                var cmd = new OleDbCommand(tsql, conn);
                var da = new OleDbDataAdapter(cmd);
                da.Fill(dt);
            }
            islem.LogEkle(dt);



            string xml = System.IO.File.ReadAllText(Server.MapPath("~/kartno.xml"));
            return Content(xml, "xml");
        }

        [Route("PlanlarSipKartNo/{id}")]
        public ActionResult KartNo(string id)
        {

            if (id != "")
            {


                var cevirID = id.Replace("-", "/");
                var tsql = "SELECT Kartno From Planlar Where SipNo =" + "'" + cevirID + "'" + " ";
                using (var conn = new OleDbConnection(connect))
                {
                    var cmd = new OleDbCommand(tsql, conn);
                    var da = new OleDbDataAdapter(cmd);
                    da.Fill(dt);
                }
                islem.LogEkle(dt);


            }
            string xml = System.IO.File.ReadAllText(Server.MapPath("~/kartno.xml"));
            return Content(xml, "xml");
        }
        #endregion

        #region Prg
        [Route("Prg/SipNo/{id}")]
        public ActionResult PrgSipNo(string id)
        {
            if (id != null)
            {
                using (var con = new OleDbConnection(connect))
                {
                    var cevirID = id.Replace("-", "/");
                    var tsql = "SELECT Tarih, SipNo, İştar as Istar, Yazılış as Yazilis From dbo_Prg Where SipNo =" + "'" + cevirID + "'" + " ";
                    var command = new OleDbCommand(tsql, con);
                    var da = new OleDbDataAdapter(command);
                    da.Fill(dt);
                    islem.LogEkle(dt);
                }
            }
            string xml = System.IO.File.ReadAllText(Server.MapPath("~/kartno.xml"));
            return Content(xml, "xml");
        }

        #endregion

        #region Reçete

        [Route("Recete/ReceteNo/{sorgu}")]
        public ActionResult ReceteNo(string encodingType)
        {
            //var bas = sorgu.Split('-')[0];
            //var bit = sorgu.Split('-')[1];

            var base64EncodedBytes = System.Convert.FromBase64String(encodingType);
            string deger = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);

            var tsql = "SELECT Adi,SUM(Miktar) From Reçete WHERE ReceteNo='" + deger + "' GROUP BY Adi";
            using (var conn = new OleDbConnection(connect))
            {
                var cmd = new OleDbCommand(tsql, conn);
                var da = new OleDbDataAdapter(cmd);
                da.Fill(dt);
            }
            islem.LogEkle(dt);

            string xml = System.IO.File.ReadAllText(Server.MapPath("~/kartno.xml"));
            return Content(xml, "text/xml");
        }


        #endregion

        #region UretimReçete

        [Route("UretimRecete/SipNo/{deger}")]
        public ActionResult UretimReceteSipNo(string deger)
        {


            var cevirID = deger.Replace("-", "/");
            var tsql = "SELECT DISTINCT ReceteNo,Tarih,Miktar From UretimRecete Where SiparisNo =" + "'" + cevirID + "'" + " ";
            using (var conn = new OleDbConnection(connect))
            {
                var cmd = new OleDbCommand(tsql, conn);
                var da = new OleDbDataAdapter(cmd);
                da.Fill(dt);
            }

            islem.LogEkle(dt);


            string xml = System.IO.File.ReadAllText(Server.MapPath("~/kartno.xml"));
            return Content(xml, "xml");

        }




        #endregion

        #region UretimReçeteDesen

        [Route("ReceteDesen/SipNo/{id}")]
        public ActionResult ReceteDesenSipNo(string id)
        {
            if (id != "")
            {

                var cevirID = id.Replace("-", "/");
                var tsql = "SELECT * From dbo_UretimReceteDesen Where SiparisNo =" + "'" + cevirID + "'" + " ";
                using (var conn = new OleDbConnection(connect))
                {
                    var cmd = new OleDbCommand(tsql, conn);
                    var da = new OleDbDataAdapter(cmd);
                    da.Fill(dt);
                }

                islem.LogEkle(dt);

            }
            string xml = System.IO.File.ReadAllText(Server.MapPath("~/kartno.xml"));
            return Content(xml, "xml");

        }

        #endregion

        #region Tablo Sipariş Föyü


        [Route("SiparisFoyu/SipNo/{id}")]
        public ActionResult SiparisFoyuSipNo(string id)
        {
            if (id != "")
            {
                //,[Müşteri Ünvanı] as MusteriUnvani,Metraj,Birim,[Parti No] as PartiNo,D,ÖR as OR,[Kumaş Cinsi] as KumasCinsi,En,Gramaj,[Gr/Mtül] as GrMtul,Sanfor,Zımpara as Zimpara,[Şardon Tek Yüz] as SardonTekYuz,[Şardon Çift Yüz] as SardonCiftYuz
                var cevirID = id.Replace("-", "/");
                var tsql = "SELECT [Sipariş No] as SipNo , [Sipariş Tarihi]  as SipTarih,HazırlıkSip as HazirlikSip,[Müşteri Ünvanı] as MusteriUnvani,Metraj,Birim,[Parti No] as PartiNo,D,[ÖR] as ORR,[Kumaş Cinsi] as KumasCinsi,En,Gramaj,[Gr/Mtül] as GrMtul,Sanfor,Zımpara as Zimpara,[Şardon Tek Yüz] as SardonTekYuz,[Şardon Çift Yüz] as SardonCiftYuz,[Krinkıl-] as Krinkil,Diğer as Diger,Varyant,[İpek Apre] as IpekApre,[Su İtici Apre] as SuIticiApre,[Teflon Apre],[Dolgun Apre],[Yanmaz Apre],[Buruşmaz Apre] as BurusmazApre,[Kalendı-] as Kalendi,[Ram-],Pigment,[Pigment Fonlu],Reaktif,[Reaktif Fonlu],Ronjan,B,A,O,K,Ö as O,KKL,PPV,AB,BH,BAH,KİÜ as KIU,ÇT as CT,YY,MW,YB,[Anlaşmalı Fiyat] as AnlasmaliFiyat,[Döviz Cinsi] as DovizCinsi,[Özel Anlaşma] as OzelAnlasma,[F/S] as FS,L,[Kayıt Tarihi] as KayitTarihi,Ödeme as Odeme,KayıtSaati as KayitSaati,Durumu,Kod1,Kod2,Kod3,Kod4,Kod5,Kod6,Kod7,Kod8,Alfa1,Alfa2,Num1,Num2,BAS1,BAS2,BAS3,B1,B2,B3,Fasonfiyat,Fasondoviz,Rsip,Gsip From TabloSiparişFöyü Where [Sipariş No] =" + "'" + cevirID + "'" + " ";

                using (var conn = new OleDbConnection(connect))
                {
                    var cmd = new OleDbCommand(tsql, conn);
                    var da = new OleDbDataAdapter(cmd);
                    da.Fill(dt);
                }

                islem.LogEkle(dt);

            }
            string xml = System.IO.File.ReadAllText(Server.MapPath("~/kartno.xml"));
            return Content(xml, "xml");

        }

        #endregion

        #region TIGER Veriler

        [Route("Tiger/{encodingType}/{tarih}")]
        public ActionResult TigerVeriDoviz(string encodingType, string tarih)
        {

            var base64EncodedBytes = System.Convert.FromBase64String(encodingType);
            string deger = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);

            using (var con = new OleDbConnection(connectionString))
            {
                //YIL- AY GÜN
                var tsql = "SELECT PRICE as BirimFiyat,CRR as DovizCinsi,PTYPE as Birim FROM Tiger WHERE CODE = '" + deger + "' AND PDATE LIKE '" + tarih + "%" + "' ";
                var command = new OleDbCommand(tsql, con);
                var da = new OleDbDataAdapter(command);
                da.Fill(dt);

            }
            islemTiger.LogEkle(dt);




            string xml = System.IO.File.ReadAllText(Server.MapPath("~/tiger.xml"));
            return Content(xml, "xml");
        }


        #endregion



    }
}