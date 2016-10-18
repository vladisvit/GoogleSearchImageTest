angular.module('apptest').directive('archBox', function () {
    function link(scope, el, attrs, controller) {
        scope.previousResults = controller.previousResults;
        scope.loadResult = function(id) {
            controller.loadResult(id);
        };

        scope.deleteResult = function(id) {
            controller.deleteResult(id);
        };

        scope.$on('resultsLoaded', function (event, data) {
            scope.previousResults = data;
        });
    }

    return {
        restrict: 'E',
        require: '^mainDirective',
        templateUrl: 'src/directives/archBox.tmpl.html',
        link: link,
        scope: {
            annotations: '='
        }
    };
});