$(document).ready(function () {

    SendFindJobs();

    $("#datetimepicker").datepicker({
        startDate: new Date(2018, 3, 25),
        endDate: new Date()
    });

    $("input[name=search-jobs]").click(function () {
        SendFindJobs();
    });


    $(".search-table tbody").on("click", "input[name=delete-job]", function () {
        var tr = $(this).parent().parent().parent();
        var jobId = tr.find("input[name=jobId]").val();

        $.ajax({
            url: "/job/DeleteEmployeeFromJob",
            type: "POST",
            data: { "id": jobId },
            success: function () {
                SendFindJobs();
            }
        });
    });

    function SendFindJobs() {
        $(".search-table tbody").empty();

        var fromDate = null;
        var fromDateStr = $("#datetimepicker input[type=text]").val();
        if (fromDateStr != "") {
            fromDate = (new Date(fromDateStr)).toISOString();
        }

        $.ajax({
            url: "/Employee/Myjobs",
            type: "POST",
            data: { "fromDate": fromDate },
            success: AppendJobs
        });
    };

    function AppendJobs(data) {

        $(".pagination").pagination({
            dataSource: data,
            pageSize: 3,
            formatResult: FormatResult,

            callback: function (data) {
                $(".search-table tbody").empty();
                $(".search-table tbody").append(data);
            }
        });
    };

    function FormatResult(data) {
        var result = [];
        for (var i = 0; i < data.length; i++) {
            var html = "<tr>" +
                "<td class='col-md-6'><input type='hidden' name='jobId' value='" + data[i].Id + "'/>" +
                "<a href='/Job/Details/" + data[i].Id + "' class='job-title'>" +
                data[i].Title + "</a><div class='job-skills'>";

            var skillsHtml = "";
            var skills = data[i].Skills;
            for (var j = 0; j < skills.length; j++) {
                skillsHtml += "<a class='skill-link' skill-id='" + skills[j].Id + "'>" +
                    skills[j].Title + " </a>";
            }

            html += skillsHtml + "</div></td>";

            var date = ParseCsharpDate(data[i].DateAdded);
            html += "<td class='col-md-2 text-center hidden-xs'>" +
                "<a class='job-customer' href='/customer/details/" + data[i].CustomerId + "'>" +
                data[i].UserName + "</a></td><td class='col-md-1 text-center'><span class='price'>" +
                data[i].Price + "$</span></td><td class='col-md-2 text-center hidden-sm hidden-xs'><span class='count-offers'>" +
                data[i].CountOffers + " offers</span></td><td class='col-md-1 text-center hidden-xs'><span class='date-added'>" +
                date + "</span></td><td class='col-md-1'><input type='submit' class='btn btn-primary' name='delete-job' value='Reject'/></td></tr>"

            result.push(html);
        }

        return result;
    };

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
    };
});