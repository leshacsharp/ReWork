$(document).ready(function () {


    var currentCulture = $.cookie("lang");
    if (currentCulture != undefined) {
        var option = $("select").find("option[value=" + currentCulture + "]");
        option.attr("selected", "selected");
    }


    var userId = $("input[name=userId]").val();

    if (userId != undefined) {

        var currentProfile = $.cookie("profile");
        if (currentProfile != undefined) {
            $("button[value=" + currentProfile + "]").addClass("active");
        }


        //change-profile
        $("button[value=Employee]").click(function () {
            var selectedProfile = $(this).val();

            $.ajax({
                url: "/employee/ProfileExists",
                type: "POST",
                data: { "userId": userId },
                success: function (exists) {
                    if (exists) {
                        $.post("/profile/changeprofiletype", { "profile": "Employee" }, function () {
                            ChangeProfileType(selectedProfile);
                        })
                    }
                    else {
                        $("#not-exists-emloyee").modal("show");
                    }
                }
            })
        })

        //change-profile
        $("button[value=Customer]").click(function () {
            var selectedProfile = $(this).val();
            ChangeProfileType(selectedProfile);
        })

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

                    $(".notifications-counter").html(countNotifications.length);


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


        $.ajax({
            url: "/notification/FindNotifications",
            type: "POST",
            success: function (notifications) {
                AppendFormatNotifications(notifications);
            }
        })
    }




    var notificationHub = $.connection.notificationHub;
    var userHub = $.connection.userHub;

    notificationHub.client.refreshNotifications = function (notifications) {
        var parseNotifications = $.parseJSON(notifications);

        AppendFormatNotifications(parseNotifications);
    };

    userHub.client.refreshUsersCounter = function (count) {
        $(".users-count > .counter").html(count);
    }

    $.connection.hub.start();




    function AppendFormatNotifications(notifications) {

        $(".notifications-counter").html(notifications.length);

        $("#notifications-container").empty();

        if (notifications.length == 0) {
            var html = "<div class='empty-notification-item'><center>Empty</center></div>";
            $("#notifications-container").append(html);
            return;
        }

        for (var i = 0; i < notifications.length; i++) {

            var notifyDate = ParseCsharpDate(notifications[i].AddedDate);
            var imagePath = "data:image/jpeg;base64," + notifications[i].SenderImagePath;

            var html = "<div class='notification-item'><input type='hidden' name='notifyId' value='" + notifications[i].Id +
                "'/><div class='notification-item-delete'><span class='fa fa-times'></span>" +
                "</div><table><tbody><tr> <td class='col-md-2 col-sm-4 hidden-xs'> <img class='notification-item-image' src='" +
                imagePath + "'></td><td class='col-md-10 col-sm-8 col-xs-12'><div class='notificaton-item-details'>" +
                "<div class='notification-item-username'>" + notifications[i].SenderName + "</div><div class='notification-item-date'>" + notifyDate + "</div>" +
                "<div class='notification-item-text'>" + notifications[i].Text + "</div></div></div></td></tr></tbody></table></div>";

            $("#notifications-container").append(html);
        }
    }

    function ChangeProfileType(selectedProfile) {
        var url = window.location.pathname;
        var returnUrl = url;

        if (url == "/employee/myoffers" || url == "/customer/myoffers") {
            if (selectedProfile == "Employee")
                returnUrl = "/employee/myoffers";
            else
                returnUrl = "/customer/myoffers";
        }
        else if (url == "/employee/myjobs" || url == "/customer/myjobs") {
            if (selectedProfile == "Employee")
                returnUrl = "/employee/myjobs";
            else
                returnUrl = "/customer/myjobs";
        }

        $.post("/profile/ChangeProfileType", { "profile": selectedProfile }, function () {
            location.href = returnUrl;
        });
    }

    function ParseCsharpDate(date) {
        var dateMs = date.replace(/[^0-9 +]/g, '');
        var dateMsInt = parseInt(dateMs);
        var fullDate = new Date(dateMsInt);
        return fullDate.toLocaleString().replace(/,/, '');
    }
})