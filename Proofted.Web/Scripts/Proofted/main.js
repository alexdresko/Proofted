//requirejs.config(
//    {
//        // well-know paths to selected scripts
//        paths: {
//            'breeze': '../breeze.debug'
//        }
//    }
//);

//define(['logger', 'breeze'], function(logger) {

//    logger.info('Breeze Todo is booting');

//    require(['shellViewModel'],        
//        function(shellViewModel) {
//            ko.applyBindings(shellViewModel);

//        });
//});

(function (root) {

    requirejs.config({
        paths: { // well-known paths to selected scripts
            'breeze': '../breeze.debug', // load debug version of breeze
        }
    });

    // Register with require these 3rd party libs 
    // which are now in the root global namespace
    define('jquery', function () { return root.jQuery; });
    define('ko', function () { return root.ko; });

    //  Launch the app
    define(['jquery', 'ko', 'logger'], function ($, ko, logger) {

        logger.info('Breeze Todo is booting');

        require(['shellViewModel'],

            function (shellViewModel) {
                ko.applyBindings(shellViewModel);

            });
    });

})(window);