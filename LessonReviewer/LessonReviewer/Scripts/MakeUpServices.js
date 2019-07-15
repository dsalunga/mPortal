function ExecuteKeepAlive(interval) {
    var timerDelay = interval == null ? 5 * 60 * 1000 : interval;

    setInterval(function () {
        $.ajax({
            type: "GET",
            url: "/Handlers/AjaxHandler.ashx",
            data: "Method=KeepAlive&" + MCGI.DateTime.Ticks
        });
    }, timerDelay);
}