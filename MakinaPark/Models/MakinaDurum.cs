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
    public class MakinaDurum
    {
        public int Id { get; set; }
        public string MakinaDurumu { get; set; }
        

        public static MakinaDurum Parse(DataRow row)
        {
            return new MakinaDurum
            {
                Id = row.GetInteger("Id"),
                MakinaDurumu = row.GetString("MakinaDurumu")
               
            };
        }

        public static List<MakinaDurum> Listele()
        {
            return Sql.GetInstance().List("sp_makina_durumu_listesi", null, (row) =>
            {
                return Parse(row);
            });
        }

      
    }
}