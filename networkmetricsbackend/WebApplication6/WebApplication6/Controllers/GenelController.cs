using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Services;
using WebApplication6.Controllers;
using WebApplication6.Models;


namespace WebApplication6.Controllers
{
    public class GenelController : Controller
    {
        // GET: Genel
        public ActionResult Index()
        {
            sosyalmedyaEntities baglan = new sosyalmedyaEntities();

            var menu = (from x in baglan.bilgisayarlar select x.kayitID).ToList();

            ViewBag.menuAl = menu;



            var bilgisayarlar = (from x in baglan.bilgisayarlar select x).ToList();
            //Total Ram Hesaplama

            int totalRamToplam = 0;
            int bilgisayarSayisiToplam = bilgisayarlar.Count();
            int hddToplam = 0;
            int programSayisiToplam = 0;

            StringBuilder chart1 = new StringBuilder();
            StringBuilder chart2 = new StringBuilder();
            StringBuilder chart3 = new StringBuilder();
            StringBuilder chart4 = new StringBuilder();

            foreach (var x in bilgisayarlar)
            {
                int totalRamToplamAra = 0;
                int hddToplamAra = 0;
                int programSayisiToplamAra = 0;

                int bilgisayarIDs = Convert.ToInt32(x.kayitID);
                var durumCek = (from a in baglan.durumGecmisi where a.bilgisayarID == bilgisayarIDs orderby a.id descending select a).FirstOrDefault();


                chart1.Append("\""+ x.kayitID + "\"");
                chart1.Append(",");

                var diskKullanimOrani = ((durumCek.anlikKullanilanDiskSize*100) / durumCek.kullanilanTotalDiskSize);
                if (diskKullanimOrani == 0)
                {
                    diskKullanimOrani = 1;
                }
                chart2.Append(diskKullanimOrani);
                chart2.Append(",");

                var ramKullanimOrani = ((durumCek.anlikKullanilanRam*100) / durumCek.totalRam);
                if (ramKullanimOrani == 0)
                {
                    ramKullanimOrani = 1;
                }
                chart3.Append(diskKullanimOrani);
                chart3.Append(",");

                chart4.Append(durumCek.islemciKullanimi);
                chart4.Append(",");

                hddToplamAra = durumCek.kullanilanTotalDiskSize;
                hddToplam = hddToplamAra + hddToplam;

                totalRamToplamAra = durumCek.totalRam;
                totalRamToplam = totalRamToplamAra + totalRamToplam;
                
                programSayisiToplamAra = x.programlar.Split(',').Count();
                programSayisiToplam = programSayisiToplamAra + programSayisiToplam;

            }
            StringBuilder son = new StringBuilder();

            var aradeger = "";
            aradeger = Convert.ToString(chart1).TrimEnd(',');
            son.Append(aradeger);
            son.Append("&");

            aradeger = "";
            aradeger = Convert.ToString(chart2).TrimEnd(',');
            son.Append(aradeger);
            son.Append("&");

            aradeger = "";
            aradeger = Convert.ToString(chart3).TrimEnd(',');
            son.Append(aradeger);
            son.Append("&");

            aradeger = "";
            aradeger = Convert.ToString(chart4).TrimEnd(',');
            son.Append(aradeger);

            ViewBag.chartCiz = Convert.ToString(son);


            string al = bilgisayarSayisiToplam + "," + programSayisiToplam + "," + hddToplam + "," + totalRamToplam;
            ViewBag.istatistik = al;


            return View();
        }


