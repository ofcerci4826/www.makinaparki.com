using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Vegatro.Database;
using Vegatro.NetCore.Utils;

namespace MakinaPark.Models
{
    public class Makina
    {
        public long Id { get; set; }
        public long refKullanici { get; set; }
        public string AdSoyad { get; set; }
        public int refKategori { get; set; }
        public string KategoriAd { get; set; }
        public int refKategoriAlt { get; set; }
        public string KategoriAltAd { get; set; }
        public int refKategoriMarka { get; set; }
        public string MarkaAd { get; set; }
        public int refKategoriMarkaModel { get; set; }
        public string ModelAd { get; set; }
        public int refMakinaDurum { get; set; }
        public string MakinaDurumu { get; set; }
        public int CalismaSaati { get; set; }
        public int ModelYil { get; set; }
        public string  Plaka { get; set; }
        public string SeriNo { get; set; }
        public string Aciklama { get; set; }
        public bool Aktif { get; set; }
        public DateTime KayitTarihi { get; set; }
        public DateTime GuncellemeTarihi { get; set; }

        public static Makina Parse(DataRow row)
        {
            return new Makina
            {
                Id = row.GetLong("Id"),
                refKullanici = row.GetLong("refKullanici"),
                AdSoyad = row.GetString("AdSoyad"),
                refKategori = row.GetInteger("refKategori"),
                KategoriAd = row.GetString("KategoriAd"),
                refKategoriAlt = row.GetInteger("refKategoriAlt"),
                KategoriAltAd = row.GetString("KategoriAltAd"),
                refKategoriMarka = row.GetInteger("refKategoriMarka"),
                MarkaAd = row.GetString("MarkaAd"),
                refKategoriMarkaModel = row.GetInteger("refKategoriMarkaModel"),
                ModelAd = row.GetString("ModelAd"),
                refMakinaDurum = row.GetInteger("refMakinaDurum"),
                MakinaDurumu = row.GetString("MakinaDurumu"),
                CalismaSaati = row.GetInteger("CalismaSaati"),
                ModelYil = row.GetInteger("ModelYil"),
                Plaka = row.GetString("Plaka"),
                SeriNo = row.GetString("SeriNo"),
                Aciklama = row.GetString("Aciklama"),

            };
        }

        //public static string Olustur(int refKategori, int refKategoriAlt, int refKategoriMarka, int refKategoriMarkaModel, int refMakinaDurum, int ModelYil, int CalismaSaati, int Plaka, int SeriNo, string Aciklama)

              public static string Olustur(Makina param)
        {
            return Sql.GetInstance().Get("sp_kullanici_makina_olustur", new List<object> {
               Kullanici.Oturum().Id,
               param.refKategori,
               param.refKategoriAlt,
               param.refKategoriMarka,
               param.refKategoriMarkaModel,
               param.refMakinaDurum,
               param.CalismaSaati,
               param.ModelYil,
               param.Plaka,
               param.SeriNo,
               param.Aciklama
            }, (row) =>
            {
                return row.GetString("Result");
            });
        }

        public static Makina Detay(string slug)
        {
            Makina makina = new Makina();

            Sql.GetInstance().Set("sp_makina_detay", new List<object> { Kullanici.Oturum().Id, slug }, (ds) =>
            {
                makina = Parse(ds.Tables[0].Rows[0]);
            });

            return makina;
        }

        //public static List<Makina> Listesi()
        //{
        //    return Sql.GetInstance().List("sp_makina_listesi", new List<object> { Kullanici.Oturum().Id }, (row) =>
        //    {
        //        return Parse(row);
        //    });
        //}

        public static List<Makina> Parkim()
        {
            return Sql.GetInstance().List("sp_kullanici_makina_listesi", new List<object> { Kullanici.Oturum().Id }, (row) =>
            {
                return Parse(row);
            });
        }


        
    }
}
