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
        $("#employees").empty();
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
            success: appendEmployees
        })
    }

    function appendEmployees(data) {
        $("#employees").empty();
        $(".filter-count").html(data.length);

        $(".pagination").pagination({
            dataSource: data,
            pageSize: 3,
            formatResult: FormatResult,

            callback: function (data, pagination) {
                $("#employees").empty();
                $("#employees").append(data);
            }
        })
    }

    function FormatResult(data) {
        var result = [];
        for (var i = 0; i < data.length; i++) {
            var html = "<div style='border:1px solid black;'>" + data[i].UserName;

            var skillsHtml = "";
            var skills = data[i].Skills;
            for (var j = 0; j < skills.length; j++) {
                skillsHtml += "<a class='skill-link' skill-id='" + skills[j].Id + "'>" +
                    skills[j].Title + " </a>";
            }
            html += skillsHtml + "</div>";

            result.push(html);
        }
        return result;
    }
})