(function () {
    "use strict";
    
    function Toast(type, css, msg) {
        this.type = type;
        this.css = css;
        this.msg = msg;
    }

    toastr.options.closeButton = false;
    toastr.options.positionClass = 'toast-top-right';
    toastr.options.extendedTimeOut = 1000;
    toastr.options.timeOut = 5000;
    toastr.options.showDuration = 300;
    toastr.options.hideDuration = 5000;    
    toastr.options.showEasing = "swing";
    toastr.options.hideEasing = "linear";
    toastr.options.showMethod="fadeIn";
    toastr.options.hideMethod="fadeOut";
    toastr.options.onclick = null;   

    function showToast(type, msg) {               
        toastr[type](msg);       
    }


}());
