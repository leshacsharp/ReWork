$(document).ready(function () {

    $("select[name=SelectedSkills]").multiselect({
        buttonWidth: '250px'
    });

    $("button[name=finish-job]").click(function () {
        var jobId = $("input[name=JobId]").val();

        $.ajax({
            type: "POST",
            url: "/job/EmployeeExistsInJob",
            data: { "jobId": jobId },
            success: function (exists) {
                if (exists) {
                    window.location.href = "/job/finishjob/" + jobId;
                }
                else {
                    $("#not-found-freelancer").modal("show");
                }
            }
        });
    });
});