$(document).ready(function () {
    $("#btnSave").click(function () {
        debugger
        ValidationForm();
    });
});

function ValidationForm() {
    debugger
    if (!$("#employeeForm").valid()) {
        return false;
    }
    else {
        return true;
    }
}