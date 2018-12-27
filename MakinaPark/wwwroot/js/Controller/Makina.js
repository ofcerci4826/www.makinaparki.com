var Makina = {

    MakinaOlustur: function () {

     

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

        if ($('#inputMarka').val() == "0") {
            alert('Lütfen Bir Marka Seçiniz ');
            return;
        }

        if ($('#inputModel').val() == "0") {
            alert('Lütfen Bir Model Seçiniz ');
            return;
        }
        if ($('#inputMakinaDurum').val() == "0") {
            alert('Lütfen Bir Ödeme Tipi Seçiniz ');
            return;
        }
        if ($('#txtModelYili').val() == "") {
            alert('Lütfen Model yılı giriniz ');
            return;
        }

        if ($('#txtCalismaSaati').val() == "") {
            alert('Lütfen Kiralama Süresini Belirtiniz ');
            return;
        }

       

        if ($('#txtPlaka').val() == "0") {
            alert('Lütfen Kiralama Süresini Belirtiniz ');
            return;
        }

        if ($('#txtSeriNo').val() == "") {
            alert('Lütfen Model yılı giriniz ');
            return;
        }
   


        var params = {
            refKategori: $('#inputKategori').val(),
            refKategoriAlt: $('#inputAltKategori').val(),
            refKategoriMarka: $('#inputMarka').val(),
            refKategoriMarkaModel: $('#inputModel').val(),
            refMakinaDurum: $('#inputMakinaDurum').val(),
            ModelYili: $('#txtModelYili').val(),
            CalismaSaati: $('#txtCalismaSaati').val(),
            Plaka: $('#txtPlaka').val(),
            txtSeriNo: $('#txtSeriNo').val(),
          
        };
        GlobalLoading.show();
        Network.ajaxRequest({
            method: "post",
            url: '/Makina/Olustur',
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
                GlobalLoading.hide();
            }

        });
        GlobalLoading.hide();
    },
   
}