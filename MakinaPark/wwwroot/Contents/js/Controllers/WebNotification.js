var WebNotification = {
    NotificationList: function (idPushNotification) {
        var fragmentUst = " ";
        var fragmentHar = '  <ul class="list-group"> ';
        var bildirimSayisi = 0;
        var liNotifications = $('#liNotifications');
        var url = '/Notification/NotificationList';


        Network.ajaxRequest({
            url: url,
            data: "",
            contentType: "application/json; charset=utf-8",
            processData: false,
            dataType: 'json',
            beforeSend: function () {
            },
            success: function (msg) {
                console.log(msg);

            $.each(msg.Sonuc, function (index, item) {


               
                //fragmentHar += '   <li> ';
                //fragmentHar += '      <div class="dropdown-messages-box"> ';
                //fragmentHar += '              <div class="media-body"> ';
                //fragmentHar += '                  <small class="pull-right"></small> ';
                //fragmentHar += '                <b> ' + item.BASLIK + ' </b>  <br>   ';
                //fragmentHar += '                 ' + item.MESAJ + '  ';
                //fragmentHar += '                    <br>  <small class="text-muted">' + item.KAYITTARIHI + '</small> ';
                //fragmentHar += '                 </div> ';
                //fragmentHar += '              </div> ';
                //fragmentHar += '          </li> ';

               
                fragmentHar += '   <li class="list-group-item"> ';
                fragmentHar += '        <p><a class="text-info" href="#">' + item.BASLIK + '</a> ' + item.MESAJ + '</p> ';
                fragmentHar += '        <small class="block text-muted"><i class="fa fa-clock-o"></i> ' + item.KAYITTARIHI + '</small> ';
                fragmentHar += '    </li> ';

               


                //fragmentHar += ' <li class="divider"></li> ';

                bildirimSayisi += 1;
                });


            fragmentHar += ' </ul> ';

            fragmentUst += '    <a class="dropdown-toggle count-info" data-toggle="modal" href="#modalBildirim"> ';
            fragmentUst += '      <i class="fa fa-envelope"></i>  <span class="label label-warning">' + bildirimSayisi+'</span> ';
            fragmentUst += '    </a> ';
            fragmentUst += '      <ul class="dropdown-menu dropdown-messages"> ';

            fragmentUst += fragmentHar + '            </ul > ' ;
           
            $("#liNotifications").html(fragmentUst);
            $("#divBildirim").html(fragmentHar);
                

            //liNotifications.empty().append(result);
            if (idPushNotification)
                document.getElementById('audioBeep').play();
            },
            error: function (a, b, c) {

            },
            complete: function () {

            }
        });
        return false;
    },
     ReadRequest: function (notificationIds) {
        var liNotifications = $('#liNotifications');
        $.ajax({
            url: '/Notification/ReadRequest',
            contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
            type: 'POST',
            dataType: 'html',
            data: "notificationIds=" + encodeURIComponent(notificationIds)
        }).success(function (result) {
            liNotifications.empty().append(result);
        }).error(function () {

        });
        return false;
    }
}