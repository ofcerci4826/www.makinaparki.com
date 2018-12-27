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
    public class Kullanici
    {
        public long Id { get; set; }
        public long refFirma { get; set; }
        public long refKullaniciGrup { get; set; }
        public string KullaniciGrupAd { get; set; }
        public string Token { get; set; }
        public string AdSoyad { get; set; }
        public string Eposta { get; set; }
        public string TelefonNo { get; set; } 
        public string Parola { get; set; }
        public bool FirmaVarmi { get; set; }
        public string FirmaAdi { get; set; }
        public int refVergiDairesi { get; set; }
        public string VergiDairesi { get; set; }
        public string VergiNo { get; set; }
        public int refIl { get; set; }
        public string Il { get; set; }
        public int refIlce { get; set; }
        public string Ilce { get; set; }
        public string Adres { get; set; }
        public string WebSitesi { get; set; }
        public bool Aktif { get; set; }
        public string RecordTime { get; set; }
        public List<Yetki> Yetkiler { get; set; }

        public static Kullanici Parse(DataRow row)
        {
            return new Kullanici
            {
                Id = row.GetLong("Id"),
                refFirma = row.GetLong("refFirma"),
                refKullaniciGrup = row.GetLong("refKullaniciGrup"),
                KullaniciGrupAd = row.GetString("KullaniciGrupAd"),
                Token = row.GetString("Token"),
                AdSoyad = row.GetString("AdSoyad"),
                Eposta = row.GetString("Eposta"),
                TelefonNo = row.GetString("TelefonNo"),
                Parola = row.GetString("Parola"),  
                FirmaVarmi = row.GetBool("FirmaVarmi"),
                FirmaAdi = row.GetString("FirmaAdi"),
                refVergiDairesi = row.GetInteger("refVergiDairesi"),
                VergiDairesi = row.GetString("VergiDairesi"),
                VergiNo = row.GetString("VergiNo"),
                refIl = row.GetInteger("refIl"),
                Il = row.GetString("Il"),
                refIlce = row.GetInteger("refIlce"),
                Ilce = row.GetString("Ilce"),
                Adres = row.GetString("Adres"),
                WebSitesi = row.GetString("WebSitesi"), 
                Aktif = row.GetBool("Aktif"),
                RecordTime = row.GetString("RecordTime"),
              
                
                Yetkiler = new List<Yetki>()
            };
        }

        public bool IsValid(bool isUpdate = false)
        {
            if (isUpdate && Id <= 0)
                return false;

            if (string.IsNullOrEmpty(AdSoyad)
                || string.IsNullOrEmpty(Eposta)
                || string.IsNullOrEmpty(TelefonNo)
                || string.IsNullOrEmpty(Parola)
                )
                return false;

            return true;
        }

        public static Kullanici Oturum(long? id = null)
        {
            if (id != null)
            {
                Kullanici kullanici = new Kullanici();
                kullanici.Id = (long)id;
                return kullanici;
            }

            if (ContextObject.Current.Session.GetString("Kullanici") == null)
                return new Kullanici();

            return JsonConvert.DeserializeObject<Kullanici>(ContextObject.Current.Session.GetString("Kullanici"));
        }

        public bool IsAuthorized(string controller, string action, string platform = Platform.WEB)
        {
            if (string.IsNullOrEmpty(controller)
                || string.IsNullOrEmpty(action)
                || string.IsNullOrEmpty(platform))
                return false;

            controller = controller.ToLower();
            action     = action.ToLower();
            platform   = platform.ToLower();

            foreach (var item in Yetkiler)
            {
                if (!platform.Equals(item.Platform.ToLower()))
                    continue;

                if (("*".Equals(item.Controller) || item.Controller.ToLower().Equals(controller))
                     && ("*".Equals(item.Action) || ("*".Equals(action) || item.Action.ToLower().Equals(action)))) 
                    return true;
            }

            return false;
        }

        public static Kullanici Giris(string eposta, string parola, string platform, string platformToken, string deviceId = "")
        {
            string ip        = ContextObject.Current.Connection.RemoteIpAddress.ToString();
            string port      = ContextObject.Current.Connection.RemotePort.ToString();
            string userAgent = ContextObject.Current.Request.Headers["User-Agent"].ToString();

            Kullanici kullanici = new Kullanici();

            Sql.GetInstance().Set("sp_kullanici_giris", new List<object> { eposta, parola, ip, port, userAgent, platform, platformToken, deviceId }, (ds) =>
            {
                if (ds.Tables.Count <= 0)
                    return;

                if (ds.Tables[0].Rows.Count <= 0)
                    return;

                if (!"SUCCEED".Equals(ds.Tables[0].Rows[0].GetString("Result")))
                    return;

                kullanici = Parse(ds.Tables[0].Rows[0]);

                if (ds.Tables.Count > 1)
                {
                    foreach (DataRow row in ds.Tables[1].Rows)
                        kullanici.Yetkiler.Add(Yetki.Parse(row));
                }

                if (Platform.WEB.ToLower().Equals(platform.ToLower()))
                {
                    string sessionId = string.IsNullOrEmpty(Config.Get("Logging:SessionId")) ? "LogId" : Config.Get("Logging:SessionId");

                    ContextObject.Current.Session.SetString("Kullanici", JsonConvert.SerializeObject(kullanici));
                    ContextObject.Current.Session.SetString(sessionId, kullanici.Id.ToString());
                }

                Sql.ResetInstance();
            });

            return kullanici;
        }

        public static void Cikis()
        {
            ContextObject.Current.Session.Remove("Kullanici");
        }

        public static Kullanici Detay(int id)
        {
            return Sql.GetInstance().Get("sp_kullanici_detay", new List<object> { Oturum().Id, id }, (row) =>
            {
                return Parse(row);
            });
        }

        public static List<Kullanici> Listele()
        {
            return Sql.GetInstance().List("sp_kullanici_listesi", new List<object> { Oturum().Id }, (row) =>
            {
                return Parse(row);
            });
        }

        public static string Olustur(Kullanici param)
        {
            return Sql.GetInstance().Get("sp_kullanici_olustur", new List<object> {
                null,
                null,
                param.AdSoyad,
                param.TelefonNo,
                param.Eposta,
                param.Parola,
                1,
                2,
                param.FirmaVarmi,
                param.FirmaAdi,
                param.refVergiDairesi,
                param.VergiNo,
                param.refIl,
                param.refIlce,
                param.Adres,
                param.WebSitesi
            }, (row) =>
            {
                return row.GetString("Result");
            });
        }

        public static string Guncelle(Kullanici param)
        {
            return Sql.GetInstance().Get("sp_kullanici_guncelle", new List<object>
            {
                Oturum().Id,
                param.Id,
                param.AdSoyad,
                param.Aktif,
                param.refKullaniciGrup
            }, (row) =>
            {
                return row.GetString("Result");
            });
        }

        public static string Sil(int id)
        {
            return Sql.GetInstance().Get("sp_kullanici_sil", new List<object> { Oturum().Id, id }, (row) =>
            {
                return row.GetString("Result");
            });
        }

        public static string BilgilerimiGuncelle(string  AdSoyad, string Eposta, string Telefon)
        {
            return Sql.GetInstance().Get("sp_kullanici_bilgilerimi_guncelle", new List<object>
            {
                Oturum().Id,
                AdSoyad,
                Eposta,
                Telefon
            }, (row) =>
            {
                return row.GetString("Result");
            });
        }
    }
}