﻿
Dropzone.options.mydropzone = {
    url: "/account/uploadimage",
    paramName: "userphoto",
    uploadMultiple: true,
    maxFilesize: 2,
    maxFiles: 1,
    acceptedFiles: ".png,.jpg,.jpeg",
    parallelUploads: 100,
    addRemoveLinks: true,
    clickable: true,
    init: function () {
        this.on("maxfilesexceeded", function (file) { this.removeAllFiles(); this.addFile(file); });
    },
}


$(document).ready(function () {

    $("#employee-section").on("click", "input[name=employee-delete]", function () {

        var employeeId = $("input[name=userId]").val();
        $(this).parent().append("<input type='hidden' name='employeeId' value='" + employeeId + "'>");
    })

    $.ajax({
        url: "/employee/EmployeeProfileExists",
        type: "POST",
        dataType: "json",
        success: function (exists) {
            var html;
            if (exists) {
                html = "<form action='/employee/delete' method='POST'><input type='submit' value='delete' name='employee-delete' class='btn btn-danger'/></form>";
            }
            else {
                html = "<a href='/employee/create'><input type='button' value='create' class='btn btn-success'/></a>";
            }
            $("#employee-section").append(html);
        }
    })
})
