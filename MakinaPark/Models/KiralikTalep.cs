using Vegatro.Database;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;
using Vegatro.NetCore;
using Vegatro.NetCore.Utils;
using Vegatro.Core;
using System;

namespace MakinaPark.Models
{
    public class KiralikTalep
    {
        public long Id { get; set; }
        public long refKullaniciId { get; set; }
        public int refIl { get; set; }
        public int refIlce { get; set; }
        public long refKategori { get; set; }
        public long refKategoriAlt { get; set; }
        public long refKategoriMarka { get; set; }
        public long refKategoriMarkaModel { get; set; }
        public int refTeklifDurum { get; set; }
        public string  Baslik { get; set; }
        public string Aciklama { get; set; }
        public int refOdemeTipi { get; set; }
        public string OdemeTipiDiger { get; set; }
        public int KacAdet { get; set; }
        public int KiralamaSure { get; set; }
        public int refKiralamaSureTipi { get; set; }
        public string  IsBaslangic { get; set; }
        public int refOperatorVarmi { get; set; }
        public bool Aktif { get; set; }
        public DateTime GecerlilikTarihi { get; set; }
        public DateTime KayitTarihi { get; set; }
        public DateTime GuncellemeTarihi { get; set; }

        public static KiralikTalep Parse(DataRow row)
        {
            return new KiralikTalep
            {
                Id = row.GetLong("Id"),
                refKullaniciId = row.GetLong("refKullaniciId"),
                refIl = row.GetInteger("refIl"),
                refIlce = row.GetInteger("refIlce"),
                refKategori = row.GetLong("refKategori"),
                refKategoriAlt = row.GetLong("refKategoriAlt"),
                refKategoriMarka = row.GetLong("refKategoriMarka"),
                refKategoriMarkaModel = row.GetLong("refKategoriMarkaModel"),
                refTeklifDurum = row.GetInteger("refTeklifDurum"),
                Baslik = row.GetString("Baslik"),
                Aciklama = row.GetString("Aciklama"),
                refOdemeTipi= row.GetInteger("refOdemeTipi"),
                OdemeTipiDiger = row.GetString("OdemeTipiDiger"),
                KacAdet = row.GetInteger("KacAdet"),
                KiralamaSure = row.GetInteger("KiralamaSure"),
                refKiralamaSureTipi = row.GetInteger("refKiralamaSureTipi"),
                IsBaslangic = row.GetString("IsBaslangic"),
                refOperatorVarmi = row.GetInteger("refOperatorVarmi"),
                Aktif = row.GetBool("Aktif"),
                GecerlilikTarihi = row.GetDatetime("GecerlilikTarihi"),
                KayitTarihi = row.GetDatetime("KayitTarihi"),
                GuncellemeTarihi = row.GetDatetime("GuncellemeTarihi"),
            };
        }


        public static string KiralikMakinaTalepOlustur(int refIl, int refIlce, int refKategori,
            int refKategoriAlt, int refKategoriMarka, int refKategoriMarkaModel, string Baslik, string Aciklama,
            int refOdemeTipi, string OdemeTipiDiger, int KacAdet, int KiralamaSure, int refKiralamaSureTipi,
            string  IsBaslangic, int refOperatorVarmi)
        {
            return Sql.GetInstance().Get("sp_kiralik_makina_talep_olustur", new List<object> {
               Kullanici.Oturum().Id,
                refIl,
                refIlce,
                refKategori,
                refKategoriAlt,
                refKategoriMarka,
                refKategoriMarkaModel,
                Baslik,
                Aciklama,
                refOdemeTipi,
                OdemeTipiDiger,
                KacAdet,
                KiralamaSure,
                refKiralamaSureTipi,
                IsBaslangic,
                refOperatorVarmi,
            }, (row) =>
            {
                return row.GetString("Result");
            });
        }
    }
}