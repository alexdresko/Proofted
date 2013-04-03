/// <reference path="../typings/jquery/jquery.d.ts" />
/// <reference path="../typings/requirejs/require.d.ts" />

require.config({
    paths: {
        "breeze": "../breeze.debug",
        "jQuery": "../jquery-1.9.1",
        "bootstrap": "../bootstrap",
        "ko": "../knockout-2.2.0"
    },
    shim: {
        "jQuery": {
            deps: [],
            init: function () {
                return $;
            }
        }
    }
});


import logger = module("Logger2");
import _ko = module("KO");
export module Proofted {
    logger.Proofted.Logger.info("Prooted is booting");

    require(['shellViewModel'],

    function (shellViewModel) {
        ko.applyBindings(shellViewModel);

    });

}