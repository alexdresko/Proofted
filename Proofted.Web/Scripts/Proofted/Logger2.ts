/// <reference path="../typings/toastr/toastr.d.ts" />

toastr.options.timeOut = 2000; // 2 second toast timeout
toastr.options.positionClass = 'toast-bottom-right';

export module Proofted {
    export class Logger {
        static error(message: string, title?: string) {
            toastr.error(message, title);
            log("Error: " + message);
        };
        static info(message: string, title?: string) {
            toastr.info(message, title);
            log("Info: " + message);
        };
        static success(message: string, title?: string) {
            toastr.success(message, title);
            log("Success: " + message);
        };
        static warning(message: string, title?: string) {
            toastr.warning(message, title);
            log("Warning: " + message);
        };

        // IE and google chrome workaround
        // http://code.google.com/p/chromium/issues/detail?id=48662
        static log(message: string) {
            var console = window.console;
            !!console && console.log && console.log.apply && console.log.apply(console, arguments);
        }
    }
}