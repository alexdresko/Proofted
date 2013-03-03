//define(function(require) {
//    var breeze = require('breeze'),
//        serviceName = 'breeze/startapi',
//        manager = new breeze.EntityManager(serviceName);

//    var logger = require('logger');
define(['ko', 'breeze', 'logger'], function (ko, breeze, logger) {
    var serviceName = 'breeze/startapi';

    // *** Cross origin service example  ***
    //var serviceName = 'http://todo.breezejs.com/api/todos'; // controller in different origin

    var manager = new breeze.EntityManager(serviceName);

    function getAllOrganizations() {
        var query = breeze.EntityQuery
            .from("Organizations")
            .orderBy("Name")
            .expand("Invitations, Users, Projects");
        var executeQuery = manager.executeQuery(query);
        return executeQuery;
    }

    function createOrganization() {
        return manager.createEntity("Organization");
    }
    
    function createInvitation() {
        return manager.createEntity("Invitation");
    }
    
    function createProject() {
        return manager.createEntity("Project");
    }

    function saveChanges() {
        if (manager.hasChanges()) {
            manager.saveChanges()
                .then(saveSucceeded)
                .fail(saveFailed);
        } else {
            logger.info("Nothing to save");
        }
    }

    function saveSucceeded(saveResult) {
        logger.success("# of Organizations saved = " + saveResult.entities.length);
        logger.log(saveResult);
    }

    function saveFailed(error) {
        var reason = error.message;
        var detail = error.detail;

        if (reason === "Validation error") {
            handleSaveValidationError(error);
            return;
        }
        if (detail && detail.ExceptionType.indexOf('OptimisticConcurrencyException') !== -1) {
            reason =
                "Another user, perhaps the server, may have deleted one or all of the todos.";
            manager.rejectChanges(); // DEMO ONLY: discard all pending changes
        }

        logger.error(error,
            "Failed to save changes. " + reason +
                " You may have to restart the app.");
    }


    function handleSaveValidationError(error) {
        var message = "Not saved due to validation error";
        try { // fish out the first error
            var firstErr = error.entitiesWithErrors[0].entityAspect.getValidationErrors()[0];
            message += ": " + firstErr.errorMessage;
        } catch(e) { /* eat it for now */
        }
        logger.error(message);
    }

    return {
        getAllOrganizations: getAllOrganizations,
        createOrganization: createOrganization,
        saveChanges: saveChanges,
        createInvitation: createInvitation,
        createProject: createProject
    };
    //#endregion

});