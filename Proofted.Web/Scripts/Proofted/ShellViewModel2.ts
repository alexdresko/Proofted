/// <reference path="../typings/jquery/jquery.d.ts" />
/// <reference path="../typings/knockout.validation/knockout.validation.d.ts" />
/// <reference path="../typings/knockout/knockout.d.ts" />
/// <reference path="../../T4TS.d.ts" />

	import logger = module("Logger2");
	export import _dataservice = module("DataService2");

export module Proofted {
	export class ShellViewModel {
		dataservice = new _dataservice.Proofted.DataService();

		organizations = ko.observableArray([]);

		public finishRender: () => void;

		constructor() {
			this.finishRender = () => {
				this.rendered(true);
			};

			this.querySucceeded = (data) => {
				this.organizations([]);
				data.results.forEach((item) => {
					this.extendOrganization(item);
					this.organizations.push(item);
				});
				logger.Proofted.Logger.info("Fetched Orgs ");
			};
		}

		newOrganizationClick() {
			this.editingOrganization(true);

		};



		editingOrganization = ko.observable(false);

		organizationNameInput = ko.observable('');
		organizationSubmit() {
			this.editingOrganization(false);
			var newOrganization = this.dataservice.createOrganization();

			newOrganization.Name(this.organizationNameInput());
			if (newOrganization.entityAspect.validateEntity()) {
				this.extendOrganization(newOrganization);
				this.organizations.push(newOrganization);
				this.dataservice.saveChanges();
				this.organizationNameInput("");
			} else {
				this.handleItemErrors(newOrganization);

			}
		};
		cancelOrganizationForm() {
			this.editingOrganization(false);
		};

		rendered = ko.observable(false);



		extendInvitation(invitation) {
			invitation.destroy = () => {
				invitation.Organization().Invitations.remove(invitation);
				invitation.entityAspect.setDeleted();
				//this.dataservice.saveChanges();
			};
		}




		extendOrganization(organization) {
			organization.addUserClick = () => {
				if (organization.invitationErrors().length == 0) {
					organization.invitingUser(false);
					var invitation = this.dataservice.createInvitation({ EmailAddress: organization.emailAddressInput() });

					if (invitation.entityAspect.validateEntity()) {
						this.extendInvitation(invitation);
						organization.Invitations.push(invitation);
						this.dataservice.saveChanges();
					}
				} else {
					organization.invitationErrors.showAllMessages();
				}


			};


			organization.addProjectClick = () => {
				organization.creatingProject(false);
				var project = this.dataservice.createProject();
				project.Name(organization.projectNameInput());

				if (project.entityAspect.validateEntity()) {
					organization.extendProject(project);
					organization.Projects.push(project);
					this.dataservice.saveChanges();
				}
			};

			organization.extendProject = (project) => {

			};

			organization.destroy = () => {
				this.organizations.remove(organization);
				organization.entityAspect.setDeleted();
				this.dataservice.saveChanges();
			};

			organization.emailAddressInput = ko.observable('').extend({ required: true, email: true });
			organization.invitationErrors = ko.validation.group(organization, { deep: false });



			organization.cancelUserInvite = () => {
				organization.invitingUser(false);
			};
			organization.inviteUserClick = () => {
				organization.invitingUser(true);
			};
			organization.invitingUser = ko.observable(false);

			organization.projectNameInput = ko.observable('');
			organization.creatingProject = ko.observable(false);

			organization.createProjectClick = () => {
				organization.creatingProject(true);
			};


			organization.cancelCreatingProject = () => {
				organization.creatingProject(false);
			};

			$.each(organization.Invitations(), (i1, i2) => {
				this.extendInvitation(i2);
			});

		};

		handleItemErrors = (item) => {
			if (!item) {
				return;
			}
			var errs = item.entityAspect.getValidationErrors();
			if (errs.length == 0) {
				logger.Proofted.Logger.info("No errors for current item");
				return;
			}
			var firstErr = item.entityAspect.getValidationErrors()[0];
			logger.Proofted.Logger.error(firstErr.errorMessage);
			item.entityAspect.rejectChanges(); // harsh for demo 
		};

		getAllOrganizations() {
			this.dataservice.getAllOrganizations()
				.then(this.querySucceeded)
				.fail(this.queryFailed);
		};

			querySucceeded: (data) => void;

		queryFailed(error) {
			logger.Proofted.Logger.error(error, "Query failed");
		};




	};

}