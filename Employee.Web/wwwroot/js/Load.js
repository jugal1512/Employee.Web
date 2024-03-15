﻿$(document).ready(function () {
    load();
});

function load() {
    debugger
    $.ajax({
        url: "employee/getallemployees",
        type: 'GET',
        dataType: 'json',
        success: function (response, index) {
            $('#paginationTable').pagination({
                debugger
                dataSource: response,
                pageSize: 5,
                pageRange: null,
                showPageNumbers: true,
                callback: function (response, pagination) {
                    debugger
                    let employeeData = "";
                    $('#tblEmployee tbody').empty();
                    response.forEach(function (employee) {
                        debugger
                        employeeData += "<tr>";
                        employeeData += "<td><button class='btn btnExpand' type='button'><i class='fa-solid fa-circle-plus'></i></button></td>";
                        employeeData += "<td>" + '<img class="rounded-circle profileImage" src="uploads/' + employee.image + '" alt="" height="50px" width="50px" />' + "</td>";
                        employeeData += "<td>" + employee.firstName + " " + employee.lastName + "</td>";
                        employeeData += "<td>" + employee.email + "</td>";
                        employeeData += "<td>" + employee.designation + "</td>";
                        employeeData += "<td>" + employee.gender + "</td>";
                        employeeData += "<td><a class='btn bg - primary' onclick=deleteSweetAlert('/employee/delete/"+employee.id+"')><i class='fa-solid fa-trash' style='color: #e00b0b;'></i></a></td>";
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