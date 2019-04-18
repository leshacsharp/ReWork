$(document).ready(function () {

    $("input[name=create-offer]").click(function () {
        var text = $("input[name=text]").val();
        var daysToImplement = Number($("input[name=daysToImplement]").val());
        var offerPayement = Number($("input[name=offerPayement]").val());
        var validation = true;

        if (text.length < 3 || text.length > 500) {
            $("#text-error").text("text should be from 3 to 500 symbols");
            validation = false;
        }

        if (isNaN(daysToImplement) || daysToImplement <= 0) {
            $("#time-error").text("days to implement should be from 1 day");
            validation = false;
        }

        if (isNaN(offerPayement) || offerPayement < 0) {
            $("#payment-error").text("offer payement should be from 0 $");
            validation = false;
        }


        if (validation) {
            var jobId = $("input[name=id]").val();

            $.ajax({
                url: "/Offer/Create",
                type: "POST",
                data: { "jobId": jobId, "text": text, "daysToImplement": daysToImplement, "offerPayement": offerPayement },
                success: function () {
                    alert("offer successfully create");
                }
            })
        }

    })

})