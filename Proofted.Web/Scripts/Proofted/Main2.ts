/// <reference path="../typings/knockout/knockout.d.ts" />
/// <reference path="../typings/jquery/jquery.d.ts" />
/// <reference path="../typings/requirejs/require.d.ts" />

import logger = module("Logger2");
import configure = module("Configure");
import modal = module("KoModal");
export module Proofted {
	configure.Proofted.Configuration.Configure();
	
	modal.Proofted.Knockout.Modal.Setup();

    logger.Proofted.Logger.info("Prooted is booting");

    require(['shellViewModel2'],

    (shellViewModel) => {
    	var model = new shellViewModel.Proofted.ShellViewModel();

    	model.getAllOrganizations();

		ko.applyBindings(model);
	

    });

}