/// <reference path="../../T4TS.d.ts" />


export module Proofted {

	import _breeze = module("Breeze");
	_breeze;

	import logger = module("Logger2");


	export class DataService {

		serviceName: string;
		manager: breeze.EntityManager;
		constructor() {
			this.serviceName = "breeze/startapi";
			this.manager = new breeze.EntityManager(this.serviceName);

		}

		getAllOrganizations() {
			var query = breeze.EntityQuery
				.from("Organizations")
				.orderBy("Name")
				.expand("Invitations, Users, Projects");
			var executeQuery = this.manager.executeQuery(query);
			return executeQuery;
		}

		createOrganization(): T4TS.Organization {
			return <T4TS.Organization>this.manager.createEntity("Organization");
		}

		createInvitation(initialValues) {
			return this.manager.createEntity("Invitation", initialValues);
		}

		createProject(): T4TS.Project {
			return <T4TS.Project>this.manager.createEntity("Project");
		}

		saveChanges() {
			if (this.manager.hasChanges()) {
				this.manager.saveChanges()
					.then(this.saveSucceeded)
					.fail(this.saveFailed);
			} else {
				logger.Proofted.Logger.info("Nothing to save");
			}
		}

		saveSucceeded(saveResult) {
			logger.Proofted.Logger.success("# of Organizations saved = " + saveResult.entities.length);
			logger.Proofted.Logger.log(saveResult);
		}

		saveFailed(error) {
			var reason = error.message;
			var detail = error.detail;

			if (reason === "Validation error") {
				this.handleSaveValidationError(error);
				return;
			}
			if (detail && detail.ExceptionType.indexOf('OptimisticConcurrencyException') !== -1) {
				reason =
					"Another user, perhaps the server, may have deleted one or all of the todos.";
				this.manager.rejectChanges(); // DEMO ONLY: discard all pending changes
			}

			logger.Proofted.Logger.error(error,
				"Failed to save changes. " + reason +
					" You may have to restart the app.");
		}


		handleSaveValidationError(error) {
			var message = "Not saved due to validation error";
			try { // fish out the first error
				var firstErr = error.entitiesWithErrors[0].entityAspect.getValidationErrors()[0];
				message += ": " + firstErr.errorMessage;
			} catch (e) { /* eat it for now */
			}
			logger.Proofted.Logger.error(message);
		}

	}
}