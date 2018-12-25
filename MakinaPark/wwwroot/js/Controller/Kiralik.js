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
        GlobalLoading.show();
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
                GlobalLoading.hide();
            }

        });
        GlobalLoading.hide();
    },
    KiralikMakinaTalepListesi: function () {


        Network.ajaxRequest({
            method: "post",
            url: '/Kiralik/KiralikMakinaTalepListesi',

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
                var layout = "";
                $.each(data.Result, function (index, item) {
                    //$('#inputKategori').append('<option  value="' + item.Id + '">' + item.KategoriAd + '</option>');
                   

                    layout += ' <div class="col-md-4 col-sm-6">  ';
                    layout += ' <div class="blog-post"> ';
                    layout += '      <div class="entry-header"> ';
                    layout += '          <div class="blog-image"> ';
                    layout += '               <div class="entry-thumbnail"> ';
                    layout += '                   <a href="blog-details.html"><img class="img-responsive" src="../client/images/blog/ekskav-out-content-a.png" alt="Blog Image"></a> ';
                    layout += '           </div> ';
                    layout += '                   <div class="time"> ';
                    layout += '                        <h2>' + item.Il+'</h2> ';
                    layout += '                    </div> ';
                    layout += '                </div> <!-- blog-image --> ';
                    layout += '     </div> ';
                    layout += '             <div class="entry-post"> ';
                    layout += '                <div class="entry-title"> ';
                    layout += '                    <h4><a href="blog-details.html">' + item.Baslik + '</a></h4> ';
                    layout += '               </div> ';
                    layout += '               <div class="post-content"> ';
                    layout += '                    <div class="entry-summary"> ';
                    layout += '                       <p>' + item.Aciklama + '</p> ';
                    layout += '                        <div class="entry-meta"> ';
                    layout += '                            <ul class="list-inline"> ';
                    layout += '                                <li>' + item.KiralamaSure + ' - ' + item.KiralamaSureTipi+'</li> ';
                    layout += '                                <li><a href="#"><i class="fa fa-user"></i>' + item.OperatorVarmiAck + '</a></li> ';
                    layout += '                                <li><a href="#"><i class="fa fa-clock"></i>' + item.KayitTarihi + '</a></li> ';
                    layout += '                           </ul> ';
                    layout += '                        </div> ';
                    layout += ' ';
                    layout += '                   </div> ';
                    layout += '               </div> ';
                    layout += '           </div><!-- entry-post --> ';
                    layout += ' </div> ';
                    layout += '    </div> ';

                });
                $("#divKiralikTalepListesi").append(layout);

            },
            complete: function () {

            }
        });
    },
}