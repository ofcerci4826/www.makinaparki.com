var GlobalLoading = (function(){
    return{
        show: function(){
            $('body').append('<div class="og-loading-overlay"></div>');
            $('body').append('<div id="og-loading" style="display:none;">\
                                                <div id="og-loading-c">\
                                                    <div id="og-loading-logo"></div>\
                                                    <div class="og-loading-circle"></div>\
                                                    <div class="og-loading-circle-2"></div>\
                                                </div>\
                                            </div>');
            //$('body').css('filter', 'blur(5px)');

            //$('#loadingBox:last-child').css('left', ($(window).width() - $('#loadingBox').width()) / 2);
            //$('#loadingBox:last-child').css('top', ($(window).height() - $('#loadingBox').height()) / 2);

            $('#og-loading:last-child').fadeIn(300);
        },
        hide: function(){
            setTimeout(function() {
                $('.og-loading-overlay').fadeOut(200);
                $('#og-loading').fadeOut(200);

                setTimeout(function () {
                    //$('body').css('filter', 'blur(0px)');
                    $('.defaultOverlay').remove();
                    $('#og-loading').remove();
                }, 0);
            }, 200);
        }
    }
})();