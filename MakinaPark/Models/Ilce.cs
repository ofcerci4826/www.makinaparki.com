using Vegatro.Database;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;
using Vegatro.NetCore;
using Vegatro.NetCore.Utils;
using Vegatro.Core;

namespace MakinaPark.Models
{
    public class Ilce
    {
        public int Id { get; set; }
        public string IlceAd { get; set; }
        public int refIl { get; set; }

        public static Ilce Parse(DataRow row)
        {
            return new Ilce
            {
                Id = row.GetInteger("Id"),
                IlceAd = row.GetString("Ilce"),
                refIl = row.GetInteger("refIl")

            };
        }


        public static List<Ilce> Listele(int refIl)
        {
            return Sql.GetInstance().List("sp_ilce_listesi", new List<object> { refIl }, (row) =>
            {
                return Parse(row);
            });
        }

        //public static List<Sehir> Listele()
        //{
        //    return Sql.GetInstance().List("sp_kullanici_listesi", new List<object> { Oturum().Id }, (row) =>
        //    {
        //        return Parse(row);
        //    });
        //}

        //public static string Olustur(Kullanici param)
        //{
        //    return Sql.GetInstance().Get("sp_kullanici_olustur", new List<object> {
        //        Oturum().Id,
        //        param.refId,
        //        param.AdSoyad,
        //        param.Eposta,
        //        param.Parola,
        //        param.Aktif,
        //        param.refKullaniciGrup
        //    }, (row) =>
        //    {
        //        return row.GetString("Result");
        //    });
        //}

        //public static string Guncelle(Kullanici param)
        //{
        //    return Sql.GetInstance().Get("sp_kullanici_guncelle", new List<object>
        //    {
        //        Oturum().Id,
        //        param.refId,
        //        param.Id,
        //        param.AdSoyad,
        //        param.Aktif,
        //        param.refKullaniciGrup
        //    }, (row) =>
        //    {
        //        return row.GetString("Result");
        //    });
        //}

        //public static string Sil(int id)
        //{
        //    return Sql.GetInstance().Get("sp_kullanici_sil", new List<object> { Oturum().Id, id }, (row) =>
        //    {
        //        return row.GetString("Result");
        //    });
        //}
    }
}