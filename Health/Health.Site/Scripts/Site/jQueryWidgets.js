$(document).ready(function () {
    $.datepicker.setDefaults(
        $.extend($.datepicker.regional["ru"])
    );
    $(".datepicker").datepicker(
        { 
            dateFormat: "dd.mm.yy",
            showWeek: true,
            firstDay: 1
        });
});