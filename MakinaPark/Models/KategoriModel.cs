using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Vegatro.Database;
using Vegatro.NetCore.Utils;

namespace MakinaPark.Models
{
    public class KategoriModel
    {
        public long Id { get; set; }
        public string Kategori { get; set; }
        public string KategoriAlt { get; set; }
        public string Aciklama { get; set; }
        public string Slug { get; set; }

        public static KategoriModel Parse(DataRow row)
        {
            return new KategoriModel
            {
                Id = row.GetLong("Id"),
                Kategori = row.GetString("Kategori"),
                KategoriAlt = row.GetString("KategoriAlt"),
                Aciklama = row.GetString("Aciklama"),
                Slug = row.GetString("Slug")
            };
        }

        public static List<KategoriModel> Detay(string slug)
        {
              
           return Sql.GetInstance().List("sp_kategorialt_listesi", new List<object> { slug }, (row) =>
            {
               return Parse(row);
            });

            //return kategori;
        }

        public static List<KategoriModel> Listesi()
        {
            return Sql.GetInstance().List("sp_kategori_listesi", new List<object> { }, (row) =>
            {
                return Parse(row);
            });
        }
    }
}
