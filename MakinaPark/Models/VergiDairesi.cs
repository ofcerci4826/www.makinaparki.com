using System.Collections.Generic;
using System.Data; 
using Vegatro.Database;
using Vegatro.NetCore.Utils;

namespace MakinaPark.Models
{
    public class VergiDairesi
    {
        public int Id { get; set; }
        public string Ad { get; set; }


        public static VergiDairesi Parse(DataRow row)
        {
            return new VergiDairesi
            {
                Id = row.GetInteger("Id"),
                Ad = row.GetString("Ad")

            };
        }

        public static List<VergiDairesi> Listele(int refIl)
        {
            return Sql.GetInstance().List("sp_vergi_dairesi_listesi", new List<object> { refIl }, (row) =>
            {
                return Parse(row);
            });
        }
    }
}
