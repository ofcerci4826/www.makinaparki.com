using Vegatro.Database;
using System.Collections.Generic;
using System.Data;
using Vegatro.NetCore.Utils;

namespace MakinaPark.Models
{
    public class Yetki
    {
        public long Id { get; set; }
        public string Platform { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Label { get; set; }

        public static Yetki Parse(DataRow row)
        {
            return new Yetki
            {
                Id         = row.GetLong("Id"),
                Platform   = row.GetString("Platform"),
                Controller = row.GetString("Controller"),
                Action     = row.GetString("Action"),
                Label      = row.GetString("Label")
            };
        }

        public static List<Yetki> Listesi()
        {
            return Sql.GetInstance().List("sp_yetki_listesi", new List<object> { Kullanici.Oturum().Id }, (row) =>
            {
                return Parse(row);
            });
        }

        public static string Olustur(Yetki param)
        {
            return Sql.GetInstance().Get("sp_yetki_olustur", new List<object> {
                Kullanici.Oturum().Id,
                param.Platform,
                param.Controller,
                param.Action,
                param.Label
            }, (row) => {
                return row.GetString("Result");
            });
        }

        public static string Sil(long id)
        {
            return Sql.GetInstance().Get("sp_yetki_sil", new List<object> { Kullanici.Oturum().Id, id }, (row) =>
            {
                return row.GetString("Result");
            });
        }
    }
}