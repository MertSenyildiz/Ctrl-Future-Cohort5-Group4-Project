(function ($) {
    $("#search").on("click",
        function () {
            const query = (new URL(document.location)).searchParams;
            const val = $("#nameFilter").val()
            const params = new URLSearchParams({});
            if (val !== "") {
                params.append("nameFilter", val)
            }
            if (query.has("categoryFilter")) {
                params.append("categoryFilter", query.get("categoryFilter"))
            }
            const href = $("#search").attr("href") + "?" + params.toString();
            $("#search").attr("href", href)
        });

    $("#nameFilter").on("keydown", function search(e) {
        if (e.keyCode == 13) {
            $("#search")[0].click()
        }
    });
})(window.jQuery);