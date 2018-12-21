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
    public class KategoriMarka
    {
        public int refMarka { get; set; }
        public string MarkaAd { get; set; }

        public static KategoriMarka Parse(DataRow row)
        {
            return new KategoriMarka
            {
                refMarka = row.GetInteger("refMarka"),
                MarkaAd = row.GetString("MarkaAd")
               
            };
        }

        public static List<KategoriMarka> Listele(int refKategoriAlt)
        {
            return Sql.GetInstance().List("sp_kategori_marka_listesi", new List<object> { refKategoriAlt }, (row) =>
            {
                return Parse(row);
            });
        }

    }
}