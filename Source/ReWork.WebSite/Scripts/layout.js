$(document).ready(function () {

    var currentCulture = $.cookie("lang");
    if (currentCulture != undefined) {
        var option = $("select").find("option[value=" + currentCulture + "]");
        option.attr("selected","selected");
    }

    $("input[name=customerProfile]").click(function () {
        var customer = $(this);
        Ajax("/profile/SetProfileOnCustomer", "POST", null, null, function () {
            customer.css("background-color", "red");
            $("input[name=freelancerProfile]").css("background-color", "white");
        })
    })

    $("input[name=freelancerProfile]").click(function () {
        var employee = $(this);
        Ajax("/profile/SetProfileOnEmployee", "POST", null, null, function () {
            employee.css("background-color", "red");
            $("input[name=customerProfile]").css("background-color", "white");
        })
    })

    function Ajax(url, type, dataType, data, success) {
        $.ajax({
            url: url,
            type: type,
            dataType: dataType,
            data: data,
            success: success
        })
    }
})