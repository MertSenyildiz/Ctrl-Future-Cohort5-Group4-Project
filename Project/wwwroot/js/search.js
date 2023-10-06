$("#search").on("click",
    function () {
        const query = (new URL(document.location)).searchParams;
        console.log("I called");
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