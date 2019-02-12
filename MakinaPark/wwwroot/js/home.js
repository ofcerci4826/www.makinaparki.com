var delay = (function(){
    var timer = 0;

    return function(callback, ms){
        clearTimeout (timer);
        timer = setTimeout(callback, ms);
    };
})();


$(document).ready(function () {
    $(document).on('submit', '.login-form', function () {
        var $this = $(this);
        var url = $this.attr('action');
                $.ajax({
            type: 'POST',
            url: url,
            data: $this.serialize(),
            dataType: 'json',
            success: function (data) {
                if (data.Status != 200) {
                    Toast.show({ content: data.Result });
                    //alert();
                    return;
                }

                location.reload();
            }
        });

        return false;
    });

    $(document).on('click', 'body', function(e){
        if($('.user-login-w').hasClass('visible') 
            && !$(e.target).is('.user-login-w') 
            && !$(e.target).is('a.sign-in') 
            && $(e.target).parents('.user-login-w').length == 0
            )
                $('.user-login-w').removeClass('visible');
    });

    $(document).on('click', '.user-nav-menu ul li a.sign-in', function(){
        if(!$('.user-login-w').hasClass('visible'))
            $('.user-login-w').addClass('visible');
    });
});