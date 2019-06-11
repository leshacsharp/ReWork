$(document).ready(function () {

    var userId = $("input[name=Id]").val();

    $.ajax({
        url: "/Employee/EmployeeProfileExists",
        type: "POST",
        data: { "employeeId": userId },
        success: function (exists) {
            if (exists) {
                var html = "<div class='form-group'><div class='col-md-offset-3 col-md-9'>" +
                    "<input type='button' class='btn btn-danger' name='delete-employee' value='Delete Employee Profile' /></div></div";

                $("#edit-user-form").append(html);
            }
        }
    });


    $("#edit-user-form").on("click", "input[name=delete-employee]", function () {
        $.ajax({
            url: "/Employee/Delete",
            type: "POST",
            data: { "employeeId": userId },
            success: function () { alert("you successfully delete employee profile") },
            error: function () { alert("you not delete employee profile") }
        });
    });
});