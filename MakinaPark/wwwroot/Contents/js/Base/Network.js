var INTERNET_CONNECTED = true;
var SUCCESS = 'SUCCESS';

function isNullOrUndefined(param) {
    return (param == undefined || typeof (param) == 'undefined' || param == null || param == '');
}

var Network = (function () {
    return {
        isOnline: function () {
            return INTERNET_CONNECTED;
        },
        ajaxRequest: function (params) {
            var CONFIG = {
                method: "post",
                url: '',
                data: '',
                dataType: "json",
                contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
                timeout: 120000,
                
                cache: false,
                processData: false,
                global: false,
                beforeSend: function () { },
                success: function (msg) { },
                complete: function () { },
                error: function (a, b, c) { console.log(a, b, c); }
            };
            if (params) { $.extend(CONFIG, params); }

            if (!Network.isOnline()) {
                CONFIG.error();
                CONFIG.complete();
                return;
            }

            $.ajax({
                async: false,
                type: CONFIG.method,
                url: CONFIG.url,
                data: CONFIG.data,
                timeout: CONFIG.timeout,
                dataType: CONFIG.dataType,
                contentType: CONFIG.contentType,
                processData: CONFIG.processData,
                global: CONFIG.global,
                cache: CONFIG.cache,
                beforeSend: function (jqXHR) {
                    jqXHR.setRequestHeader('X-Requested-With', 'XMLHttpRequest');
                    console.log(CONFIG.data);
                    //Network.showLoader();
                    if (typeof (CONFIG.beforeSend) == "function")
                        CONFIG.beforeSend(this);
                },
                success: function (msg) {

                    // session dolmuş, logine yönlendir
                    if (!isNullOrUndefined(msg) && msg.Durum == 403) {
                        window.location.href = "/Error/Forbidden";
                        return;
                    }
                    if (isNullOrUndefined(msg)) {
                        //Notification.Error("Hata", "Giriş Başarısız!");
                        //Network.hideLoader();

                        return;
                    }

                    if (msg.Durum != 200) {
                        //Notification.Error("Hata", msg.Sonuc);
                        //alert("Hata", msg.Sonuc);
                        //Network.hideLoader();
                        console.log(msg.Sonuc)
                        //swal({
                        //    title: "Hata",
                        //    text: msg.Sonuc,
                        //    type: "error",
                        //    showCancelButton: false,
                        //    confirmButtonColor: "#DD6B55",
                        //    confirmButtonText: "Tamam!",
                        //    closeOnConfirm: true
                        //});

                        
                        return;
                    }

                    if (typeof (CONFIG.success) == "function")
                        CONFIG.success(msg);
                },
                error: function (a, b, c) {
                    if (typeof (CONFIG.error) == "function")
                        CONFIG.error(a, b, c);
                    //Network.hideLoader();
                    //Notification.Error('Hata', 'İşleminiz yapılırken bir hata meydana geldi. Lütfen daha sonra tekrar deneyiniz! ---- ' + a + '' + b + '' + c + '');
                },
                complete: function () {
                    if (typeof (CONFIG.complete) == "function")
                        CONFIG.complete();
                }
            });
        },
        showLoader: function () {
            var $modalDialog = $('.modal-dialog'),
            modalHeight = $modalDialog.height(),
            browserHeight = window.innerHeight;
            $modalDialog.css({ 'margin-top': modalHeight >= browserHeight ? 0 : (browserHeight - modalHeight) / 4 });
            $('#loadingModal').modal({ backdrop: 'static', keyboard: false });
            $('#loadingModal').appendTo("body");
            $('#loadingModal').modal('show');
        },
        hideLoader: function () {
            setTimeout(Network.hideLoaderSyc, 100);
        },
        hideLoaderSyc: function () {
            $('#loadingModal').modal('hide');
        },
        formatDate: function (date) {
            //var hours = date.getHours();
            //var minutes = date.getMinutes();
            //var ampm = hours >= 12 ? 'pm' : 'am';
            //hours = hours % 12;
            //hours = hours ? hours : 12; // the hour '0' should be '12'
            //minutes = minutes < 10 ? '0' + minutes : minutes;
            //var strTime = hours + ':' + minutes + ' ' + ampm;
            return date.getDate() + "." + (date.getMonth() + 1) + "." + date.getFullYear() + "";//" + strTime;
        },
        addCommas: function (nStr) {
            nStr += '';
            x = nStr.split('.');
            x1 = x[0];
            x2 = x.length > 1 ? ',' + x[1] : '';
            var rgx = /(\d+)(\d{3})/;
            while (rgx.test(x1)) {
                x1 = x1.replace(rgx, '$1' + '.' + '$2');
            }
            return x1 + x2;
        },
        qString: function (name, url) {
            if (!url) url = window.location.href;
            name = name.replace(/[\[\]]/g, "\\$&");
            var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
                results = regex.exec(url);
            if (!results) return null;
            if (!results[2]) return '';
            return decodeURIComponent(results[2].replace(/\+/g, " "));
        },
        currencyFormat: function (n, currency) {
            return parseFloat(n).toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, "$1,");
        }
    };
})();