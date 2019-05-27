$(document).ready(function () {

    SendFindJobs();


    $("button[name=search-jobs]").click(function () {
        var keyWords = $("#jobs-tab input[name=keyWords]").val();
        SendFindJobs(keyWords);
    })

    function SendFindJobs(keyWords) {
        $.ajax({
            url: "/Job/Jobs",
            type: "POST",
            data: { "keyWords": keyWords},
            success: appendJobs
        })
    }

    function appendJobs(data) {
        //TODO: СДЕЛАТЬ сколько найдено items

        $("#jobs-tab .pagination").pagination({
            dataSource: data,
            pageSize: 3,
            formatResult: FormatJobResult,


            callback: function (data, pagination) {
                $(".search-table tbody").empty();
                $(".search-table tbody").append(data);
            }
        })
    }

    function FormatJobResult(data) {
        var result = [];
        for (var i = 0; i < data.length; i++) {
            var html = "<tr><td class='col-ms-6'><span class='jobs-counter'>" + (i + 1) + ".</span>" +
                "<div class='search-item-body'>" + 
                "<a href='/Job/Details/" + data[i].Id + "' class='job-title'>" +
                data[i].Title + "</a><div class='job-skills'>";

            var skillsHtml = "";
            var skills = data[i].Skills;
            for (var j = 0; j < skills.length; j++) {
                skillsHtml += "<a class='skill-link' skill-id='" + skills[j].Id + "'>" +
                    skills[j].Title + " </a>";
            }
            html += skillsHtml + "</div></div></td>";
            var date = ParseCsharpDate(data[i].DateAdded);

            html += "<td class='col-md-1 text-center hidden-xs'>" +
                "<a class='job-customer' href='/customer/details/" + data[i].CustomerId + "'>" +
                data[i].UserName + "</a></td><td class='col-md-1 text-center'><span class='price'>" +
                data[i].Price + "$</span></td><td class='col-md-2 text-center hidden-sm hidden-xs'><span class='count-offers'><a href='/Job/Details/" + data[i].Id + "'>" + 
                data[i].CountOffers + " offers</a></span></td><td class='col-md-1 text-center hidden-xs'><span class='date-added'>" +
                date + "</span></td></tr>"

            result.push(html);
        }
        return result;
    }



    $("button[name=search-employees],.nav-tabs a[name=link-employees-tab]").click(function () {
        var keyWords = $("#employees-tab input[name=keyWords]").val();
        SendFindEmployees(keyWords);
    })

    function SendFindEmployees(keyWords) {
        $.ajax({
            url: "/Employee/Employees",
            type: "POST",
            data: { "keyWords": keyWords },
            success: appendEmployees
        })
    }

    function appendEmployees(data) {
        //TODO: СДЕЛАТЬ сколько найдено items

        $("#employees-tab .pagination").pagination({
            dataSource: data,
            pageSize: 3,
            formatResult: FormatEmployeeResult,

            callback: function (data, pagination) {
                $("#search-employees-container").empty();
                $("#search-employees-container").append(data);
            }
        })
    }

    function FormatEmployeeResult(data) {
        var result = [];

        for (var i = 0; i < data.length; i++) {

            var employee = "/employee/details/" + data[i].Id;

            var feedbacks = data[i].QualityOfWorks;
            var countPositiveFeedbacks = 0;
            for (var j = 0; j < feedbacks.length; j++) {
                if (feedbacks[j] >= 3) {
                    countPositiveFeedbacks++;
                }
            }
            var percentPositiveFeedbacks = countPositiveFeedbacks / (feedbacks.length == 0 ? 1 : feedbacks.length);

            
            var imagePath = "data:image/jpeg;base64," + data[i].ImagePath;

            var html = "<div class='employee'><div class='row'><div class='col-md-8 col-sm-7 col-xs-12'><div class='pull-left employee-photo'>" +
                "<a href='" + employee + "'><img src='" + imagePath + "'" +
                "</a></div><div class='employee-name'><a href='" + employee + "'>" + data[i].UserName +
                "</a><span class='hidden-xs'> (" + data[i].FirstName + " " + data[i].LastName + ")</span></div><div class='employee-skills>";



            var skillsHtml = "";
            var skills = data[i].Skills;
            for (var j = 0; j < skills.length; j++) {
                skillsHtml += "<a class='skill-link' skill-id='" + skills[j].Id + "'>" + skills[j].Title + " </a>";
            }

            html += skillsHtml + "</div><div class='employee-country'><span>Беларусь</span></div></div>" +
                "<div class='col-md-4 col-sm-5 col-xs-12'><div class='employee-card'><div class='see-profile'>" +
                "<a href='" + employee + "'><input type='submit' class='btn btn-success see-profile-btn' value='profile' /></a></div>" +
                "<div class='employee-feedbacks'><div class='employee-feedbacks-count'>" + feedbacks.length + "</div>" +
                "<a href='" + employee + "'>reviews</a></div><div class='employee-feedbacks-info'>" +
                "<div class='employee-feedbacks-info-num'>" + percentPositiveFeedbacks + "%</div>" +
                "<span>positive</span></div></div></div></div></div>"

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