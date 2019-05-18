$(document).ready(function () {

    var userAuthenticated = $("input[name=userId]").val() != undefined ? true : false;


    if (userAuthenticated) {

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
            var parseNotifications = $.parseJSON(notifications);

            AppendFormatNotifications(parseNotifications);
        };

        $.connection.hub.start() 
    }

    $("#notifications-container").on("click", ".notification-item-delete", function () {
        var notifyItem = $(this).parent();
        var notifyId = notifyItem.find("input[name=notifyId]").val();

        $.ajax({
            url: "/notification/Delete",
            type: "POST",
            data: { "notifyId": notifyId },
            success: function () {
                notifyItem.remove();

                var countNotifications = $(".notification-item");

                $(".notifications-counter").html(countNotifications);


                if (countNotifications.length == 0) {
                    var html = "<div class='empty-notification-item'><center>Empty</center></div>";
                    $("#notifications-container").append(html);
                }
            }
        })
    })

    $(".notification-delete-all").click(function () {

        var countNotifications = $(".notification-item");

        if (countNotifications.length > 0) {

            var counter = Number($(".notifications-counter").html());
            $(".notifications-counter").html(--counter);

            $.ajax({
                url: "/notification/DeleteAll",
                type: "POST",
                success: function () {
                    var html = "<div class='empty-notification-item'><center>Empty</center></div>";
                    $("#notifications-container").append(html);
                }
            })
        }
    })

    function AppendFormatNotifications(notifications) {

        $(".notifications-counter").html(notifications.length);

        $("#notifications-container").empty();

        if (notifications.length == 0) {
            var html = "<div class='empty-notification-item'><center>Empty</center></div>";
            $("#notifications-container").append(html);
            return;
        }

        for (var i = 0; i < notifications.length; i++) {
           // var notifyDate = ParseCsharpDate(notifications[i].AddedDate);
            var imagePath = "data:image/jpeg;base64," + notifications[i].SenderImagePath;

            var html = "<div class='notification-item'><input type='hidden' name='notifyId' value='" + notifications[i].Id +
                "'/><div class='notification-item-delete'><span class='fa fa-times'></span>" +
                "</div><table><tbody><tr> <td class='col-md-2 col-sm-4 hidden-xs'> <img class='notification-item-image' src='" +
                imagePath + "'></td><td class='col-md-10 col-sm-8 col-xs-12'><div class='notificaton-item-details'>" +
                "<div class='notification-item-username'>" + notifications[i].SenderName + "</div><div class='notification-item-date'>28.01</div>" +
                "<div class='notification-item-text'>" + notifications[i].Text + "</div></div></div></td></tr></tbody></table></div>";

            $("#notifications-container").append(html);
        }
    }

    //function ParseCsharpDate(date) {
    //    var dateMs = date.replace(/[^0-9 +]/g, '');
    //    var dateMsInt = parseInt(dateMs);
    //    var fullDate = new Date(dateMsInt);
    //    return fullDate.toLocaleString().replace(/,/, '');
    //}
})