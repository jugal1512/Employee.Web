$(document).ready(function () {
    load();
});

function load() {
    $.ajax({
        url: "employee/getallemployees",
        type: 'GET',
        dataType: 'json',
        success: function (response, index) {
            $('#paginationTable').pagination({
                dataSource: response,
                pageSize: 5,
                pageRange: null,
                showPageNumbers: true,
                callback: function (response, pagination) {
                    let employeeData = "";
                    $('#tblEmployee tbody').empty();
                    response.forEach(function (employee) {
                        employeeData += "<tr>";
                        employeeData += "<td><button class='btn btnExpand' type='button'><i class='fa-solid fa-circle-plus'></i></button></td>";
                        employeeData += "<td>" + '<img class="rounded-circle profileImage" src="uploads/' + employee.image + '" alt="" height="50px" width="50px" />' + "</td>";
                        employeeData += "<td>" + employee.firstName + " " + employee.lastName + "</td>";
                        employeeData += "<td>" + employee.email + "</td>";
                        employeeData += "<td>" + employee.designation + "</td>";
                        employeeData += "<td>" + employee.gender + "</td>";
                        employeeData += "<td>" + '<button id="' + employee.id + '" class="btn btnUpdate"><i class="fa-solid fa-pen" style="color: #e6c805;"></i></button> <button type="button" id="' + employee.id + '" class="btn btnDelete ms-3" ><i class="fa-solid fa-trash" style="color: #e30d0d;"></i></button> ' + "</td>";
                        employeeData += "</tr>";
                        employeeData += "<tr class='hiddenRow' style='display: none;'>";
                        employeeData += "<td></td>";
                        employeeData += "<td><strong>Description:</strong> " + employee.description + "</td>";
                        employeeData += "<td><strong>DOB:</strong> " + employee.dob + "</td>";
                        employeeData += "<td><strong>Joining Date:</strong> " + employee.joiningDate + "</td>";
                        employeeData += "<td><strong>Skills:</strong> " + employee.skillName + "</td>";
                        employeeData += "</tr>";
                    });
                    $('#tblEmployee tbody').append(employeeData).html();
                }
            });
        }
    });
}