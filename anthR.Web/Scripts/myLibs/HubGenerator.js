(function ($) {
    $.ajax({
        url: "/signalr/js",
        dataType: "script",
        async: false
    });
}(jQuery));