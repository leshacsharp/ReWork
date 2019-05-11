$(document).ready(function () {

    SendFindReviews();

    function SendFindReviews() {
        $.ajax({
            url: "/profile/RecivedFeedBacks",
            type: "POST",
            success: appendReviews
        })
    }

    function appendReviews(data) {
        //TODO: СДЕЛАТЬ сколько найдено items

        $(".pagination").pagination({
            dataSource: data,
            pageSize: 3,
            formatResult: FormatReviewsResult,

            callback: function (data, pagination) {
                $(".review-container").empty();
                $(".review-container").append(data);
            }
        })
    }

    function FormatReviewsResult(data) {
        var result = [];

        for (var i = 0; i < data.length; i++) {
            var offerDate = ParseCsharpDate(data[i].AddedDate);
            var imagePath = "data:image/jpeg;base64," + ArrayBufferToBase64(data[i].SenderImage);

            var html = "<div class='review'><div class='row'><div class='col-md-1 col-sm-2'>" +
                "<img class='review-user-photo' src='" + imagePath + "'> </div><div class='col-md-4 col-sm-5'>" +
                "<div class='review-user-username'>" + data[i].SenderName + "</div><div class='review-rating'>";

            for (var j = 0; j <= data[i].QualityOfWork; j++) {
                html += "<span class='fa fa-star star-active'></span>";
            }

            for (var j = 0; j < 4 - data[i].QualityOfWork ; j++) {
                html += "<span class='fa fa-star star'></span>";
            }

            html += "</div><div>Review to job:<a href='/job/details/" + data[i].JobId + "' class='review-job'>" + data[i].JobTitle + "</a></div></div>" +
                "<div class='col-md-7 col-sm-5'><div class='review-dop'>Added: " + offerDate + "</div>" +
                "<div class='review-text'>" + data[i].Text + "</div></div></div></div>";


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