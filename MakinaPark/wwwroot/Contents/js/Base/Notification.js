$(window).click(function () {
    toastr.clear();
});
$(document).keyup(function (e) {
    if (e.keyCode == 27) { // escape key maps to keycode `27`
        toastr.clear();
    }
});
var Notification = {
    Error: function (title, message)
    {
        toastr.options = {

            //"closeButton": true,
            //"debug": false,
            //"progressBar": true,
            //"positionClass": "toast-top-center",
            //"onclick": null,
            //"showDuration": "1000",
            //"hideDuration": "3000",
            //"timeOut": "7000",
            //"extendedTimeOut": "1000",
            //"showEasing": "swing",
            //"hideEasing": "linear",
            //"showMethod": "fadeIn",
            //"hideMethod": "fadeOut"

            //"closeButton": true,
            //"debug": false,
            //"progressBar": true,
            //"preventDuplicates": false,
            //"positionClass": "toast-top-right",
            //"onclick": null,
            //"showDuration": "10000",
            //"hideDuration": "10000",
            //"timeOut": "10000",
            //"extendedTimeOut": "10000",
            //"showEasing": "swing",
            //"hideEasing": "linear",
            //"showMethod": "fadeIn",
            //"hideMethod": "fadeOut"

            "closeButton": true,
            "debug": false,
            "progressBar": true,
            "positionClass": "toast-top-right",
            "onclick": null,
            "showDuration": "4000",
            "hideDuration": "6000",
            "timeOut": "7000",
            "extendedTimeOut": "5000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        
        }
        toastr.error(message, title);
    },
    Success: function (title, message) {
        toastr.options = {
            //"closeButton": true,
            //"debug": false,
            //"progressBar": true,
            //"positionClass": "toast-top-center",
            //"onclick": null,
            //"showDuration": "1000",
            //"hideDuration": "3000",
            //"timeOut": "7000",
            //"extendedTimeOut": "1000",
            //"showEasing": "swing",
            //"hideEasing": "linear",
            //"showMethod": "fadeIn",
            //"hideMethod": "fadeOut"

            //"closeButton": true,
            //"debug": false,
            //"progressBar": true,
            //"preventDuplicates": false,
            //"positionClass": "toast-top-right",
            //"onclick": null,
            //"showDuration": "10000",
            //"hideDuration": "10000",
            //"timeOut": "10000",
            //"extendedTimeOut": "10000",
            //"showEasing": "swing",
            //"hideEasing": "linear",
            //"showMethod": "fadeIn",
            //"hideMethod": "fadeOut"

            "closeButton": true,
            "debug": false,
            "progressBar": true,
            "positionClass": "toast-top-right",
            "onclick": null,
            "showDuration": "4000",
            "hideDuration": "6000",
            "timeOut": "7000",
            "extendedTimeOut": "5000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        }
        toastr.success(message, title);
    },
    Info: function (title, message) {
        toastr.options = {
            //"closeButton": true,
            //"debug": false,
            //"progressBar": true,
            //"positionClass": "toast-top-center",
            //"onclick": null,
            //"showDuration": "1000",
            //"hideDuration": "3000",
            //"timeOut": "7000",
            //"extendedTimeOut": "1000",
            //"showEasing": "swing",
            //"hideEasing": "linear",
            //"showMethod": "fadeIn",
            //"hideMethod": "fadeOut"

            //"closeButton": true,
            //"debug": false,
            //"progressBar": true,
            //"preventDuplicates": false,
            //"positionClass": "toast-top-right",
            //"onclick": null,
            //"showDuration": "10000",
            //"hideDuration": "10000",
            //"timeOut": "10000",
            //"extendedTimeOut": "10000",
            //"showEasing": "swing",
            //"hideEasing": "linear",
            //"showMethod": "fadeIn",
            //"hideMethod": "fadeOut"

            "closeButton": true,
            "debug": false,
            "progressBar": true,
            "positionClass": "toast-top-right",
            "onclick": null,
            "showDuration": "4000",
            "hideDuration": "6000",
            "timeOut": "7000",
            "extendedTimeOut": "5000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        }
        toastr.info(message, title);
    },
    Warning: function (title, message) {
        toastr.options = {
            //"closeButton": true,
            //"debug": false,
            //"progressBar": true,
            //"positionClass": "toast-top-center",
            //"onclick": null,
            //"showDuration": "1000",
            //"hideDuration": "3000",
            //"timeOut": "7000",
            //"extendedTimeOut": "1000",
            //"showEasing": "swing",
            //"hideEasing": "linear",
            //"showMethod": "fadeIn",
            //"hideMethod": "fadeOut"

            //"closeButton": true,
            //"debug": false,
            //"progressBar": true,
            //"preventDuplicates": false,
            //"positionClass": "toast-top-right",
            //"onclick": null,
            //"showDuration": "10000",
            //"hideDuration": "10000",
            //"timeOut": "10000",
            //"extendedTimeOut": "10000",
            //"showEasing": "swing",
            //"hideEasing": "linear",
            //"showMethod": "fadeIn",
            //"hideMethod": "fadeOut"

            "closeButton": true,
            "debug": false,
            "progressBar": true,
            "positionClass": "toast-top-right",
            "onclick": null,
            "showDuration": "4000",
            "hideDuration": "6000",
            "timeOut": "7000",
            "extendedTimeOut": "5000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        }
        toastr.warning(message, title);
    },
}