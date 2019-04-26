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
            formatResult: FormatResult,


            callback: function (data, pagination) {
                $("tbody").empty();
                $("tbody").append(data);
            }
        })
    }

    function FormatResult(data) {
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

            html += "<td class='col-md-2 text-center'><span class='price'>" +
                data[i].Price + "$</span></td><td class='col-md-2 text-center hidden-xs'><span class='count-offers'><a href='/Job/Details/" + data[i].Id + "'>" + 
                data[i].CountOffers + " offers</a></span></td><td class='col-md-2 text-center hidden-xs'><span class='date-added'>" +
                date + "</span></td></tr>"

            result.push(html);
        }
        return result;
    }

    function ParseCsharpDate(date) {
        var dateMs = date.replace(/[^0-9 +]/g, '');
        var dateMsInt = parseInt(dateMs);
        var fullDate = new Date(dateMsInt);
        return fullDate.toDateString().replace(/,/, '');
    }
})