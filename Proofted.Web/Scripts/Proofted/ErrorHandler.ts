/// <reference path="../typings/jquery/jquery.d.ts" />
window.onerror = (eventOrMessage: any, source: string, fileno: number) => {
	var message = "Global Error Handler logged: " + eventOrMessage + ", source: " + source + ", fileno: " + fileno;
	$.get("/Logging/GlobalJavascriptError", { eventOrMessage: eventOrMessage, source: source, fileno: fileno}).done(() => {
			alert(message);
	}).fail(() => { alert("Problem logging message in global error handler: " + message) });
};
