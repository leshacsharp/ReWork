$(document).ready(function () {

    SendFindUsers();

    $("tbody").on("click", "input[name=delete-user]", function () {
        var user = $(this).parent().parent().parent();
        var userId = user.find("input[name=Id]").val();
        
        $.ajax({
            url: "/moderator/deleteuser",
            type: "POST",
            data: { "id": userId },
            success: function () {
                user.remove();
            }
        })
    })

    $("tbody").on("click", "input[name=user-details]", function () {
        var user = $(this).parent().parent().parent();
        var userId = user.find("input[name=Id]").val();

        $.ajax({
            url: "/moderator/detailsuser/",
            type: "GET",
            data: { "id": userId },
            success: function (userDetailsHtml) {
                $("#user-details-modal .modal-dialog").empty();
                $("#user-details-modal .modal-dialog").append(userDetailsHtml);
                $("#user-details-modal").modal("show");
            }
        })
    })


    $("button[name=search-users]").click(function () {
        var userName = $("input[name=user-name]").val();
        SendFindUsers(userName);
    })

    function SendFindUsers(userName) {
        $.ajax({
            url: "/moderator/findusers",
            type: "POST",
            data: { "userName": userName },
            success: appendUsers   
        })
    }

    function appendUsers(data) {
        //TODO: СДЕЛАТЬ сколько найдено items

        $(".pagination").pagination({
            dataSource: data,
            pageSize: 3,
            formatResult: FormatUsersResult,

            callback: function (data, pagination) {
                $("tbody").empty();
                $("tbody").append(data);
            }
        })
    }

    function FormatUsersResult(data) {
        var result = [];

        for (var i = 0; i < data.length; i++) {
            var firstName = data[i].FirstName != null ? data[i].FirstName : "";
            var lastName = data[i].LastName != null ? data[i].LastName : "";
            var id = data[i].Id;

            var html = "</tr><tr><td class='col-md-3'><input type='hidden' name='Id' value='" + id + "'><span class='user-fullname'>" + firstName + " " + lastName + " [" + data[i].UserName +
                "]</span></td><td class='col-md-3'><span class='user-email'>" + data[i].Email + "</span></td>" +
                "<td class='col-md-6'><div class='job-button'><input type='submit' class='btn btn-primary' name='delete-user' value='Delete'></div>" +
                "<div class='job-button'><input type='submit' class='btn btn-primary' name='user-details' value='Details'/></div>" +
                "<div class='job-button'><a class='btn btn-primary' href='/moderator/edituser/" + id + "'>Manage</a></div></td></tr>";
            result.push(html);
        }
        return result;
    }

})