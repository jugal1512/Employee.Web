$(document).ready(function () {
    $("#btnSave").click(function () {
        ValidationForm();
    });
});

function ValidationForm() {
    if (!$("#employeeForm").valid()) {
        return false;
    }
    else {
        return true;
    }
}