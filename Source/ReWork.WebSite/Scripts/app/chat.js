$(document).ready(function () {

    var chatRoomId = $("input[name=Id]").val();
    const countMessages = 10; 
    var page = 1;
    var isFirstLoad = true;




    FindMessages(page, countMessages);

    function FindMessages(page, count) {
        $.ajax({
            url: "/chat/findmessages",
            type: "POST",
            data: { "chatRoomId": chatRoomId, "count": count, "page": page },
            success: function (messages) {

                if (messages.length == countMessages) {
                    $(".load-more-messages input").remove();
                    var loadMorebtn = "<input type='submit' class='btn btn-default' id=load-more value='Load more' />";
                    $(".load-more-messages").append(loadMorebtn);
                }
                else {
                    $(".load-more-messages input").remove();
                }
                

                FormatMessages(messages);

                if (isFirstLoad) {
                    isFirstLoad = false;
                    $(".messages").scrollTop($(".messages").height());
                }
            }
        })
    }

    function FormatMessages(messages) {

        for (var i = 0; i < messages.length; i++) {

            var sendDate = ParseCsharpDate(messages[i].DateAdded);
            var html = "<li class='message-item'><table><tbody><td class='col-md-2 hidden-xs'>" +
                "<a href='/customer/details/" + messages[i].SenderId + "'><img src='data:image/jpeg;base64, " + messages[i].SenderImagePath + "' class='user-photo'></a></td>" +
                "<td class='col-md-10'><div class='message-info'><a href='/customer/details/" + messages[i].SenderId + "'>" +
                messages[i].SenderName + "</a><span class='sent-date'>" + sendDate + "</span> <div class='message-text'>" +
                messages[i].Text + "</div></div></td></tbody></table></li>";

            $(".messages .load-more-messages").after(html);
        }
    }

    function FindUsers(userName) {
        $.ajax({
            url: "/chat/findusers",
            type: "POST",
            data: { "userName": userName },
            success: AppendUsers
        })
    }

    function AppendUsers(users){
        $(".users > .user-item").remove();
        
        for (var i = 0; i < users.length; i++) {
            var html = "<div class='user-item'><input type='hidden' name='userId' value='" + users[i].Id + "'>" +
                "<a href='/customer/details/" + users[i].Id +
                "'><img src='data:image/jpeg;base64," + users[i].ImagePath + "'><span>" + users[i].UserName + "</span></a>" +
                "<input type='submit' class='btn btn-success' style='margin-left:5px' name='invite-user' value='invite'></div>";
                
            $(".users").append(html);
        }
    }



    $(".load-more-messages").on("click", "#load-more", function () {
        page++;
        FindMessages(page, countMessages);
    })

    $("button[name=invite-users]").click(function () {
        var userName = $("input[name=user-name]").val();
        FindUsers(userName);
    })

    $("input[name=user-name]").on("input",function () {
        var userName = $("input[name=user-name]").val();
        FindUsers(userName);
    })

    $(".users").on("click", "input[name=invite-user]", function () {
        var userId = $(this).parent().find("input[name=userId]").val();

        $.ajax({
            url: "/chat/addmembertochatroom",
            type: "POST",
            data: { "chatRoomId": chatRoomId, "userId": userId },
        })
    })




    var chatHub = $.connection.chatHub;

    chatHub.client.refreshChatRoom = function (msg) {
        var parseMsg = $.parseJSON(msg);

        var html = "<li class='message-item'><table><tbody><td class='col-md-2 hidden-xs'>" +
            "<a href='/customer/details/" + parseMsg.SenderId + "'><img src='data:image/jpeg;base64, " + parseMsg.SenderImagePath + "' class='user-photo'></a></td>" +
            "<td class='col-md-10'><div class='message-info'><a href='/customer/details/" + parseMsg.SenderId + "'>" +
            parseMsg.SenderName + "</a><span class='sent-date'>" + parseMsg.DateAdded + "</span> <div class='message-text'>" +
            parseMsg.Text + "</div></div></td></tbody></table></li>";

        $(".messages").append(html).animate({ 
            scrollTop: $(".messages").get(0).scrollHeight
        });
    }

    var isChrome = !!window.chrome;
    if (isChrome == false) {
        $.connection.hub.stop();
    }

    $.connection.hub.start().done(function () {
        chatHub.server.connect(chatRoomId);

        $("input[name=send-msg]").click(function () {

            var text = $("input[name=msg-text]").val();

            $.ajax({
                type: "POST",
                url: "/chat/addmessage",
                data: { "chatRoomId": chatRoomId, "text": text },
            })
        })
    });



    function ParseCsharpDate(date) {
        console.log(date);
        var dateMs = date.replace(/[^0-9 +]/g, '');
        var dateMsInt = parseInt(dateMs);
        var fullDate = new Date(dateMsInt);
        return fullDate.toLocaleString().replace(/,/, '');
    }
})
