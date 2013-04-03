define(["require", "exports", "Logger2"], function(require, exports, __logger__) {
    require.config({
        paths: {
            "breeze": "../breeze.debug",
            "jQuery": "../jquery-1.9.1",
            "bootstrap": "../bootstrap",
            "ko": "../knockout-2.2.0"
        },
        shim: {
            "jQuery": {
                deps: [],
                init: function () {
                    return $;
                }
            }
        }
    });
    var logger = __logger__;

    
    (function (Proofted) {
        logger.Proofted.Logger.info("Prooted is booting");
        require([
            'shellViewModel'
        ], function (shellViewModel) {
            ko.applyBindings(shellViewModel);
        });
    })(exports.Proofted || (exports.Proofted = {}));
    var Proofted = exports.Proofted;
})
//@ sourceMappingURL=Main2.js.map
