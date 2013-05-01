define(["require", "exports", "Logger2", "DataService2"], function(require, exports, __logger__, ___dataservice__) {
    var logger = __logger__;

    var _dataservice = ___dataservice__;

    (function (Proofted) {
        var ShellViewModel = (function () {
            function ShellViewModel() {
                var _this = this;
                this.dataservice = new _dataservice.Proofted.DataService();
                this.organizations = ko.observableArray([]);
                this.editingOrganization = ko.observable(false);
                this.organizationNameInput = ko.observable('');
                this.rendered = ko.observable(false);
                this.handleItemErrors = function (item) {
                    if(!item) {
                        return;
                    }
                    var errs = item.entityAspect.getValidationErrors();
                    if(errs.length == 0) {
                        logger.Proofted.Logger.info("No errors for current item");
                        return;
                    }
                    var firstErr = item.entityAspect.getValidationErrors()[0];
                    logger.Proofted.Logger.error(firstErr.errorMessage);
                    item.entityAspect.rejectChanges();
                };
                this.finishRender = function () {
                    _this.rendered(true);
                };
                this.querySucceeded = function (data) {
                    _this.organizations([]);
                    data.results.forEach(function (item) {
                        _this.extendOrganization(item);
                        _this.organizations.push(item);
                    });
                    logger.Proofted.Logger.info("Fetched Orgs ");
                };
            }
            ShellViewModel.prototype.newOrganizationClick = function () {
                this.editingOrganization(true);
            };
            ShellViewModel.prototype.organizationSubmit = function () {
                this.editingOrganization(false);
                var newOrganization = this.dataservice.createOrganization();
                newOrganization.Name(this.organizationNameInput());
                if(newOrganization.entityAspect.validateEntity()) {
                    this.extendOrganization(newOrganization);
                    this.organizations.push(newOrganization);
                    this.dataservice.saveChanges();
                    this.organizationNameInput("");
                } else {
                    this.handleItemErrors(newOrganization);
                }
            };
            ShellViewModel.prototype.cancelOrganizationForm = function () {
                this.editingOrganization(false);
            };
            ShellViewModel.prototype.extendInvitation = function (invitation) {
                invitation.destroy = function () {
                    invitation.Organization().Invitations.remove(invitation);
                    invitation.entityAspect.setDeleted();
                };
            };
            ShellViewModel.prototype.extendOrganization = function (organization) {
                var _this = this;
                organization.addUserClick = function () {
                    if(organization.invitationErrors().length == 0) {
                        organization.invitingUser(false);
                        var invitation = _this.dataservice.createInvitation({
                            EmailAddress: organization.emailAddressInput()
                        });
                        if(invitation.entityAspect.validateEntity()) {
                            _this.extendInvitation(invitation);
                            organization.Invitations.push(invitation);
                            _this.dataservice.saveChanges();
                        }
                    } else {
                        organization.invitationErrors.showAllMessages();
                    }
                };
                organization.addProjectClick = function () {
                    organization.creatingProject(false);
                    var project = _this.dataservice.createProject();
                    project.Name(organization.projectNameInput());
                    if(project.entityAspect.validateEntity()) {
                        organization.extendProject(project);
                        organization.Projects.push(project);
                        _this.dataservice.saveChanges();
                    }
                };
                organization.extendProject = function (project) {
                };
                organization.destroy = function () {
                    _this.organizations.remove(organization);
                    organization.entityAspect.setDeleted();
                    _this.dataservice.saveChanges();
                };
                organization.emailAddressInput = ko.observable('').extend({
                    required: true,
                    email: true
                });
                organization.invitationErrors = ko.validation.group(organization, {
                    deep: false
                });
                organization.cancelUserInvite = function () {
                    organization.invitingUser(false);
                };
                organization.inviteUserClick = function () {
                    organization.invitingUser(true);
                };
                organization.invitingUser = ko.observable(false);
                organization.projectNameInput = ko.observable('');
                organization.creatingProject = ko.observable(false);
                organization.createProjectClick = function () {
                    organization.creatingProject(true);
                };
                organization.cancelCreatingProject = function () {
                    organization.creatingProject(false);
                };
                $.each(organization.Invitations(), function (i1, i2) {
                    _this.extendInvitation(i2);
                });
            };
            ShellViewModel.prototype.getAllOrganizations = function () {
                this.dataservice.getAllOrganizations().then(this.querySucceeded).fail(this.queryFailed);
            };
            ShellViewModel.prototype.queryFailed = function (error) {
                logger.Proofted.Logger.error(error, "Query failed");
            };
            return ShellViewModel;
        })();
        Proofted.ShellViewModel = ShellViewModel;        
        ; ;
    })(exports.Proofted || (exports.Proofted = {}));
    var Proofted = exports.Proofted;
})
//@ sourceMappingURL=ShellViewModel2.js.map
