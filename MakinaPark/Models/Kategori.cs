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
    public class Kategori
    {
        public int Id { get; set; }
        public string KategoriAd { get; set; }
        

        public static Kategori Parse(DataRow row)
        {
            return new Kategori
            {
                Id = row.GetInteger("Id"),
                KategoriAd = row.GetString("KategoriAd")
               
            };
        }

        public static List<Kategori> Listele()
        {
            return Sql.GetInstance().List("sp_kategori_listesi", null, (row) =>
            {
                return Parse(row);
            });
        }

    }
}