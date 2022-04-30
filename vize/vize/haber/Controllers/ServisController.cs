using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using haber.Models;
using haber.ViewModel;

namespace haber.Controllers
{

    public class ServisController : ApiController
    {
        DB03Entities db = new DB03Entities();
        SonucModel sonuc = new SonucModel();

        #region Haber

        [HttpGet]
        [Route("api/haberliste")]
        public List<HaberModel> HaberListe()
        {
            List<HaberModel> liste = db.Haber.Select(x => new HaberModel()
            {
                HaberId = x.HaberId,

                HaberBaslik = x.HaberBaslik,
                HaberIcerik = x.HaberIcerik,
                YayinlanmaTarihi = x.YayinlanmaTarihi,
                Kategori = x.Kategori,

            }).ToList();
            return liste;
        }

        [HttpGet]
        [Route("api/haberbyid/{haberId}")]
        public HaberModel HaberById(int HaberId)
        {
            HaberModel kayit = db.Haber.Where(s => s.HaberId == HaberId).Select(x => new HaberModel()
            {
                HaberId = x.HaberId,

                HaberBaslik = x.HaberBaslik,
                HaberIcerik = x.HaberIcerik,
                YayinlanmaTarihi = x.YayinlanmaTarihi,
                Kategori = x.Kategori,
            }).SingleOrDefault();
            return kayit;
        }

        [HttpPost]
        [Route("api/haberekle")]
        public SonucModel HaberEkle(HaberModel model)
        {
            if (db.Haber.Count(c => c.HaberBaslik == model.HaberBaslik) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Aynı Başlığa Sahip Haber Mevcuttur";
                return sonuc;
            }

            Haber yeni = new Haber();
            yeni.HaberBaslik = model.HaberBaslik;
            yeni.HaberIcerik = model.HaberIcerik;
            yeni.Kategori = model.Kategori;

            yeni.YayinlanmaTarihi = model.YayinlanmaTarihi;

            db.Haber.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Haber Eklendi";
            return sonuc;
        }

        [HttpPut]
        [Route("api/haberduzenle")]
        public SonucModel HaberDuzenle(HaberModel model)
        {
            Haber kayit = db.Haber.Where(s => s.HaberId == model.HaberId).SingleOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Haber Bulunmadı!";
                return sonuc;
            }

            kayit.HaberBaslik = model.HaberBaslik;
            kayit.HaberIcerik = model.HaberIcerik;

            kayit.Kategori = model.Kategori;
            kayit.Yazar = model.Yazar;
            kayit.YayinlanmaTarihi = model.YayinlanmaTarihi;

            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Haber Düzenlendi";
            return sonuc;
        }

        [HttpDelete]
        [Route("api/habersil/{haberId}")]
        public SonucModel HaberSil(int HaberId)
        {
            Haber kayit = db.Haber.Where(s => s.HaberId == HaberId).SingleOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Haber Bulunmadı!";
                return sonuc;
            }

            if (db.Haber.Count(c => c.HaberId == HaberId) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Bu haber silinemez Silinemez!";
                return sonuc;
            }


