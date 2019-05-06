$(document).ready(function () {

    SendFindOffers();

    function SendFindOffers() {
        $.ajax({
            url: "/Customer/CustomerOffers",
            type: "POST",
            success: appendOffers
        })
    }

    function appendOffers(data) {
        //TODO: СДЕЛАТЬ сколько найдено items

        $(".pagination").pagination({
            dataSource: data,
            pageSize: 3,
            formatResult: FormatOffersResult,

            callback: function (data, pagination) {
                $("#customer-offers-container").empty();
                $("#customer-offers-container").append(data);
            }
        })
    }

    function FormatOffersResult(data) {
        var result = [];

        for (var i = 0; i < data.length; i++) {

            var html = "<div style='border:1px solid gray'>" + data[i].UserName + " - " + data[i].Text + "</div>";

            result.push(html);
        }
        return result;
    }

})