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
    public class KategoriAlt
    {
        public int Id { get; set; }
        public int refKategori { get; set; }
        public string KategoriAltAd { get; set; }

        


        public static KategoriAlt Parse(DataRow row)
        {
            return new KategoriAlt
            {
                Id = row.GetInteger("Id"),
                refKategori = row.GetInteger("refKategori"),
                KategoriAltAd = row.GetString("KategoriAltAd")
               
            };
        }

        public static List<KategoriAlt> Listele(int refKategori)
        {
            return Sql.GetInstance().List("sp_kategori_alt_listesi", new List<object> { refKategori }, (row) =>
            {
                return Parse(row);
            });
        }

    }
}