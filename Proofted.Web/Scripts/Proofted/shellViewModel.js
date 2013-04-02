"use strict";
	define(['ko', 'logger', 'dataservice'], function (ko, logger, dataservice) {
	
// ReSharper disable InconsistentNaming
	var ViewModel = function() {

		// ReSharper restore InconsistentNaming
	    var self = this;
		self.organizations = ko.observableArray([]);
		self.newOrganizationClick = function() {
			self.editingOrganization(true);
		};
	    


		self.editingOrganization = ko.observable(false);

	    self.modal = {
	        header: ko.observable("This is a modal"),
	        body: ko.observable("Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum."),
	        closeLabel: "Close",
	        primaryLabel: "Do Something",
	        onClose: function() {
	            self.onModalClose();
	        },
	        onAction: function() {
	            self.onModalAction();
	        }
	    };



	    self.onModalClose = function() {
	        alert("CLOSE!");
	    };
	    self.onModalAction = function() {
	        alert("ACTION!");
	    };


		self.organizationNameInput = ko.observable('');
		self.organizationSubmit = function() {
		    self.editingOrganization(false);
		    var newOrganization = dataservice.createOrganization();
			newOrganization.Name(self.organizationNameInput());
		    
			if (newOrganization.entityAspect.validateEntity()) {
				extendOrganization(newOrganization);
				self.organizations.push(newOrganization);
				dataservice.saveChanges();
				self.organizationNameInput("");
			} else {
				handleItemErrors(newOrganization);

			}
		};
		self.cancelOrganizationForm = function() {
			self.editingOrganization(false);
		};

		self.rendered = ko.observable(false);
	    self.finishRender = function() {
	        self.rendered(true);
	    };
		
		function extendInvitation(invitation) {
			invitation.destroy = function () {
				invitation.Organization().Invitations.remove(invitation);
				invitation.entityAspect.setDeleted();
				dataservice.saveChanges();
			};
		}
	    
		

		
		function extendOrganization(organization) {
		    organization.addUserClick = function () {
		        if (organization.invitationErrors().length == 0) {
		            organization.invitingUser(false);
		            var invitation = dataservice.createInvitation({ EmailAddress: organization.emailAddressInput() });

		            if (invitation.entityAspect.validateEntity()) {
		                extendInvitation(invitation);
		                organization.Invitations.push(invitation);
		                dataservice.saveChanges();
		            }
		        } else {
		            organization.invitationErrors.showAllMessages();
		        }
		        

			};
		   

		    organization.addProjectClick = function() {
		        organization.creatingProject(false);
		        var project = dataservice.createProject();
		        project.Name(organization.projectNameInput());

		        if (project.entityAspect.validateEntity()) {
		            organization.extendProject(project);
		            organization.Projects.push(project);
		            dataservice.saveChanges();
		        }
		    };

		    organization.extendProject = function(project) {

		    };

			organization.destroy = function() {
				self.organizations.remove(organization);
				organization.entityAspect.setDeleted();
				dataservice.saveChanges();
			};

			organization.emailAddressInput = ko.observable('').extend({ required: true, email: true });
			organization.invitationErrors = ko.validation.group(organization, { deep: false });



			organization.cancelUserInvite = function () {
			    organization.invitingUser(false);
			};
			organization.inviteUserClick = function () {
			    organization.invitingUser(true);
			};
			organization.invitingUser = ko.observable(false);

		    organization.projectNameInput = ko.observable('');
		    organization.creatingProject = ko.observable(false);
		    
		    organization.createProjectClick = function() {
		        organization.creatingProject(true);
		    };
		    

		    organization.cancelCreatingProject = function() {
		        organization.creatingProject(false);
		    };

			$.each(organization.Invitations(), function(i1, i2) {
				extendInvitation(i2);
			});

		};
		self.handleItemErrors = function(item) {
			if (!item) {
				return;
			}
			var errs = item.entityAspect.getValidationErrors();
			if (errs.length == 0) {
				logger.info("No errors for current item");
				return;
			}
			var firstErr = item.entityAspect.getValidationErrors()[0];
			logger.error(firstErr.errorMessage);
			item.entityAspect.rejectChanges(); // harsh for demo 
		};

		self.getAllOrganizations = function() {
			dataservice.getAllOrganizations()
				.then(querySucceeded)
				.fail(queryFailed);
		};

		function querySucceeded(data) {
			self.organizations([]);
			data.results.forEach(function(item) {
				extendOrganization(item);
				self.organizations.push(item);
			});
			logger.info("Fetched Orgs ");
		};

		function queryFailed(error) {
			logger.error(error, "Query failed");
		};

		


	};

	var vm = new ViewModel();
	vm.getAllOrganizations();

	return vm;
	});

