define(["require", "exports"], function(require, exports) {
    (function (Proofted) {
        (function (Knockout) {
            var Modal = (function () {
                function Modal() { }
                Modal.Setup = function Setup() {
                    ko.bindingHandlers["bootstrapModal"] = {
                        init: function (element) {
                            ko.utils.toggleDomNodeCssClass(element, "modal hide", true);
                            $(element).modal({
                                "backdrop": "static",
                                keyboard: false,
                                show: false
                            });
                        },
                        update: function (element, valueAccessor) {
                            var props = valueAccessor();
                            $(element).modal(props.show() ? 'show' : 'hide');
                        }
                    };
                }
                return Modal;
            })();
            Knockout.Modal = Modal;            
        })(Proofted.Knockout || (Proofted.Knockout = {}));
        var Knockout = Proofted.Knockout;
    })(exports.Proofted || (exports.Proofted = {}));
    var Proofted = exports.Proofted;
})
//@ sourceMappingURL=KoModal.js.map
