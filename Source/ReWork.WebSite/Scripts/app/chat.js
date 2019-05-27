$(document).ready(function () {

    var chatHub = $.connection.chatHub;

    chatHub.client.refreshChatRoom = function (msg) {;
        var parseMsg = $.parseJSON(msg);
        var msgBlock = "<div>" + parseMsg.Text + "</div>";
        $("#first").before(msgBlock);
    }

    var isChrome = !!window.chrome;
    if (isChrome == false) {
        $.connection.hub.stop();
    }

  
    $.connection.hub.start().done(function () {
        chatHub.server.connect(1);

        $("input[name=send-msg]").click(function () {

            var text = $("input[name=msg-text]").val();

            $.ajax({
                type: "POST",
                url: "/chat/addmessage",
                data: { "text": text },   
            })
        })
    });
})