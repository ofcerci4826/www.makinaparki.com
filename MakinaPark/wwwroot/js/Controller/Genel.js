var Genel = {

    IlComboDoldur: function (selectedIl) {
      

        Network.ajaxRequest({
            method: "post",
            url: '/Genel/IlListesi',
           
            success: function (data) {
                //console.log(data);

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
               
                $('#inputIl').append(
                    $('<option value="">Şehir Seçiniz </option>'))

                $.each(data.Result, function (index, item) {
                    $('#inputIl').append('<option  value="' + item.Id + '">' + item.Il + '</option>');
              
                });
                $('#inputIl').selectpicker('refresh');
                $('#inputIlce').append(
                    $('<option >İlçe Seçiniz </option>'))
                $('#inputVergiDairesi').append(
                    $('<option >Vergi Dairesi Seçiniz </option>'))
            },
            complete: function () {
                
            }
        });
    },
    IlceComboDoldur: function (selectedIl) {
        
        inputIlce = $('#inputIlce');

        var params = {
            refIl: selectedIl,
        };

        Network.ajaxRequest({
            data: params,
            method: "post",
            url: '/Genel/IlceListesi',
            success: function (data) {
               // console.log(data);

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
                inputIlce.empty();
                inputIlce.append('<option value="">İlçe Seçiniz</option>');
                $.each(data.Result, function (index, item) {
                    

                    inputIlce.append('<option  value="' + item.Id + '">' + item.IlceAd +'</option>');
                });
                
                $('#inputIlce').selectpicker('refresh');

            },
            complete: function () {

            }
        });

    },
    VergiDairesiComboDoldur: function (selectedIl) {

        inputIlce = $('#inputVergiDairesi');

        var params = {
            refIl: selectedIl,
        };

        Network.ajaxRequest({
            data: params,
            method: "post",
            url: '/Genel/VergiDairesiListesi',
            success: function (data) {
                //console.log(data);

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
                inputIlce.empty();
                inputIlce.append('<option value="">Vergi Dairesi Seçiniz</option>');
                $.each(data.Result, function (index, item) {


                    inputIlce.append('<option  value="' + item.Id + '">' + item.Ad + '</option>');
                });

                $('#inputVergiDairesi').selectpicker('refresh');

            },
            complete: function () {

            }
        });

    },


    //=======================================
    //              CHECK TC ID             =
    //=======================================
    checkTcNum: function (value) {
        value = value.toString();
        var isEleven = /^[0-9]{11}$/.test(value);
        var totalX = 0;
        for (var i = 0; i < 10; i++) {
            totalX += Number(value.substr(i, 1));
        }
        var isRuleX = totalX % 10 == value.substr(10, 1);
        var totalY1 = 0;
        var totalY2 = 0;
        for (var i = 0; i < 10; i += 2) {
            totalY1 += Number(value.substr(i, 1));
        }
        for (var i = 1; i < 10; i += 2) {
            totalY2 += Number(value.substr(i, 1));
        }
        var isRuleY = ((totalY1 * 7) - totalY2) % 10 == value.substr(9, 0);
        return isEleven && isRuleX && isRuleY;
    }
    //window.checkTcNum = checkTcNum;
    //--------  End of check tc id  ---------//
}