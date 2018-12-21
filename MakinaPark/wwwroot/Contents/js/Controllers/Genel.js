var Genel = {

    IlComboDoldur: function (selectedIl) {
        inputIl = $('#inputIl');
        inputIlce = $('#inputIlce');
        var url = '/Genel/IlListesi';
        var params = "";

        Network.ajaxRequest({
            url: url,
            data: params,
            contentType: "application/json; charset=utf-8",
            processData: false,
            dataType: 'json',
            beforeSend: function () {
            },
            success: function (msg) {
                console.log(msg);
                inputIl.empty();
                inputIl.append('<option value="0">İl Seçiniz</option>');
               
                $.each(msg.Sonuc, function (index, item) {

                    inputIl.append(
                        $('<option>', {
                            value: item.Id,
                            text: item.Il
                        }, '</option>'))
                });
                $('#inputIl').selectpicker('val', selectedIl);
            },
            error: function (a, b, c) {

            },
            complete: function () {
               
            }
        });

    },
    IlceComboDoldur: function (selectedIl, selectedIlce) {

        inputIlce = $('#inputIlce');


        var url = '/Helper/IlceListesi';
        var params = JSON.stringify({
            IlId: selectedIl,
        });

        Network.ajaxRequest({
            url: url,
            data: params,
            contentType: "application/json; charset=utf-8",
            processData: false,
            dataType: 'json',
            beforeSend: function () {
            },
            success: function (msg) {
                console.log(msg);
                inputIlce.empty();
                inputIlce.selectpicker('refresh');
                inputIlce.append('<option value="0">İlçe Seçiniz</option>');
                $.each(msg.Sonuc, function (index, item) {

                    inputIlce.append(
                        $('<option>', {
                            value: item.Id,
                            text: item.IlceAdi
                        }, '</option>'))
                });
                $('#inputIlce').selectpicker('val', selectedIlce);
                inputIlce.selectpicker('refresh');
            },
            error: function (a, b, c) {

            },
            complete: function () {
               
            }
        });

    },
    //=======================================
    //              CHECK TC ID             =
    //=======================================
     checkTcNum : function (value) {
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