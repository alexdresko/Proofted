/// <reference path="../typings/requirejs/require.d.ts" />
require.config({
	baseUrl: "Scripts/proofted",
	paths: {
		"Breeze": "../breeze.debug"
	}

});

require(["Main2"]);