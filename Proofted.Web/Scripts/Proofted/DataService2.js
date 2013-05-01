define(["require", "exports", "Breeze", "Logger2"], function(require, exports, ___breeze__, __logger__) {
    (function (Proofted) {
        var _breeze = ___breeze__;

        _breeze;
        var logger = __logger__;

        var DataService = (function () {
            function DataService() {
                this.serviceName = "breeze/startapi";
                this.manager = new breeze.EntityManager(this.serviceName);
            }
            DataService.prototype.getAllOrganizations = function () {
                var query = breeze.EntityQuery.from("Organizations").orderBy("Name").expand("Invitations, Users, Projects");
                var executeQuery = this.manager.executeQuery(query);
                return executeQuery;
            };
            DataService.prototype.createOrganization = function () {
                return this.manager.createEntity("Organization");
            };
            DataService.prototype.createInvitation = function (initialValues) {
                return this.manager.createEntity("Invitation", initialValues);
            };
            DataService.prototype.createProject = function () {
                return this.manager.createEntity("Project");
            };
            DataService.prototype.saveChanges = function () {
                if(this.manager.hasChanges()) {
                    this.manager.saveChanges().then(this.saveSucceeded).fail(this.saveFailed);
                } else {
                    logger.Proofted.Logger.info("Nothing to save");
                }
            };
            DataService.prototype.saveSucceeded = function (saveResult) {
                logger.Proofted.Logger.success("# of Organizations saved = " + saveResult.entities.length);
                logger.Proofted.Logger.log(saveResult);
            };
            DataService.prototype.saveFailed = function (error) {
                var reason = error.message;
                var detail = error.detail;
                if(reason === "Validation error") {
                    this.handleSaveValidationError(error);
                    return;
                }
                if(detail && detail.ExceptionType.indexOf('OptimisticConcurrencyException') !== -1) {
                    reason = "Another user, perhaps the server, may have deleted one or all of the todos.";
                    this.manager.rejectChanges();
                }
                logger.Proofted.Logger.error(error, "Failed to save changes. " + reason + " You may have to restart the app.");
            };
            DataService.prototype.handleSaveValidationError = function (error) {
                var message = "Not saved due to validation error";
                try  {
                    var firstErr = error.entitiesWithErrors[0].entityAspect.getValidationErrors()[0];
                    message += ": " + firstErr.errorMessage;
                } catch (e) {
                }
                logger.Proofted.Logger.error(message);
            };
            return DataService;
        })();
        Proofted.DataService = DataService;        
    })(exports.Proofted || (exports.Proofted = {}));
    var Proofted = exports.Proofted;
})
//@ sourceMappingURL=DataService2.js.map
