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
    public class KategoriModel
    {
        public int Id { get; set; }
        public int refMarka { get; set; }
        public string ModelAd { get; set; }

        public static KategoriModel Parse(DataRow row)
        {
            return new KategoriModel
            {
                Id = row.GetInteger("Id"),
                refMarka = row.GetInteger("refMarka"),
                ModelAd = row.GetString("ModelAd")
               
            };
        }

        public static List<KategoriModel> Listele(int refKategoriMarka)
        {
            return Sql.GetInstance().List("sp_kategori_model_listesi", new List<object> { refKategoriMarka }, (row) =>
            {
                return Parse(row);
            });
        }

    }
}