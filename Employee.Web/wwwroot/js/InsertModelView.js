$(function () {
    var PlaceHolderElement = $("#PlaceHolderHere");
    $('button[data-toggle="ajax-model"]').click(function (event) {
        var url = $(this).data('url');
        var decodeUrl = decodeURIComponent(url);
        $.get(decodeUrl).done(function (data) {
            PlaceHolderElement.html(data);
            PlaceHolderElement.find('.modal').modal('show');
        });
    });

    PlaceHolderElement.on('click', '[data-save="modal-insert"]', function (event) {
        var form = $(this).parents('.modal').find('form');
        var actionUrl = form.attr('action');
        var url = "/employee/" + actionUrl;
        var sendData = form.serialize();
        $.post(url, sendData).done(function (data) {
            window.location = "/employee/index";
            PlaceHolderElement.find('.modal').modal('hide');
        });
    });

    PlaceHolderElement.on('click', '[data-save="modal-update"]', function (event) {
        var form = $(this).parents('.modal').find('form');
        var actionUrl = form.attr('action');
        var id = form.attr('employeeId');
        var url = "/employee/" + actionUrl + "/" + id ;
        var sendData = form.serialize();
        $.post(url, sendData).done(function (data) {
            window.location = "/employee/index";
            PlaceHolderElement.find('.modal').modal('hide');
        });
    });
});