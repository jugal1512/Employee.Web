$(function () {
    var PlaceHolderElement = $("#PlaceHolderHere");
    $('button[data-toggle="ajax-model"]').click(function (event) {
        var url = $(this).data('url');
        var decodeUrl = decodeURIComponent(url);
        $.ajax({
            url: decodeUrl,
            type: "GET",
            processData: false,
            contentType: false,
            success: function (data) {
                PlaceHolderElement.html(data);
                PlaceHolderElement.find('.modal').modal('show');
            }
        })
    });

    PlaceHolderElement.on('click', '[data-save="modal"]', function (event) {
        debugger
        var isValidate = ValidationForm();
        if (isValidate) {
            var form = $(this).parents('.modal').find('form');
            var actionUrl = form.attr('action');
            var formData = new FormData(form[0]);
            var Image = $("#fImage")[0].files[0];
            formData.append('Image', Image);
            var id = form.attr('employeeId');
            if (id != null) {
                var url = "/employee/" + actionUrl + "/" + id;
            }
            else {
                var url = "/employee/" + actionUrl;
            }
            $.ajax({
                url: url,
                type: "POST",
                data: formData,
                processData: false,
                contentType: false,
                success: function (data) {
                    debugger
                    PlaceHolderElement.find('.modal').modal('hide');
                    Swal.fire({
                        title: "Thank You!",
                        text: "Save Successfully.",
                        icon: "success"
                    });
                    load('', '');
                }
            });
        }
    });
});