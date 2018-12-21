var Kategori = {

    KategoriComboDoldur: function () {


        Network.ajaxRequest({
            method: "post",
            url: '/Kategori/KategoriListele',
          
            success: function (data) {
                console.log(data);

                if (data.Status == 402) {
                    Toast.show({ content: "Geçersiz oturum." });
                    window.location.reload(true);
                    return;
                }

                if (data.Status == 403) {
                    Toast.show({ content: "Bu işleme yetkiniz bulunmamaktadır." });
                    return;
                }

                if (data.Status == 400) {
                    Toast.show({ content: "Lütfen tüm gerekli bilgileri doldurunuz." });
                    return;
                }

                if (data.Status != 200 && data.Status != 199) {
                    Toast.show({ content: data.Result });
                    return;
                }
                $('#inputKategori').append(
                    $('<option value=0>Kategori Seçiniz </option>'))

                $.each(data.Result, function (index, item) {
                    $('#inputKategori').append('<option  value="' + item.Id + '">' + item.KategoriAd + '</option>');

                });

                $('#inputKategori').selectpicker('refresh');
                $('#inputAltKategori').append('<option  value="0">Alt Kategori Seçiniz</option>');
                $('#inputMarka').append('<option  value="0">Alt Marka Seçiniz</option>');
                $('#inputModel').append('<option  value="0">Alt Model Seçiniz</option>');
            },
            complete: function () {

            }
        });
    },
    AltKategoriComboDoldur: function (selectedKategori) {

        inputAltKategori = $('#inputAltKategori');

        Network.ajaxRequest({
            data: 'refKategori=' + selectedKategori,
            method: "post",
            url: '/Kategori/KategoriAltListele',
            success: function (data) {
                console.log(data);

                if (data.Status == 402) {
                    Toast.show({ content: "Geçersiz oturum." });
                    window.location.reload(true);
                    return;
                }

                if (data.Status == 403) {
                    Toast.show({ content: "Bu işleme yetkiniz bulunmamaktadır." });
                    return;
                }

                if (data.Status == 400) {
                    Toast.show({ content: "Lütfen tüm gerekli bilgileri doldurunuz." });
                    return;
                }

                if (data.Status != 200 && data.Status != 199) {
                    Toast.show({ content: data.Result });
                    return;
                }
                inputAltKategori.empty();
                inputAltKategori.append('<option  value="0">Alt Kategori Seçiniz</option>');
                $.each(data.Result, function (index, item) {

                    inputAltKategori.append('<option  value="' + item.Id + '">' + item.KategoriAltAd + '</option>');
                });

                $('#inputAltKategori').selectpicker('refresh');

            },
            complete: function () {

            }
        });

    },
    KategoriMarkaComboDoldur: function (selectedKategoriAlt) {

        inputMarka = $('#inputMarka');

        Network.ajaxRequest({
            data: 'refKategoriAlt=' + selectedKategoriAlt,
            method: "post",
            url: '/Kategori/KategoriMarkaListele',
            success: function (data) {
                console.log(data);

                if (data.Status == 402) {
                    Toast.show({ content: "Geçersiz oturum." });
                    window.location.reload(true);
                    return;
                }

                if (data.Status == 403) {
                    Toast.show({ content: "Bu işleme yetkiniz bulunmamaktadır." });
                    return;
                }

                if (data.Status == 400) {
                    Toast.show({ content: "Lütfen tüm gerekli bilgileri doldurunuz." });
                    return;
                }

                if (data.Status != 200 && data.Status != 199) {
                    Toast.show({ content: data.Result });
                    return;
                }
                inputMarka.empty();
                inputMarka.append('<option  value="0">Marka Seçiniz</option>');
                $.each(data.Result, function (index, item) {

                    inputMarka.append('<option  value="' + item.refMarka + '">' + item.MarkaAd + '</option>');
                });

                $('#inputMarka').selectpicker('refresh');

            },
            complete: function () {

            }
        });

    },
    KategoriModelComboDoldur: function (selectedKategoriMarka) {

        inputModel = $('#inputModel');

        Network.ajaxRequest({
            data: 'refKategoriMarka=' + selectedKategoriMarka,
            method: "post",
            url: '/Kategori/KategoriModelListele',
            success: function (data) {
                console.log(data);

                if (data.Status == 402) {
                    Toast.show({ content: "Geçersiz oturum." });
                    window.location.reload(true);
                    return;
                }

                if (data.Status == 403) {
                    Toast.show({ content: "Bu işleme yetkiniz bulunmamaktadır." });
                    return;
                }

                if (data.Status == 400) {
                    Toast.show({ content: "Lütfen tüm gerekli bilgileri doldurunuz." });
                    return;
                }

                if (data.Status != 200 && data.Status != 199) {
                    Toast.show({ content: data.Result });
                    return;
                }
                inputModel.empty();
                inputModel.append('<option  value="0">Model Seçiniz</option>');
                $.each(data.Result, function (index, item) {

                    inputModel.append('<option  value="' + item.Id + '">' + item.ModelAd + '</option>');
                });

                $('#inputModel').selectpicker('refresh');

            },
            complete: function () {

            }
        });

    },

}