$(document).ready(function () {

    var userId = $("input[name=userId]").val();

    $("input[name=delete-customer]").click(function () {

        $.ajax({
            url: "/Customer/Delete",
            type: "POST",
            data: { "id": userId },
            success: function () { alert("you successfully delete customer profile") },
            error: function () { alert("you not delete customer profile") }

        })
    })

    $("input[name=delete-employee]").click(function () {

        $.ajax({
            url: "/Employee/Delete",
            type: "POST",
            data: { "id": userId },
            success: function () { alert("you successfully delete employee profile") },
            error: function () { alert("you not delete employee profile") }

        })
    })
})