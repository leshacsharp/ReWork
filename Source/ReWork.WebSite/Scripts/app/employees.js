$(document).ready(function () {

    SendFindEmployees();

    $(".skills-form").validate({
        focusInvalid: true,
        errorClass: "input-error",
        submitHandler: function (form) {

            var skillsId = $('input[name=SkillsId]').map(function (i, v) {
                return $(this).val()
            }).toArray();

            SendFindEmployees(skillsId);
            return false;
        }
    });

    $("button[name=reset]").click(function () {
        $(".selected-skills-container .selected-skill").remove();
        AppendSelectedSkillsHeader();
        $(".employees-container").empty();
        $(".panel-group ul li.active").removeClass("active");

        SendFindEmployees();
    })


    $(".pannel-inner").click(function () {
        var active = $(this).hasClass("active")
        var skillName = $(this).find(".skill-link").text().replace(/\n|\r|\s+/g, "");

        if (!active) {
            var skillId = $(this).find("input[type=hidden]").val();
            var selectedSkill = "<div class='selected-skill'><div class='delete-skill'>" +
                "<span class='fa fa-times'></span></div><span class='selected-skill-name'>" +
                skillName + "</span> <input type='hidden' name='SkillsId' value='" + skillId + "' /></div>";

            $(".selected-skills-container").append(selectedSkill);
            $(this).addClass("active");

            var countSkills = $(".selected-skill");
            if (countSkills.length > 0) {
                $(".selected-skills-header").remove();
            }
        }
        else {
            var selector = ".selected-skill span:contains('" + skillName + "')";
            $(selector).parent().remove();

            $(this).removeClass("active");

            AppendSelectedSkillsHeader();
        }
    })

    $(".selected-skills-container").on("click", ".delete-skill", function () {
        var parent = $(this).parent();
        var skillName = parent.find(".selected-skill-name").text().replace(/\n|\r|\s+/g, "");

        var pannel = $(".panel-group a:contains(" + skillName + ")").parent();
        pannel.removeClass("active");

        parent.remove();
        AppendSelectedSkillsHeader();
    })

    $(".skill-link").click(function () {

        var skillId = $(this).find("input[type=hidden]").val();
        var skillsId = [skillId];
        SendFindEmployees(skillsId);
    });



    function AppendSelectedSkillsHeader() {
        var countSkills = $(".selected-skill");
        var header = $(".selected-skills-header");
        if (countSkills.length == 0 && header.length == 0) {
            var header = "<div class='selected-skills-header'> Titles categories </div>";
            $(".selected-skills-container").append(header);
        }
    }

    function SendFindEmployees(skillsId) { 
        $.ajax({
            url: "/Employee/Employees",
            type: "POST",
            data: { "skillsId": skillsId },
            success: appendEmployees,
            error: function (jqXHR, exception) {
                var a = jqXHR;
            }
        })
    }

    function appendEmployees(data) {
        $(".filter-count").html(data.length);

        $(".pagination").pagination({
            dataSource: data,
            pageSize: 3,
            formatResult: FormatResult,

            callback: function (data, pagination) {
                $(".employees-container").empty();
                $(".employees-container").append(data);
            }
        })
    }

    function FormatResult(data) {
        var result = [];

        for (var i = 0; i < data.length; i++) {

            var employee = "/employee/details/" + data[i].Id;
            var imagePath = "data:image/jpeg;base64," + data[i].ImagePath;

            var feedbacks = data[i].QualityOfWorks;
            var countPositiveFeedbacks = 0;
            for (var j = 0; j < feedbacks.length; j++) {
                if (feedbacks[j] >= 3) {
                    countPositiveFeedbacks++;
                }
            }
            var percentPositiveFeedbacks = countPositiveFeedbacks * 100 / (feedbacks.length == 0 ? 1 : feedbacks.length);
            

            var html = "<div class='employee'><div class='row'><div class='col-md-8 col-sm-7 col-xs-12'><div class='pull-left employee-photo'>" +
                "<a href='" + employee + "'><img src='" + imagePath + "'" +
                "</a></div><div class='employee-name'><a href='" + employee + "'>" + data[i].UserName +
                "</a><span class='hidden-xs'> (" + data[i].FirstName + " " + data[i].LastName + ")</span></div><div class='employee-skills'>";
               


            var skillsHtml = "";
            var skills = data[i].Skills;
            for (var j = 0; j < skills.length; j++) {
                skillsHtml += "<a class='skill-link' skill-id='" + skills[j].Id + "'>" +  skills[j].Title + " </a>";
            }

            html += skillsHtml + "</div><div class='employee-country'><span>Беларусь</span></div></div>" +
                "<div class='col-md-4 col-sm-5 col-xs-12'><div class='employee-card'><div class='see-profile'>" +
                "<form action='/chat/createchatroom' method='post'><input type='hidden' name='userId' value='" + data[i].Id + "'/>" +
                "<input type='submit' class='btn btn-success see-profile-btn' name='write-freelancer' value='write' /></form></div>" +
                "<div class='employee-feedbacks'><div class='employee-feedbacks-count'>" + feedbacks.length + "</div>" +
                "<a href='" + employee + "'>reviews</a></div><div class='employee-feedbacks-info'>" +
                "<div class='employee-feedbacks-info-num'>" + percentPositiveFeedbacks + "%</div>" +
                "<span>positive</span></div></div></div></div></div>"

            result.push(html);
        }
        return result;
    }
})