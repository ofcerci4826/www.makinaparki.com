var INTERNET_CONNECTED = true;
var ACTIVE_AJAX_REQUESTS = [];

document.addEventListener('online', function(){ INTERNET_CONNECTED = true; }, false);
document.addEventListener('offline', function(){ INTERNET_CONNECTED = false; }, false);

var Network = (function(){
    return {
        isOnline: INTERNET_CONNECTED,
        ajaxRequest: function(params, abortRequests){
            if(typeof(abortRequests) === 'undefined')
                abortRequests = true;
            
            if(!Network.isOnline)
            {
                hideLoadingAnimation();
                Toast.show({ content: 'Veriler alınamadı, lütfen internet bağlantınızı kontrol edin.' });

                return;
            }
                
            var OPTIONS = {
                async           : false,
                type            : 'GET',
                dataType        : 'json',
                data            : '',
                success         : function(data){ },
                cache           : false,
                error           : function(jqXHR, textStatus, error){
                    if(error == 'abort')
                        return;
                    
                    Toast.show({ content: 'Veriler alınırken bir hata oluştu. Lütfen tekrar deneyin.' });
                    GlobalLoading.hide();
                },
                beforeSend      : function(jqXHR){
                    if(!abortRequests)
                        return;
                    
                    var counter = 0;

                    $(ACTIVE_AJAX_REQUESTS).each(function () {
                        this.abort();
                        counter++;
                    });

                    $(ACTIVE_AJAX_REQUESTS).each(function () {
                        ACTIVE_AJAX_REQUESTS.pop();
                    });

                    ACTIVE_AJAX_REQUESTS.push(jqXHR);
                },
                complete: function () {
                    GlobalLoading.hide();
                }
            };

            if (params)
                $.extend(OPTIONS, params);
            
            OPTIONS.success = function(data){                
                if(params.success)
                    params.success(data);
            }
            
            $.ajax(OPTIONS);
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
    }
})();

