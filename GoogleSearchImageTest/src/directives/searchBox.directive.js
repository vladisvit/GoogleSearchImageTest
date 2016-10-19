angular.module('apptest').directive('searchBox', function () {
    function link(scope, el, attrs, controller) {
        scope.isDisabled = true;

        scope.doSearch = function () {
            controller.searchInput = scope.searchInput;
            controller.doSearch();
            scope.pager = controller.pager;
            scope.result = controller.result;
            scope.isDisabled = false;
        };

        scope.saveResult = function () {
            controller.result = scope.result;
            controller.saveResult();
            scope.isDisabled = true;
        };

        scope.deleteItem = function(item) {
            item.deleted = !item.deleted;
            scope.isDisabled = false;
        };

        scope.setPage = function(page) {
            controller.setPage(page);
            scope.pager = controller.pager;
        };

        scope.$on("searchDone", function (event, data) {
            scope.result = data;
            controller.searchInput = scope.searchInput;
            scope.pager = controller.pager;
        });

        scope.$on("resultSaved", function(event, data) {
            scope.result = data;
            scope.pager = controller.pager;
        });

        scope.$on("resultLoaded", function (event, data) {
            scope.result = data;
            scope.searchInput = controller.searchInput;
            scope.pager = controller.pager;
        });
    }

    return {
        restrict: 'E',
        require: '^mainDirective',
        templateUrl:'src/directives/searchBox.tmpl.html',
        link: link,
        scope: {
            annotations: '='
        }
    };
});