(function () {

    angular.module('apptest', ['ngSanitize', 'ngAnimate', 'ui.bootstrap', 'angular-loading-bar', 'ngCookies'])
        .config(function ($logProvider) {
            $logProvider.debugEnabled(true);
        });
})();