            db.Haber.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Haber Silndi";
            return sonuc;
        }



        #endregion

        #region Kullanici
        [HttpGet]
        [Route("api/kullanıcıliste")]
        public List<KullaniciModel> KullaniciListe()
        {
            List<KullaniciModel> liste = db.Kullanici.Select(x => new KullaniciModel()
            {
                AdiSoyadi = x.AdiSoyadi,
                KullaniciAdi = x.KullaniciAdi,
                Eposta = x.Eposta,
                Sifre = x.Sifre,
            }).ToList();
            return liste;
        }
        [HttpGet]
        [Route("api/kullanicibyid/{kullaniciId}")]
        public KullaniciModel KullaniciById(int KullaniciId)
        {
            KullaniciModel kayit = db.Kullanici.Where(s => s.KullaniciId == KullaniciId).Select(x => new KullaniciModel()
            {
                AdiSoyadi = x.AdiSoyadi,
                KullaniciAdi = x.KullaniciAdi,
                Eposta = x.Eposta,
                Sifre = x.Sifre,
            }).SingleOrDefault();
            return kayit;
        }

        [HttpPost]
        [Route("api/kullaniciekle")]
        public SonucModel KullaniciEkle(KullaniciModel model)
        {
            if (db.Kullanici.Count(s => s.KullaniciAdi == model.KullaniciAdi || s.Eposta == model.Eposta) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Bu kullanıcı kayıtlıdır.";
                return sonuc;
            }

            Kullanici yeni = new Kullanici();
            yeni.KullaniciAdi = model.KullaniciAdi;
            yeni.AdiSoyadi = model.AdiSoyadi;
            yeni.Eposta = model.Eposta;
            yeni.Sifre = model.Sifre;

            db.Kullanici.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Kullanıcı kaydedildi";

            return sonuc;
        }
        [HttpPut]
        [Route("api/kullaniciduzenle")]
        public SonucModel KullaniciDuzenle(KullaniciModel model)
        {
            Kullanici kayit = db.Kullanici.Where(s => s.KullaniciId == model.KullaniciId).SingleOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kullanıcı Bulunamadı!";
                return sonuc;
            }

            kayit.KullaniciAdi = model.KullaniciAdi;
            kayit.AdiSoyadi = model.AdiSoyadi;
            kayit.Eposta = model.Eposta;
            kayit.Sifre = model.Sifre;

            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Kullanıcı Düzenlendi";

            return sonuc;
        }

        [HttpDelete]
        [Route("api/kullanicisil/{kullaniciId}")]
        public SonucModel KullaniciSil(int KullaniciId)
        {
            Kullanici kayit = db.Kullanici.Where(s => s.KullaniciId == KullaniciId).SingleOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kullanıcı Bulunamadı!";
                return sonuc;
            }

            if (db.Kullanici.Count(c => c.KullaniciId == KullaniciId) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Aktif kullanici silinemez";
                return sonuc;
            }

            db.Kullanici.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Kullanici Silindi";

            return sonuc;
        }
        #endregion

        #region Yorum

        [HttpGet]
        [Route("api/yorumliste/{yorumId}")]
        public List<YorumModel> YorumListe(int yorumId)
        {
            List<YorumModel> liste = db.Yorum.Where(s => s.YorumId == yorumId).Select(x => new YorumModel()
            {
                Yorum1 = x.Yorum1,
                Puan = x.Puan,
            }).ToList();

            // foreach (var yorum in liste)
            //{
            //  yorum.HaberBilgi = HaberById(yorum.HaberId);
            // yorum.KullaniciBilgi = KullaniciById(yorum.YorumId);

            // }
            return liste;

        }
        [HttpGet]
        [Route("api/yorumliste")]
        public List<YorumModel> YorumListe()
        {
            List<YorumModel> liste = db.Yorum.Select(x => new YorumModel()
            {
                YorumId = x.YorumId,
                Yorum1 = x.Yorum1,

            }).ToList();
            return liste;
        }



        #endregion

        #region Kategori
        [HttpPost]
        [Route("api/kategoriekle")]
        public SonucModel KategoriEkle(KategoriModel model)
        {
            if (db.Kategori.Count(c => c.KategoriId == model.KategoriId) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Bu kategori bulunuyor";
                return sonuc;
            }

            Kategori yeni = new Kategori();
            yeni.KategoriId = model.KategoriId;
            db.Kategori.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Kategori Eklendi";
            return sonuc;
        }

        [HttpDelete]
        [Route("api/kategorisil/{kategoriId}")]
        public SonucModel KategoriSil(int KategoriId)
        {
            Kategori kayit = db.Kategori.Where(s => s.KategoriId == KategoriId).SingleOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kategori Bulunamadı!";
                return sonuc;
            }


            db.Kategori.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Kategori Silindi";

            return sonuc;
        }
        [HttpGet]
        [Route("api/kategoriliste")]
        public List<KategoriModel> KategoriListe()
        {
            List<KategoriModel> liste = db.Kategori.Select(x => new KategoriModel()
            {
                KategoriId = x.KategoriId,
                Kategori1 = x.Kategori1,

            }).ToList();
            return liste;
        }
        #endregion

        #region Resim

        [HttpGet]
        [Route("api/resimliste")]
        public List<ResimModel> ResimListe()
        {
            List<ResimModel> liste = db.Resim.Select(x => new ResimModel()
            {
                ResimId = x.ResimId,
                ResimAdi = x.ResimAdi,
                HaberId = x.HaberId
            }).ToList();
            return liste;
        }

        [HttpPost]
        [Route("api/resimekle")]
        public SonucModel ResimEkle(ResimModel model)
        {
            Resim yeni = new Resim();

            yeni.ResimAdi = model.ResimAdi;
            yeni.HaberId = model.HaberId;
            db.Resim.Add(yeni);
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Resim Eklendi";
            return sonuc;
        }

        [HttpPut]
        [Route("api/resimduzenle")]

        public SonucModel ResinDuzenle(ResimModel model)
        {
            Resim kayit = db.Resim.Where(s => s.ResimId == model.ResimId).SingleOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayit bulunamadi!";
                return sonuc;
            }

            kayit.ResimAdi = model.ResimAdi;
            kayit.HaberId = model.HaberId;
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Resim Düzenlendi";
            return sonuc;

        }
        [HttpDelete]
        [Route("api/resimsil/{ResimId}")]

        public SonucModel ResimSil(int ResimId)
        {
            Resim kayit = db.Resim.Where(s => s.ResimId == ResimId).SingleOrDefault(
           );
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadal";
                return sonuc;
            }

            db.Resim.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Resim Silindi";
            return sonuc;
        }
        [HttpGet]
        [Route("api/ResimById/{HaberId}")]

        public List<ResimModel> resimByHaberId(int HaberId)
        {
            List<ResimModel> kayit = db.Resim.Where(s => s.HaberId == HaberId).Select(x => new ResimModel()
            {
                ResimId = x.ResimId,
                ResimAdi = x.ResimAdi,
                HaberId = x.HaberId

            }).ToList();
            return kayit;

            


        }
        #endregion
    }
}
