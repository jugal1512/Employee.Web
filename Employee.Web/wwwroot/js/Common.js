// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

listItem = [];
$(document).ready(function () {
    $("#btnAdd").click(function () {
        let inputSkill = $('#txtSkill').val();
        $('#skillAdded').append('<li class="skillLists">' + inputSkill + ' <i class="fa-solid fa-xmark"></i> </li>');
        listItem.push(inputSkill);
        $('#txtSkill').val("");
        hiddenSkill();
        $('#skillAdded').on('click', '.fa-xmark', function () {
            let removeSkill = $(this).parent('li').text().trim();
            listItem = listItem.filter(skill => skill != removeSkill);
            $(this).parent('li').remove();
            hiddenSkill();
        });
    });
    $(document).on('click', '.btnExpand', function () {
        var hiddenRow = $(this).closest('tr').next('.hiddenRow');
        hiddenRow.toggle();
        let icon = $(this).find('i');
        icon.toggleClass('fa-circle-plus fa-circle-minus');
    });
});

function updateSkills(item) {
    $('#skillAdded').append('<li class="skillLists">' + item + ' <i class="fa-solid fa-xmark"></i> </li>');
    listItem.push(item);
    $('#txtSkill').val("");
    hiddenSkill();
    $('#skillAdded').on('click', '.fa-xmark', function () {
        let removeSkill = $(this).parent('li').text().trim();
        listItem = listItem.filter(skill => skill != removeSkill);
        $(this).parent('li').remove();
        hiddenSkill();
    });
}

function hiddenSkill() {
    var hiddenSkill = $("#SkillName").val(listItem);
}
function deleteSweetAlert(url)
{
    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    window.location = "/employee/index";
                    toastr.success(data.message);
                }
            })
        }
    });
}

$(document).ready(function () {
    $("#fImage").change(function () {
        const [file] = fImage.files;
        if (file) {
            previewImage.src = URL.createObjectURL(file);
        }
    });
});

