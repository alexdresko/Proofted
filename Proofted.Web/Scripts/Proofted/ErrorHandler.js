window.onerror = function (eventOrMessage, source, fileno) {
    var message = "Global Error Handler logged: " + eventOrMessage + ", source: " + source + ", fileno: " + fileno;
    $.get("/Logging/GlobalJavascriptError", {
        eventOrMessage: eventOrMessage,
        source: source,
        fileno: fileno
    }).done(function () {
        alert(message);
    }).fail(function () {
        alert("Problem logging message in global error handler: " + message);
    });
};
//@ sourceMappingURL=ErrorHandler.js.map
