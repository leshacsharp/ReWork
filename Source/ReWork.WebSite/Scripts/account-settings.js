$(document).ready(function () {

    $("input[name=create-customer]").click(function () {
        Ajax("/settings/CustomerProfileExists", "POST", "json", null, function (profileExists) {
            if (!profileExists) {
                Ajax("/profile/Customer", "POST");
                alert("customer profile created");
            }
            else {
                alert("customer profile exists");
            }
            
        })
    })


    function Ajax(url, type, dataType, data, success) {
        $.ajax({
            url: url,
            type: type,
            dataType: dataType,
            data: data,
            success:success
        })
    }
})