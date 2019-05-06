$(document).ready(function () {

    var currentCulture = $.cookie("lang");
    if (currentCulture != undefined) {
        var option = $("select").find("option[value=" + currentCulture + "]");
        option.attr("selected","selected");
    }

    var currentProfile = $.cookie("profile");
    if (currentProfile != undefined) {
        var option = $("select").find("option[value=" + currentProfile + "]");
        option.attr("selected", "selected");
    }
})