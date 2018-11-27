namespace Vegatro.NetCore
{
    /// <summary>
    /// Status codes enum
    /// </summary>
    public enum Status
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

        SUCCEED_WITH_IDENTITY = 199, // Islem basarili, veritabani ayrica sonuc numarasi dondu
        SUCCEED = 200,               // Islem basarili
        ALREADY_EXISTS = 201,        // Eklenmek istenen kayıt hali hazirda veritabaninda mevcut
        IN_USE = 202,                // Islem yapilmaya calisilan kayit kullanimda oldugu icin islem gerceklestirilemez
        FAILED_NO_DB_RESULT = 298,   // Islem basarisiz, veritabani result alani bos dondu
        FAILED = 299,                // Islem basarisiz
        UNKNOWN = 300,               // Islem basarisiz, durumu bilinmeyen sonuc
        CUSTOM = 301,                // Islem basarisiz, mesaj alanini goster
        INVALID_PARAMETERS = 400,    // Gonderilmeye calisilan parametreler eksik veya hatali
        INVALID_USER = 401,          // Kullanici bilgileri hatali
        INVALID_SESSION = 402,       // Gecerli bir oturum bulunamadi, giris yapilmasi gerekmekte
        NOT_AUTHORIZED = 403,        // Islemi yapmaya yetkiniz yok
        INVALID_SECURITY_CODE = 405, // Güvenlik kodu hatalı
        ERROR_UNEXPECTED = 500,      // Beklenmedik hata
        ERROR_SERVER = 501           // Server hatası

#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }
}
