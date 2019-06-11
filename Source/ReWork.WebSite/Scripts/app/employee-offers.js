$(document).ready(function () {

    SendFindOffers();

    function SendFindOffers() {
        $.ajax({
            url: "/Employee/EmployeeOffers",
            type: "POST",
            success: appendOffers
        });
    };

    function appendOffers(data) {

        $(".pagination").pagination({
            dataSource: data,
            pageSize: 3,
            formatResult: FormatOffersResult,

            callback: function (data) {
                $(".search-table tbody").empty();
                $(".search-table tbody").append(data);
            }
        });
    };

    function FormatOffersResult(data) {
        var result = [];

        for (var i = 0; i < data.length; i++) {
            var jobDate = ParseCsharpDate(data[i].JobAdded);
            var offerDate = ParseCsharpDate(data[i].AddedDate);

            var html = "<tr><td class='col-md-4'><div class='offer-job-title'> <a href='/job/details/" + data[i].JobId + "'>" +
                data[i].JobTitle + "</a></div><div><span style='font-size: 120%'>Price: </span><span class='offer-job-price'>" +
                data[i].JobPrice + "$</span></div><div class='employee-offer-reg-date hidden-xs'>Date registration: " +
                jobDate + "</div></td><td class='col-md-8'><span class='employee-offer-time'>" +
                data[i].ImplementationDays + " days</span><span class='employee-offer-price'>" +
                data[i].OfferPayment + " $</span><span class='employee-offer-reg-date'>Added: " +
                offerDate + "</span> <div class='employee-offer-text'>" + data[i].Text + "</div></td></tr>";

            result.push(html);
        }

        return result;
    };


    function ParseCsharpDate(date) {
        var dateMs = date.replace(/[^0-9 +]/g, '');
        var dateMsInt = parseInt(dateMs);
        var fullDate = new Date(dateMsInt);
        return fullDate.toLocaleString().replace(/,/, '');
    };
});