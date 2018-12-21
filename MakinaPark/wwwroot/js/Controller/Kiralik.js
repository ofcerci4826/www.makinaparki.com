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

        
        //Network.ajaxRequest({
        //    method: "post",
        //    url: '/Kategori/KategoriListele',
          
        //    success: function (data) {
        //        console.log(data);

        //        if (data.Status == 402) {
        //            Toast.show({ content: "Geçersiz oturum." });
        //            window.location.reload(true);
        //            return;
        //        }

        //        if (data.Status == 403) {
        //            Toast.show({ content: "Bu işleme yetkiniz bulunmamaktadır." });
        //            return;
        //        }

        //        if (data.Status == 400) {
        //            Toast.show({ content: "Lütfen tüm gerekli bilgileri doldurunuz." });
        //            return;
        //        }

        //        if (data.Status != 200 && data.Status != 199) {
        //            Toast.show({ content: data.Result });
        //            return;
        //        }
        //        $('#inputKategori').append(
        //            $('<option value=0>Kategori Seçiniz </option>'))

        //        $.each(data.Result, function (index, item) {
        //            $('#inputKategori').append('<option  value="' + item.Id + '">' + item.KategoriAd + '</option>');

        //        });

        //        $('#inputKategori').selectpicker('refresh');
        //        $('#inputAltKategori').append('<option  value="0">Alt Kategori Seçiniz</option>');
        //        $('#inputMarka').append('<option  value="0">Alt Marka Seçiniz</option>');
        //        $('#inputModel').append('<option  value="0">Alt Model Seçiniz</option>');
        //    },
        //    complete: function () {

        //    }
        //});
    },

}