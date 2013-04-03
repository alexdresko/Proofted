define(["require", "exports"], function(require, exports) {
    toastr.options.timeOut = 2000;
    toastr.options.positionClass = 'toast-bottom-right';
    (function (Proofted) {
        var Logger = (function () {
            function Logger() { }
            Logger.error = function error(message, title) {
                toastr.error(message, title);
                Logger.log("Error: " + message);
            }
            Logger.info = function info(message, title) {
                toastr.info(message, title);
                Logger.log("Info: " + message);
            }
            Logger.success = function success(message, title) {
                toastr.success(message, title);
                Logger.log("Success: " + message);
            }
            Logger.warning = function warning(message, title) {
                toastr.warning(message, title);
                Logger.log("Warning: " + message);
            }
            Logger.log = function log(message) {
                var console = window.console;
                !!console && console.log && console.log.apply && console.log.apply(console, arguments);
            }
            return Logger;
        })();
        Proofted.Logger = Logger;        
    })(exports.Proofted || (exports.Proofted = {}));
    var Proofted = exports.Proofted;
})
//@ sourceMappingURL=Logger2.js.map
