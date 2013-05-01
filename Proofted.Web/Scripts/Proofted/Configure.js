define(["require", "exports"], function(require, exports) {
    (function (Proofted) {
        var Configuration = (function () {
            function Configuration() { }
            Configuration.Configure = function Configure() {
                ko.validation.configure({
                    registerExtenders: true,
                    messagesOnModified: true,
                    insertMessages: true,
                    parseInputAttributes: true,
                    messageTemplate: null,
                    errorMessageClass: "help-inline",
                    decorateElement: true,
                    errorElementClass: 'error'
                });
            }
            return Configuration;
        })();
        Proofted.Configuration = Configuration;        
    })(exports.Proofted || (exports.Proofted = {}));
    var Proofted = exports.Proofted;
})
//@ sourceMappingURL=Configure.js.map
