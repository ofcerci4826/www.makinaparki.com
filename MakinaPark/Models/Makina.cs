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
        public string Ad { get; set; }
        public string Slug { get; set; }

        public static Makina Parse(DataRow row)
        {
            return new Makina
            {
                Id = row.GetLong("Id"),
                Ad = row.GetString("Name"),
                Slug = row.GetString("Slug")
            };
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

        public static List<Makina> Listesi()
        {
            return Sql.GetInstance().List("sp_makina_listesi", new List<object> { Kullanici.Oturum().Id }, (row) =>
            {
                return Parse(row);
            });
        }
    }
}
