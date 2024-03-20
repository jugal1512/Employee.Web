var PlaceHolderElement = $("#PlaceHolderHere");
function onenCloseModal(url)
{
        $.ajax({
            url: url,
            type: "GET",
            processData: false,
            contentType: false,
            success: function (data) {
                PlaceHolderElement.html(data);
                PlaceHolderElement.find('.modal').modal('show');
            }
        })
    /*});*/
}

function Upsert(url) {
    PlaceHolderElement.on('click', '[data-save="modal"]', function (event) {
        var isValidate = ValidationForm();
        if (isValidate) {
            var form = $(this).parents('.modal').find('form');

            var formData = new FormData(form[0]);
            var Image = $("#fImage")[0].files[0];
            formData.append('Image', Image);
            $.ajax({
                url: url,
                type: "POST",
                data: formData,
                processData: false,
                contentType: false,
                success: function (data) {
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
}