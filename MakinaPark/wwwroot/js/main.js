$(document).ready(function() {
    $(document).on('click', '.menu-item', function () {
        var $this = $(this);

        if ($this.find('a').attr('href') == '#') {

            $('.sub-menu').slideUp(300);

            $this.find('.sub-menu').slideDown(300);
            
            setTimeout(function () {
                $('.menu-item').removeClass('selected');
                $this.toggleClass('selected');
            }, 300);
            
        }
    });

    

    $(document).on('keyup', '.pushNotificationTitle', function () {
        var value = $(this).val();

        if (value.length == 0)
            value = strings.notificationTitle;

        $('#pushNotificationTitle').text(value);
    });

    $(document).on('keyup', '.pushNotificationMessage', function () {
        var value = $(this).val();

        if (value.length == 0)
            value = strings.notificationMessage;

        $('#pushNotificationMessage').text(value);
    });

    $(document).on('submit', '#akaryakit-olustur-form', function () {
        var $form = $(this);

        GlobalLoading.show();

        Network.ajaxRequest({
            type: 'POST',
            url: $form.attr('action'),
            data: $form.serialize(),
            success: function (data) {
                console.log(data);

                if (window.grecaptcha)
                    grecaptcha.reset();

                if (data.Status != 200)
                    $form[0].reset();

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

                if (data.Status != 200) {
                    Toast.show({ content: data.Description });
                    return;
                }

                $form.find('input[name="Toplam"]').val(new String(data.Result.Toplam).replace('.', ','));
                $form.find('input[name="Kalan"]').val(new String(data.Result.Kalan).replace('.', ','));
                $form.find('input[name="Kullanilan"]').val(new String(data.Result.Kullanilan).replace('.', ','));
                $form.find('input[name="Dagitilan"]').val(new String(data.Result.Dagitilan).replace('.', ','));

                Network.ajaxRequest({
                    type: 'POST',
                    url: '',
                    data: $form.serialize(),
                    success: function (data) {
                        console.log(data);

                        if (data.Status != 200) {
                            Toast.show({ content: data.Result });
                            return;
                        }

                        location.href = $('#returnUrl').val();
                    }
                });
            }
        });

        return false;
    });

    $(document).on('submit', '.default-form', function () {
        var message = '@Resources.Resources.msg_dil';
        alert(message);
        var $form = $(this);
        var resetOnFail = ("true" == $form.attr("data-reset"));
        var returnUrl = $('#returnUrl').val();

        GlobalLoading.show();

        Network.ajaxRequest({
            type: $form.attr("method"),
            url: $form.attr("action"),
            data: $form.serialize(),
            success: function (data) {
                console.log(data);

                if (window.grecaptcha)
                    grecaptcha.reset();

                if (data.Status != 200 && data.Status != 199 && resetOnFail)
                    $form[0].reset();

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
                    if (data.Result != null && typeof (data.Result) == 'object') {
                        Toast.show({ content: data.Result.ErrorMessage });
                        return;
                    }

                    Toast.show({ content: data.Result });
                    return;
                }

                if (data.Status == 199)
                    returnUrl += (returnUrl.indexOf("?") >= 0 ? '&id=' + data.Id : '?id=' + data.Id);

                Toast.show({ content: "İşleminiz başarılı." });

                if (returnUrl && returnUrl.length > 0)
                    location.href = returnUrl;
            }
        });

        return false;
    });

    $(document).on('submit', '#logdin-form', function () { 
        var $form = $(this);
        //var resetOnFail = ("true" == $form.attr("data-reset"));
        var returnUrl = "";//$('#returnUrl').val();

        GlobalLoading.show();

        Network.ajaxRequest({
            type: $form.attr("method"),
            url: $form.attr("action"),
            data: $form.serialize(),
            success: function (data) {
                console.log(data);

                if (window.grecaptcha)
                    grecaptcha.reset();

                if (data.Status != 200 && data.Status != 199 && resetOnFail)
                    $form[0].reset();

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
                    if (data.Result != null && typeof (data.Result) == 'object') {
                        Toast.show({ content: data.Result.ErrorMessage });
                        return;
                    }

                    Toast.show({ content: data.Result });
                    return;
                }

                if (data.Status == 199)
                    returnUrl += (returnUrl.indexOf("?") >= 0 ? '&id=' + data.Id : '?id=' + data.Id);
               
                //if (data.Eposta != '') {
                //    returnUrl = "User/Login?Eposta=" + data.Eposta + "Parolar=" + data.Parola;
               // }

                Toast.show({ content: "İşleminiz başarılı." });

                //if (returnUrl && returnUrl.length > 0)
                   // location.href = returnUrl;
            }
        });

        return false;
    });

    $(document).on("click", ".default-delete", function () {
        var $self = $(this);
        var url  = $self.attr("data-url");
        var name = $self.attr("data-name");

        if (!confirm("\"" + name + "\" kaydı ve bu kayda ilişkin tüm bilgiler silinecektir, devam etmek istediğinizden emin misiniz?"))
            return false;

        GlobalLoading.show();

        Network.ajaxRequest({
            type: 'POST',
            url: url,
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
                    if (data.Result != null && typeof (data.Result) == 'object') {
                        Toast.show({ content: data.Result.ErrorMessage });
                        return;
                    }

                    Toast.show({ content: data.Result });
                    return;
                }

                $self.parents('tr').eq(0).remove();

                Toast.show({ content: name + " kaydı başarıyla silindi." });

                window.location.reload(true);
            }
        });

        return false;
    });

    $(document).on('click', '.musteri-row', function (e) {
        if ($(e.target).is('.action'))
            return false;

        window.location.href = $(this).attr("data-url");
    });
});

function getChartConfig(type, title, datas, bcolors, labels, displayTitle = true, legendPosition = 'left') {
    return config = {
        type: type,
        data: {
            datasets: [{
                data: datas,
                backgroundColor: bcolors,
                label: 'Limit'
            }],
            labels: labels
        },
        options: {
            responsive: true,
            legend: {
                position: legendPosition,
            },
            title: {
                display: displayTitle,
                text: title
            },
            animation: {
                animateScale: true,
                animateRotate: true
            }
        }
    };
}

function detectCardType(number) {
    number = number.replace(/ /g, '').replace(/\*/g, '1');

    var re = {
        electron: /^(4026|417500|4405|4508|4844|4913|4917)\d+$/,
        maestro: /^(5[06789]|6)[0-9]{0,}$/,
        dankort: /^(5019)\d+$/,
        interpayment: /^(636)\d+$/,
        unionpay: /^(62|88)\d+$/,
        visa: /^4[0-9]{0,}$/,
        mastercard: /^(5[1-5]|222[1-9]|22[3-9]|2[3-6]|27[01]|2720)[0-9]{0,}$/,
        amex: /^3[47][0-9]{0,}$/,
        diners: /^3(?:0[0-59]{1}|[689])[0-9]{0,}$/,
        discover: /^(6011|65|64[4-9]|62212[6-9]|6221[3-9]|622[2-8]|6229[01]|62292[0-5])[0-9]{0,}$/,
        jcb: /^(?:2131|1800|35)[0-9]{0,}$/
    }

    console.log(number);

    for (var key in re) {
        if (re[key].test(number)) {
            return key
        }
    }
}