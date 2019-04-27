$(document).ready(function () {

    $("input[name=create-customer]").click(function () {
        $.ajax({
            url: "/customer/create",
            type: "POST",
            success: function (data) {
                alert("you successfully create customer profile")
            }
        })
    })

})