using Newtonsoft.Json;

namespace MakinaPark.Models.Utils
{
    public class AppResponse
    {
        public static string Return(string dbresult)
        {
            if (dbresult.Split('|').Length>0)
            {
                if ("SUCCEED".Equals(dbresult.Split('|')[0]))
                {
                    return JsonConvert.SerializeObject(new { Status = 200, Eposta = dbresult.Split('|')[1], Parola = dbresult.Split('|')[2] });
                }
            }
            if (string.IsNullOrEmpty(dbresult))
                return JsonConvert.SerializeObject(new { Status = 298, Result = "İşlem başarısız." });

            if ("SUCCEED".Equals(dbresult))
                return JsonConvert.SerializeObject(new { Status = 200 });

            if ("ALREADY_EXISTS".Equals(dbresult))
                return JsonConvert.SerializeObject(new { Status = 201, Result = "Böyle bir kayıt sistemde zaten var." });

            if ("IN_USE".Equals(dbresult))
                return JsonConvert.SerializeObject(new { Status = 202, Result = "Bu kayıt kullanımda olduğu için silinemez." });

            if ("MUSTERI_ALREADY_EXISTS".Equals(dbresult))
                return JsonConvert.SerializeObject(new { Status = 203, Result = "Böyle bir müşteri zaten sistemde kayıtlı." });

            if ("CARI_ALREADY_EXISTS".Equals(dbresult))
                return JsonConvert.SerializeObject(new { Status = 203, Result = "Böyle bir cari kodlu müşteri zaten sistemde kayıtlı." });

            if ("FAILED".Equals(dbresult))
                return JsonConvert.SerializeObject(new { Status = 299, Result = "İşlem başarısız." });

            if (dbresult.Contains("SUCCEED_ID_"))
                return JsonConvert.SerializeObject(new { Status = 199, Id = dbresult.Replace("SUCCEED_ID_", "") });

            return JsonConvert.SerializeObject(new { Status = 297, Result = "İşlem başarısız." });
        }

        public static string Return(int status, object obj = null)
        {
            return JsonConvert.SerializeObject(new { Status = status, Result = obj });
        }
    }
}