        [HttpGet]
        public ActionResult bilgisayarGoruntule()
        {
            sosyalmedyaEntities baglan = new sosyalmedyaEntities();


            var menu = (from x in baglan.bilgisayarlar select x.kayitID).ToList();

            ViewBag.menuAl = menu;


            string kayitID = Request.QueryString["id"];

            int kontrol = Convert.ToInt32(kayitID);

            var bilgisayarBilgileri = (from x in baglan.bilgisayarlar join d in baglan.durumGecmisi on x.kayitID equals d.bilgisayarID where x.kayitID == kontrol orderby d.id descending select new{ x,d}).ToList();

            var anlikKullanilanDiskY = 0;
            var anlikKullanilanRamY = 0;
            var bilgisayarIDY = 0;
            var guncellenmeTarihiY = DateTime.Now;
            var islemciKullanimiY = 0;
            var kullanilanTotalDiskSizeY = 0;
            var totalRamY = 0;
            var eklenmeTarihiY = DateTime.Now;
            var isletimSistemiY = "";
            var programlarY = "";

            foreach (var a in bilgisayarBilgileri)
            {
                StringBuilder veriler = new StringBuilder();

                bilgisayarIDY = a.d.bilgisayarID;
                veriler.Append(bilgisayarIDY);
                veriler.Append("&");


                anlikKullanilanDiskY = a.d.anlikKullanilanDiskSize;
                veriler.Append(anlikKullanilanDiskY);
                veriler.Append(",");

                kullanilanTotalDiskSizeY = a.d.kullanilanTotalDiskSize;
                veriler.Append(kullanilanTotalDiskSizeY);
                veriler.Append("&");

                anlikKullanilanRamY = a.d.anlikKullanilanRam;
                veriler.Append(anlikKullanilanRamY);
                veriler.Append(",");

                totalRamY = a.d.totalRam;
                veriler.Append(totalRamY);
                veriler.Append("&");

                islemciKullanimiY = a.d.islemciKullanimi;
                veriler.Append(islemciKullanimiY);
                veriler.Append("&");

                isletimSistemiY = a.x.isletimSistemi;
                veriler.Append(isletimSistemiY);
                veriler.Append("&");

                programlarY = a.x.programlar;
                veriler.Append(programlarY);
                veriler.Append("&");



                guncellenmeTarihiY = a.d.guncellenmeTarihi;
                veriler.Append(guncellenmeTarihiY);
                veriler.Append("&");

                eklenmeTarihiY = a.x.eklenmeTarihi;
                veriler.Append(eklenmeTarihiY);

                ViewBag.verileriCiz = Convert.ToString(veriler);

                break;
            }

            var durumCek = (from x in baglan.durumGecmisi where x.bilgisayarID == kontrol orderby x.id descending select x).ToList();


            StringBuilder diskDurumuYuzdelik = new StringBuilder();
            StringBuilder ramDurumuYuzdelik = new StringBuilder();
            StringBuilder islemciDurumuYuzdelik = new StringBuilder();
            StringBuilder toplamString = new StringBuilder();

            foreach (var c in durumCek)
            {

                var ramKullanimOrani = ((c.anlikKullanilanRam * 100) / c.totalRam);
                if (ramKullanimOrani == 0)
                {
                    ramKullanimOrani = 1;
                }

                var diskKullanimOrani = ((c.anlikKullanilanDiskSize * 100) / c.kullanilanTotalDiskSize);
                if (diskKullanimOrani == 0)
                {
                    diskKullanimOrani = 1;
                }

                ramDurumuYuzdelik.Append(ramKullanimOrani);
                ramDurumuYuzdelik.Append(",");

                diskDurumuYuzdelik.Append(diskKullanimOrani);
                diskDurumuYuzdelik.Append(",");

                islemciDurumuYuzdelik.Append(c.islemciKullanimi);
                islemciDurumuYuzdelik.Append(",");

            }

            var aradeger = "";
            aradeger = Convert.ToString(ramDurumuYuzdelik).TrimEnd(',');
            toplamString.Append(aradeger);
            toplamString.Append("&");

            aradeger = "";
            aradeger = Convert.ToString(diskDurumuYuzdelik).TrimEnd(',');
            toplamString.Append(aradeger);
            toplamString.Append("&");

            aradeger = "";
            aradeger = Convert.ToString(islemciDurumuYuzdelik).TrimEnd(',');
            toplamString.Append(aradeger);


            ViewBag.toplamStringGonder = Convert.ToString(toplamString);


            return View();
        }


        [HttpPost]
        public int stream(durumGecmisi durumCek)
        {

            sosyalmedyaEntities baglan = new sosyalmedyaEntities();
            durumGecmisi durum = new durumGecmisi();

            durum.anlikKullanilanDiskSize = durumCek.anlikKullanilanDiskSize;
            durum.anlikKullanilanRam = durumCek.anlikKullanilanRam;
            durum.bilgisayarID = durumCek.bilgisayarID;
            durum.guncellenmeTarihi = DateTime.Now;
            durum.islemciKullanimi = durumCek.islemciKullanimi;
            durum.totalRam = durumCek.totalRam;
            durum.kullanilanTotalDiskSize = durumCek.kullanilanTotalDiskSize;

            baglan.durumGecmisi.Add(durum);
            baglan.SaveChanges();

            return 200;
        }


        [HttpPost]
        public int bilgisayarGuncelle(bilgisayarlar pc)
        {

            sosyalmedyaEntities baglan = new sosyalmedyaEntities();
            

            var sorgula = (from x in baglan.bilgisayarlar where x.kayitID == pc.kayitID select x).SingleOrDefault();

            sorgula.isletimSistemi = pc.isletimSistemi;
            sorgula.programlar = pc.programlar;
            sorgula.eklenmeTarihi = DateTime.Now;
            baglan.SaveChanges();

            return 200;
        }


        [WebMethod]
        public string kayitIDAl()
        {

            Random rastgele = new Random();
            int sayi = rastgele.Next();

            sosyalmedyaEntities baglan = new sosyalmedyaEntities();
            bilgisayarlar blg = new bilgisayarlar();


            var sorgula = (from x in baglan.bilgisayarlar where x.kayitID == sayi select x).SingleOrDefault();
            string deger = "";

            if (sorgula == null)
            {
                deger = Convert.ToString(sayi);
            }
            else
            {
                deger = Convert.ToString(sayi) + "0144";
            }

            blg.eklenmeTarihi = DateTime.Now;
            blg.programlar = "0";
            blg.isletimSistemi = "0";
            blg.kayitID = Convert.ToInt32(deger);

            baglan.bilgisayarlar.Add(blg);
            baglan.SaveChanges();

            return deger;

        }


    }
}