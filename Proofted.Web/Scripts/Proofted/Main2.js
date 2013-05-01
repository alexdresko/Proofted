define(["require", "exports", "Logger2", "Configure", "KoModal"], function(require, exports, __logger__, __configure__, __modal__) {
    var logger = __logger__;

    var configure = __configure__;

    var modal = __modal__;

    (function (Proofted) {
        configure.Proofted.Configuration.Configure();
        modal.Proofted.Knockout.Modal.Setup();
        logger.Proofted.Logger.info("Prooted is booting");
        require([
            'shellViewModel2'
        ], function (shellViewModel) {
            var model = new shellViewModel.Proofted.ShellViewModel();
            model.getAllOrganizations();
            ko.applyBindings(model);
        });
    })(exports.Proofted || (exports.Proofted = {}));
    var Proofted = exports.Proofted;
})
//@ sourceMappingURL=Main2.js.map
