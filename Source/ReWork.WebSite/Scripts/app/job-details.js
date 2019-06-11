$(document).ready(function () {

    $("input[name=add-offer]").click(function () {

        var jobId = $("input[name=JobId]").val();

        $.ajax({
            url: "/offer/offerexists",
            type: "POST",
            data: { "jobId": jobId },
            success: function (exists) {
                if (!exists) {
                    $('#create-offer').modal('show');
                }
                else {
                    $("#exists-offer").modal('show');
                }
            }
        });
    });

    $("#offer-form input[type=submit]").click(function () {

        $("#offer-form").validate();
        var isValid = $("#offer-form").valid();


        if (isValid) {

            var reciverId = $("input[name=CustomerId]").val();
            var jobTitle = $("#jobTitle").html();

            $.ajax({
                url: "/notification/CreateNotify",
                type: "POST",
                data: { "reciverId": reciverId, "text": "you made offer to job - " + jobTitle }
            })
        }
    });


    $("a[name=sort-payment]").click(function () {
        sortOffers($(this), "data-payment");
    });

    $("a[name=sort-date]").click(function () {
        sortOffers($(this), "data-date", "date");
    });

    $("a[name=sort-time]").click(function () {
        sortOffers($(this), "data-days");
    });


    function sortOffers(btn, attrName, attrType) {
        var sortOffers;
        var sortSide = btn.find("span");
        var isActive = sortSide.length > 0;

        if (isActive) {
            var isIncrease = sortSide.hasClass("fa-arrow-up");
            if (isIncrease) {
                sortOffers = getSorted(".job-offer", attrName, 'descending', attrType);
                sortSide.removeClass("fa-arrow-up");
                sortSide.addClass("fa-arrow-down");
            }
            else {
                sortOffers = getSorted(".job-offer", attrName, 'increase', attrType);
                sortSide.removeClass("fa-arrow-down");
                sortSide.addClass("fa-arrow-up");
            }
        }
        else {
            btn.parent().find("a span").remove();

            sortOffers = getSorted(".job-offer", attrName, 'descending', attrType);
            btn.append("<span class='fa fa-arrow-down'></span>");
        }

        $(".job-offers-container").empty();
        $(".job-offers-container").append(sortOffers);
    };

    function getSorted(selector, attrName, side, attrType) {
        var array = $(selector).toArray();
        return $(array.sort(function (a, b) {
            var aVal = a.getAttribute(attrName);
            var bVal = b.getAttribute(attrName);

            if (attrType == "date") {
                aVal = new Date(aVal);
                bVal = new Date(bVal);
            }
            else {
                aVal = parseInt(aVal);
                bVal = parseInt(bVal);
            }

            if (side == 'increase') {
                return aVal - bVal;
            }

            if (side == "descending") {
                return bVal - aVal;
            }
        }));
    };

});
