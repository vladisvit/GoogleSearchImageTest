angular.module('apptest').directive('mainDirective', mainDirective);

mainDirective.$inject = ['searchService', 'pagerService', 'notificationService'];

function mainController($scope, searchService, pagerService, notificationService) {
    var vm = this;
    vm.title = 'controller';
    vm.result = {};
    vm.result.items = [];
    vm.searchInput = '';
    vm.previousResults = [];
    vm.pager = {};
    vm.setPage = function (page) {
        if (page < 1 || page > vm.pager.totalPages) {
            return;
        }

        vm.pager = pagerService.getPager(vm.result, page);
    };

    vm.doSearch = function () {
        var startIndex = 1;
        vm.result = {};
        vm.result.items = [];
        // google api has restriction for free version 
        // 100 requests per day and less 10 result items per requests
        // this "for" gives about 80 images
        for (var i = 1; i < 11; i++) {
            searchService.search(vm.searchInput, startIndex).then(function (response) {
                getNeedData(response.data);
                // get pager object from service
                vm.pager = pagerService.getPager(vm.result, 1);
                $scope.$broadcast('searchDone', vm.result);
                notificationService.success("Found images-" + vm.result.items.length);
            }, function errorCallback(response) {
                notificationService.error("Searching error!");
            });

            startIndex = i * searchService.numberOfResult + 1;
        }
    };

    vm.saveResult = function () {
        var saveResult = {};
        saveResult = vm.result;
        searchService.save(saveResult).then(function successCallback(response) {
            var d = response.data;
            vm.loadResults();
            processingResponse(d);
            $scope.$broadcast('resultSaved', vm.result);
            notificationService.success("Successful saving - " + vm.searchInput);
        }, function errorCallback(response) {
            notificationService.error("Unsuccessful saving - " + vm.searchInput);
        });
    };

    vm.loadResults = function () {
        searchService.load().then(function successCallback(response) {
            vm.previousResults = response.data;
            $scope.$broadcast('resultsLoaded', vm.previousResults);
        }, function errorCallback(response) {
            notificationService.error("Unsuccessful loading");
        });
    };

    vm.loadResult = function (id) {
        searchService.load(id).then(function successCallback(response) {
            processingResponse(response.data);
            $scope.$broadcast('resultLoaded', vm.result);
        }, function errorCallback(response) {
            notificationService.error("Unsuccessful loading");
        });
    };

    vm.deleteResult = function (id) {
        searchService.delete(id).then(function successCallback(response) {
            searchService.refresh();
            notificationService.success("Successful deleting");
        }, function errorCallback(response) {
            notificationService.error("Unsuccessful deleting");
        });
    };

    vm.loadResults();
    // get needed data from the google result object
    function getNeedData(respData) {
        var gdata = respData;
        if (Object.keys(gdata).length === 0) {
            return;
        }

        vm.result.name = vm.searchInput;
        vm.result.queries = gdata.queries;
        var items = gdata.items;
        for (var i = 0; i < items.length; i++) {
            if (!items[i].hasOwnProperty("pagemap") || !items[i].pagemap.hasOwnProperty('cse_image')) {
                continue;
            }
            var itemObj = {};
            itemObj.title = items[i].title;
            itemObj.htmlTitle = items[i].htmlTitle;
            itemObj.hide = false;
            // set a file name
            var splitChar = "/";
            itemObj.src = items[i].pagemap.cse_image[0].src;
            if (searchService.isSplit(itemObj.src, splitChar)) {
                itemObj.fileName = itemObj.src.split(splitChar).pop();
            }

            vm.result.items.push(itemObj);
        }
    };

    //init with the first item
    function processingResponse(data) {
        var d = data;
        d.queries = {};
        d.queries.request = [];
        var query = {};
        query.totalResults = d.items.length;
        query.count = searchService.numberOfResult;
        query.startIndex = 1;
        d.queries.request.push(query);

        vm.result = d;
        vm.searchInput = d.name;
        vm.pager = pagerService.getPager(vm.result, 1);
    };

};

function mainDirective($scope, searchService, pagerService, notificationService) {
    return {
        restrict: 'E',
        template: [
          '<div class="row">',
          '<search-box search.config="configuration"></search-box>',
          '<arch-box></arch-box',
          '</div>'
        ].join('\n'),
        controller: ['$scope','searchService', 'pagerService', 'notificationService', mainController],
        scope: {
            configuration: '='
        }
    }
};