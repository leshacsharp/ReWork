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
                $("tbody").empty();
                $("tbody").append(data);
            }
        })
    }

    function FormatOffersResult(data) {
        var result = [];

        for (var i = 0; i < data.length; i++) {
            var employee = "/employee/details/" + data[i].EmployeeId;
            var imagePath = "data:image/jpeg;base64," + ArrayBufferToBase64(data[i].EmployeeImage);
            var userRegDate = ParseCsharpDate(data[i].UserDateRegistration);
            var offerRegDate = ParseCsharpDate(data[i].AddedDate);

            var html = "<tr><td class='col-md-1 col-xs-3'><a href='" + employee + "'>" +
                "<img src='" + imagePath + "' class='employee-offer-photo'></a></td><td class='col-md-3 hidden-sm hidden-xs'>" +
                "<div class='employee-offer-name'><a href='" + employee + "'>" + data[i].UserName + "</a></div><div class='employee-offer-reg-date'>" +
                " Date registration: " + userRegDate + "</div> <div class='employee-offer-job-title'> <div class='employee-offer-job-title'>" +
                " <a href='/job/details/" + data[i].JobId + "''>" + data[i].JobTitle + "</a></div></td><td class='col-md-5 col-sm-9 col-xs-7'>" +
                "<span class='employee-offer-time'>" + data[i].ImplementationDays + " days</span> <span class='employee-offer-price'>" +
                data[i].OfferPayment + " $</span><span class='employee-offer-reg-date hidden-xs'>Date offer: " + offerRegDate + "</span>" +
                "<div class='employee-offer-text'>" + data[i].Text + "</div></td>" +
                "<td class='col-md-2 col-sm-2 col-xs-2'><div style='margin-top: -5px;'> <input type='submit' class='btn btn-primary accept-offer-btn' value='Accept offer'></div></td></tr>";

            result.push(html);
        }
        return result;
    }



    function ArrayBufferToBase64(buffer) {
        var binary = '';
        var bytes = new Uint8Array(buffer);
        var len = bytes.byteLength;
        for (var i = 0; i < len; i++) {
            binary += String.fromCharCode(bytes[i]);
        }
        return window.btoa(binary);
    }

    function ParseCsharpDate(date) {
        var dateMs = date.replace(/[^0-9 +]/g, '');
        var dateMsInt = parseInt(dateMs);
        var fullDate = new Date(dateMsInt);
        return fullDate.toLocaleString().replace(/,/, '');
    }

})