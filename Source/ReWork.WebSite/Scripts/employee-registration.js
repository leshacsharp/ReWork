$(document).ready(function () {
    var skills = [];

    $(".skill-item").click(function () {
        var skillTitle = $(this).text();
        var skillId = $(this).parent().find("input[name=skill-id]").val();

        var data = { "Id": skillId };

        var skillExists = false;
        for (var i = 0; i < skills.length; i++) {
            if (skills[i].Id == skillId) {
                skillExists = true;
            }
        }

        if (!skillExists) {
            skills.push({ "Id": skillId });
            alert("added skill" + skillTitle);
        }  
    })

    $("input[name=send-form]").click(function () {
        var age = +$("input[name=Age]").val();
        var validationBox = $(document).find("span[data-valmsg-for=Age]");
        var validate = (age >= 10 && age <= 100);

        if (validate) {
            validationBox.text("");
            $.ajax({
                url: "/settings/employee",
                type: "POST",
                data: { "Age": age, "Skills": skills }
            })
        }
        else { 
            validationBox.text("age must be from 10 to 100 years");
        }
    })

  
})