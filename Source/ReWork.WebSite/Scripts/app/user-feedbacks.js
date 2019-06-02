$(document).ready(function () {

    SendFindReviews();

    function SendFindReviews() {
        var userId = $(".profile-short-info input[name=Id]").val();

        $.ajax({
            url: "/profile/RecivedFeedBacks",
            type: "POST",
            data: { "userId": userId },
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

        $(".reviews-summary-item-count .reviews-summary-count").html(data.length);
        var percentPositiveReviews = $(".profile-feedbacks-info-percent").html();
        $(".reviews-summary-item-percent .reviews-summary-count").html(percentPositiveReviews);

        var sumMarks = 0;
        for (var i = 0; i < data.length; i++) {
            sumMarks += data[i].QualityOfWork;
        }
        var length = data.length == 0 ? 1 : data.length;
        var avg = sumMarks / length;

        $(".reviews-summary-feature")[0].innerHTML += avg;
        $(".reviews-summary-feature")[1].innerHTML += data.length;


        for (var i = 0; i < data.length; i++) {
            var offerDate = ParseCsharpDate(data[i].AddedDate);
            var imagePath = "data:image/jpeg;base64," + data[i].SenderImagePath;

            var html = "<div class='review'><div class='row'><div class='col-md-1 col-sm-2'>" +
                "<a href='/customer/details/" + data[i].SenderId + "'><img class='review-user-photo' src='" + imagePath + "'></a></div><div class='col-md-4 col-sm-5'>" +
                "<div class='review-user-username'><a href='/customer/details/" + data[i].SenderId + "'>" + data[i].SenderName + "</a></div><div class='review-rating'>";

            for (var j = 0; j < data[i].QualityOfWork; j++) {
                html += "<span class='fa fa-star star-active'></span>";
            }

            for (var j = 0; j < 5 - data[i].QualityOfWork; j++) {
                html += "<span class='fa fa-star star'></span>";
            }

            html += "</div><div>Review to job: <a href='/job/details/" + data[i].JobId + "' class='review-job'>" + data[i].JobTitle + "</a></div></div>" +
                "<div class='col-md-7 col-sm-5'><div class='review-dop'>Added: " + offerDate + "</div>" +
                "<div class='review-text'>" + data[i].Text + "</div></div></div></div>";


            result.push(html);
        }
        return result;
    }


    function ParseCsharpDate(date) {
        var dateMs = date.replace(/[^0-9 +]/g, '');
        var dateMsInt = parseInt(dateMs);
        var fullDate = new Date(dateMsInt);
        return fullDate.toLocaleString().replace(/,/, '');
    }
})