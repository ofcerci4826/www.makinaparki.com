using Vegatro.Database;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Vegatro.NetCore.Utils;

namespace MakinaPark.Models
{
    public class KullaniciGrup
    {
        public long Id { get; set; }
        public string Ad { get; set; }
        public bool Silinemez { get; set; }
        public long[] Yetkiler { get; set; }
        public List<Yetki> YetkiListesi { get; set; }
        public string RecordTime { get; set; }

        public static KullaniciGrup Parse(DataRow row)
        {
            return new KullaniciGrup
            {
                Id           = row.GetLong("Id"),
                Ad           = row.GetString("Ad"),
                RecordTime   = row.GetString("RecordTime"),
                Silinemez    = row.GetBool("Silinemez"),
                YetkiListesi = new List<Yetki>()
            };
        }

        public static KullaniciGrup Detay(int id)
        {
            KullaniciGrup kullaniciGrup = new KullaniciGrup();

            Sql.GetInstance().Set("sp_kullanici_grup_detay", new List<object> { Kullanici.Oturum().Id, id }, (ds) =>
            {
                kullaniciGrup = Parse(ds.Tables[0].Rows[0]);

                if (ds.Tables.Count <= 1)
                    return;

                foreach (DataRow row in ds.Tables[1].Rows)
                    kullaniciGrup.YetkiListesi.Add(Yetki.Parse(row));
            });

            return kullaniciGrup;
        }

        public static List<KullaniciGrup> Listesi()
        {
            return Sql.GetInstance().List("sp_kullanici_grup_listesi", new List<object> { Kullanici.Oturum().Id }, (row) =>
            {
                return Parse(row);
            });
        }

        public static string Olustur(KullaniciGrup param)
        {
            StringBuilder builder = new StringBuilder("<ROOT>");

            foreach (long item in param.Yetkiler)
                builder.Append("<R id=\"" + item + "\" />");

            builder.Append("</ROOT>");

            return Sql.GetInstance().Get("sp_kullanici_grup_olustur", new List<object> {
                Kullanici.Oturum().Id,
                param.Ad,
                param.Silinemez,
                builder.ToString()
            }, (row) => {
                return row.GetString("Result");
            });
        }

        public static string Guncelle(KullaniciGrup param)
        {
            StringBuilder builder = new StringBuilder("<ROOT>");

            foreach (long item in param.Yetkiler)
                builder.Append("<R id=\"" + item + "\" />");

            builder.Append("</ROOT>");

            return Sql.GetInstance().Get("sp_kullanici_grup_guncelle", new List<object> {
                Kullanici.Oturum().Id,
                param.Id,
                param.Ad,
                param.Silinemez,
                builder.ToString()
            }, (row) => {
                return row.GetString("Result");
            });
        }

        public static string Sil(long id)
        {
            return Sql.GetInstance().Get("sp_kullanici_grup_sil", new List<object> { Kullanici.Oturum().Id, id }, (row) =>
            {
                return row.GetString("Result");
            });
        }
    }
}