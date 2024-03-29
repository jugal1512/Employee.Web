﻿$(document).ready(function () {
    load('','');
    $('#btnsearch').click(function (e) {
        e.preventDefault();
        var searchString = $('#inputSearch').val();
        var sortOrder = $('#sortOrder').val();
        load(searchString, sortOrder);
    });

    $('#sortName').click(function (e) {
        e.preventDefault();
        var sortOrder = $('#sortOrder').val() === "name_desc" ? "" : "name_desc";
        $('#sortOrder').val(sortOrder)
        load($('#inputSearch').val(), sortOrder);
    });
});
function load(searchString, sortOrder) {
    $.ajax({
        url: "employee/getallemployees",
        type: 'GET',
        data: { searchString: searchString, sortOrder: sortOrder },
        dataType: 'json',
        success: function (result) {
            $('#paginationTable').pagination({
                dataSource: result,
                pageSize: 5,
                pageRange: null,
                callback: function (result, pagination) {
                    let employeeData = "";
                    $('#tblEmployee tbody').empty();
                    result.forEach(function (employee) {
                        employeeData += "<tr>";
                        employeeData += "<td><button class='btn btnExpand' type='button'><i class='fa-solid fa-circle-plus'></i></button></td>";
                        employeeData += "<td>" + '<img class="rounded-circle profileImage" src="uploads/' + employee.image + '" alt="" height="50px" width="50px" />' + "</td>";
                        employeeData += "<td>" + employee.firstName + " " + employee.lastName + "</td>";
                        employeeData += "<td>" + employee.email + "</td>";
                        employeeData += "<td>" + employee.designation + "</td>";
                        employeeData += "<td>" + employee.gender + "</td>";
                        employeeData += "<td><button type='button' class='btn bg-primary p-2 border-0' onclick=onenCloseModal('/employee/UpsertModel/" + employee.id +"')><i class='fa-solid fa-pen-to-square' style='color: #e2bd03;'>Model</i></button><a href='employee/Upsert/" + employee.id + "' class='btn bg-primary mx-2'><i class='fa-solid fa-pen-to-square' style='color: #e2bd03;'></i></a>            <a class='btn bg-primary' onclick=deleteSweetAlert('/employee/delete/" + employee.id + "')><i class='fa-solid fa-trash' style='color: #e00b0b;'></i></a></td>";
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
                    var PlaceHolderElement = $("#PlaceHolderHere");
                }
            });
        }
    });
}