var User = {

    BilgilerimiGuncelle: function () {

        if ($('#txtAdSoyad').val() == "") {
            alert('Lütfen Ad Soyad Griniz');
            return;
        }

        if ($('#txtEposta').val() == "") {
            alert('Lütfen Eposta Griniz');
            return;
        }

        if ($('#txtTelefon').val() == "") {
            alert('Lütfen Bir Telefon Numarası giriniz ');
            return;
        }


        var params = {
            AdSoyad: $('#txtAdSoyad').val(),
            Eposta: $('#txtEposta').val(),
            Telefon: $('#txtTelefon').val(),
        };
        GlobalLoading.show();
        Network.ajaxRequest({
            method: "post",
            url: '/User/BilgilerimiGuncelle',
            data: params,
            success: function (data) {
                console.log(data);

                if (data.Status == 402) {
                    //Toast.show({ content: "Geçersiz oturum." });
                    alert('Geçersiz oturum.');
                    window.location.reload(true);
                    return;
                }

                if (data.Status == 403) {
                    //Toast.show({ content: "Bu işleme yetkiniz bulunmamaktadır." });
                    alert('Bu işleme yetkiniz bulunmamaktadır.');
                    return;
                }

                if (data.Status == 400) {
                    //Toast.show({ content: "Lütfen tüm gerekli bilgileri doldurunuz." });
                    alert('Lütfen tüm gerekli bilgileri doldurunuz.');
                    return;
                }

                if (data.Status != 200 && data.Status != 199) {
                    //Toast.show({ content: data.Result });
                    alert(data.Result);
                    return;
                }

                alert('Başarılı');
            },
            complete: function () {
                GlobalLoading.hide();
            }

        });
        GlobalLoading.hide();
    },
  
}