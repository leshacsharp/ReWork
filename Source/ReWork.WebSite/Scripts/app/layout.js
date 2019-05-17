$(document).ready(function () {

    var userId = $("input[name=userId]").val();


    if (userId != undefined) {

        var currentCulture = $.cookie("lang");
        if (currentCulture != undefined) {
            var option = $("select").find("option[value=" + currentCulture + "]");
            option.attr("selected","selected");
        }

        var currentProfile = $.cookie("profile");
        if (currentProfile != undefined) {
            $("button[value=" + currentProfile + "]").addClass("active");
        }

        $.ajax({
            url: "/notification/FindNotifications",
            type: "POST",
            success: function (notifications) {
                AppendFormatNotifications(notifications);
            }
        })


        var notificationHub = $.connection.notificationHub;

        notificationHub.client.refreshNotifications = function (notifications) {
            AppendFormatNotifications(notifications);
        };

        $.connection.hub.start() 
    }


    function AppendFormatNotifications(notifications) {
        $(".notifications-container").empty();

        if (notifications.length == 0) {
            var html = "<div class='empty-notification-item'><center>Empty</center></div>"
            return;
        }

        for (var i = 0; i < notifications.length; i++) {
            var notifyDate = ParseCsharpDate(notifications[i].AddedDate);

            var html = "<div class='notification-item'><input type='hidden' name='notifyId' value='" + notifications[i].Id +
                "'/><div class='notification-item-delete'><span class='fa fa-times'></span>" +
                "</div><table><tbody><tr> <td class='col-md-2 col-sm-4 hidden-xs'> <img class='notification-item-image' src='" +
                notifications[i].SenderImagePath + "'></td><td class='col-md-10 col-sm-8 col-xs-12'><div class='notificaton-item-details'>" +
                "<div class='notification-item-username'>" + notifications[i].SenderName + "</div><div class='notification-item-date'>" +
                notifyDate + "</div><div class='notification-item-text'>" + notifications[i].Text + "</div></div></td></tr></tbody></table></div>";

            $(".notifications-container").append(html);
        }
    }

    function ParseCsharpDate(date) {
        var dateMs = date.replace(/[^0-9 +]/g, '');
        var dateMsInt = parseInt(dateMs);
        var fullDate = new Date(dateMsInt);
        return fullDate.toLocaleString().replace(/,/, '');
    }
})