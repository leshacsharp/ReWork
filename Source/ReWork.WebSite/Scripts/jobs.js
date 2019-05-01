$(document).ready(function () {

    SendFindJobs();

    $(".skills-form").validate({
        rules: {
            PriceFrom: {
                required: false, 
                range: [0, 1000000]
            },
            KeyWords: {
                required: false,
                maxlength: 60
            }
        },
        messages: {
            PriceFrom: "Please input price from 0 to 1000000",
            KeyWords: "max length key words is 60 symbols"
        },
        focusInvalid: true,
        errorClass: "input-error",
        submitHandler: function (form) {
            var price = $("input[name=PriceFrom]").val();
            var keyWords = $("input[name=KeyWords]").val();

            var skillsId = $('input[name=SkillsId]').map(function (i, v) {
                return $(this).val()
            }).toArray();

            SendFindJobs(skillsId, keyWords, price);
            return false;  
        }
    });


    $("button[name=reset]").click(function () {
        var price = $("input[name=PriceFrom]").val("");
        var keyWords = $("input[name=KeyWords]").val("");

        $(".selected-skills-container .selected-skill").remove();
        AppendSelectedSkillsHeader();
        $("tbody").empty();
        $(".panel-group ul li.active").removeClass("active");

        SendFindJobs();
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

    $(".selected-skills-container").on("click",".delete-skill",function () {
        var parent = $(this).parent();
        var skillName = parent.find(".selected-skill-name").text().replace(/\n|\r|\s+/g, "");

        var pannel = $(".panel-group a:contains(" + skillName + ")").parent();
        pannel.removeClass("active");

        parent.remove();
        AppendSelectedSkillsHeader();
    })

    $("tbody").on("click", ".job-skills a", function () {
        var skillId = $(this).attr("skill-id");
        var skillsId = [skillId];
        SendFindJobs(skillsId);
    })

    $(".skill-link").click(function () {
        var skillId = $(this).find("input[type=hidden]").val();
        var skillsId = [skillId];
        SendFindJobs(skillsId);
    });



    function AppendSelectedSkillsHeader() {
        var countSkills = $(".selected-skill");
        var header = $(".selected-skills-header");
        if (countSkills.length == 0 && header.length == 0) {
            var header = "<div class='selected-skills-header'> Titles categories </div>";
            $(".selected-skills-container").append(header);
        }
    }



    function SendFindJobs(skillsId, keyWords, price) {   
        $.ajax({
            url: "/Job/Jobs",
            type: "POST",
            data: { "skillsId": skillsId, "keyWords": keyWords, "priceFrom": price },
            success: appendJobs
        })
    }

    function appendJobs(data) {
        $("tbody").empty();
        $(".filter-count").html(data.length);

        $(".pagination").pagination({
            dataSource: data,
            pageSize: 3,
            formatResult: FormatResult,


            callback: function (data, pagination) {
                $("tbody").empty();
                $("tbody").append(data);
            }
        })
    }

    function FormatResult (data) {
        var result = [];
        for (var i = 0; i < data.length; i++) {
            var html = "<tr>" +
                "<td class='col-md-7'>" +
                "<a href='/Job/Details/" + data[i].Id + "' class='job-title'>" +
                data[i].Title + "</a><div class='job-skills'>";

            var skillsHtml = "";
            var skills = data[i].Skills;
            for (var j = 0; j < skills.length; j++) {
                skillsHtml += "<a class='skill-link' skill-id='"+ skills[j].Id +"'>" +
                    skills[j].Title + " </a>";
            }
            html += skillsHtml + "</div></td>";
           
            var date = ParseCsharpDate(data[i].DateAdded);
            html += "<td class='col-md-2 text-center hidden-xs'>" +
                "<a class='job-customer' href='/customer/details/" + data[i].CustomerId + "'>" +
                data[i].UserName + "</a></td><td class='col-md-1 text-center'><span class='price'>" +
                data[i].Price + "$</span></td><td class='col-md-1 text-center hidden-xs'><span class='count-offers'>" +
                data[i].CountOffers + "</span></td><td class='col-md-1 text-center hidden-xs'><span class='date-added'>" +
                date + "</span></td></tr>"

            result.push(html);
        }
        return result;
    }

    function ParseCsharpDate(date) {
        var dateMs = date.replace(/[^0-9 +]/g, '');
        var dateMsInt = parseInt(dateMs);
        var fullDate = new Date(dateMsInt);

        var monthNames = [
            "January", "February", "March",
            "April", "May", "June", "July",
            "August", "September", "October",
            "November", "December"
        ];

        var day = fullDate.getDate();
        var monthIndex = fullDate.getMonth();
        return day + ' ' + monthNames[monthIndex];
    }
})