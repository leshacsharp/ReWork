$(document).ready(function () {

    $("a[name=sort-payment]").click(function () {
        var sortOffers = getSorted(".job-offer", "data-payment");
        $(".job-offers-container").empty();
        $(".job-offers-container").append(sortOffers);
    })


    function getSorted(selector, attrName) {
        var array = $(selector).toArray();
        return $(array.sort(function (a, b) {
            var aVal = parseInt(a.getAttribute(attrName)),
                bVal = parseInt(b.getAttribute(attrName));
            return aVal - bVal;
        }));
    }
})