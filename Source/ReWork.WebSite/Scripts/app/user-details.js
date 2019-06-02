$(document).ready(function () {

    var userId = $(".profile-short-info input[name=Id]").val();


    SendFindReviews();

    CheckProfileOnExists();


    function SendFindReviews() {
        var userId = $(".profile-short-info input[name=Id]").val();

        $.ajax({
            url: "/profile/RecivedFeedBacks",
            type: "POST",
            data: {"userId":userId},
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

    function CheckProfileOnExists() {
        var profileNameExists;
        if (window.location.href.toLowerCase().indexOf("customer") != -1) {
            profileNameExists = "Employee";
        }
        else {
            profileNameExists = "Customer";
        }

        $.ajax({
            url: "/" + profileNameExists + "/ProfileExists",
            type: "POST",
            data: { "userId": userId },
            success: function (exists) {
                if (exists) {
                    var html = "<a href='/" + profileNameExists + "/details/" + userId + "' class='btn btn-default'>" + profileNameExists + " profile</a>";
                    $(".profile-btns form").after(html);
                }
            }
        })
    }





    var userHub = $.connection.userHub;

    userHub.client.checkStatus = function (status) {

        var html;
        $(".user-status span").remove();

        if (status == 1) {
            html = "<span class='fa fa-circle' style='color:green'></span><span> Online</span>";
        }
        else {
            html = "<span class='fa fa-circle' style='color:red'></span><span> Offline</span>";
        }

        $(".user-status").append(html);
    }

    $.connection.hub.start().done(function () {
        userHub.server.checkUserStatus(userId);
    })



    function ParseCsharpDate(date) {
        var dateMs = date.replace(/[^0-9 +]/g, '');
        var dateMsInt = parseInt(dateMs);
        var fullDate = new Date(dateMsInt);
        return fullDate.toLocaleString().replace(/,/, '');
    }
})