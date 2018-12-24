var Kiralik = {

    KiralikMakinaTalebiOlustur: function () {

        if ($('#txtBaslik').val() == "") {
            alert('Lütfen Talep Başlığı Griniz');
            return;
        }

        if ($('#txtAciklama').val() == "") {
            alert('Lütfen Açıklama Griniz');
            return;
        }

        if ($('#inputKategori').val() == "0") {
            alert('Lütfen Bir Kategori Seçiniz ');
            return;
        }

        if ($('#inputAltKategori').val() == "0") {
            alert('Lütfen Bir Alt Kategori Seçiniz ');
            return;
        }

        if ($('#inputIl').val() == "0") {
            alert('Lütfen Bir Şehir Seçiniz ');
            return;
        }

        if ($('#inputIlce').val() == "0") {
            alert('Lütfen Bir İlçe Seçiniz ');
            return;
        }

        if ($('#txtTelefonNo').val() == "") {
            alert('Lütfen Bir Telefon Numarası giriniz ');
            return;
        }

        if ($('#inputOdemeTipi').val() == "0") {
            alert('Lütfen Bir Ödeme Tipi Seçiniz ');
            return;
        }

        if ($('#txtKacAdet').val() == "") {
            alert('Lütfen Adet giriniz ');
            return;
        }

        if ($('#inputSevkiyat').val() == "0") {
            alert('Lütfen Sevkiyat Tipini Seçiniz ');
            return;
        }

        if ($('#inputOperator').val() == "0") {
            alert('Lütfen Operatör Tipi Seçiniz ');
            return;
        }


        if ($('#txtSure').val() == "0") {
            alert('Lütfen Kiralama Süresini Belirtiniz ');
            return;
        }

        if ($('#inputSureTipi').val() == "0") {
            alert('Lütfen Kiralama Tipi Seçiniz ');
            return;
        }

        var params = {
            refIl: $('#inputIl').val(),
            refIlce: $('#inputIlce').val(),
            refKategori: $('#inputKategori').val(),
            refKategoriAlt: $('#inputAltKategori').val(),
            refKategoriMarka: $('#inputMarka').val(),
            refKategoriMarkaModel: $('#inputModel').val(),
            Baslik: $('#txtBaslik').val(),
            Aciklama: $('#txtAciklama').val(),
            refOdemeTipi: $('#inputOdemeTipi').val(),
            OdemeTipiDiger: $('#txtOdemeTipiDiger').val(),
            KacAdet: $('#txtKacAdet').val(),
            KiralamaSure: $('#txtSure').val(),
            refKiralamaSureTipi: $('#inputSureTipi').val(),
            IsBaslangic: $("#datetimepicker1").find("input").val(),
            refOperatorVarmi: $('#inputOperator').val(),

        };

        Network.ajaxRequest({
            method: "post",
            url: '/Kiralik/KiralikMakinaTalepKaydet',
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

            },
            complete: function () {

            }
        });
    },

}