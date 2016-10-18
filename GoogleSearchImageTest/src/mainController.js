(function () {
    'use strict';

    angular
        .module('apptest')
        .controller('mainController', mainController);

    mainController.$inject = ['searchService', 'pagerService'];

    function mainController(searchService, pagerService) {
        var vm = this;
        vm.title = 'controller';
        vm.result = {};
        vm.result.items = [];
        vm.searchInput = '';
        vm.previousResults = [];
        vm.pager = {};
        vm.setPage = function(page) {
            if (page < 1 || page > vm.pager.totalPages) {
                return;
            }

            vm.pager = pagerService.getPager(vm.result, page);
        };

        vm.doSearch = function() {
            var startIndex = 1;
            vm.result = {};
            vm.result.items = [];
            // google api has restriction for free version 
            // 100 requests per day and 10 result items per requests
            for (var i = 1; i < 11; i++) {
                searchService.search(vm.searchInput, startIndex).then(function(response) {
                    getNeedData(response.data);
                    // get pager object from service
                    vm.pager = pagerService.getPager(vm.result, 1);
                });

                startIndex = i * searchService.numberOfResult + 1;
            }
        };

        vm.saveResult = function() {
            var saveResult = {};
            saveResult = vm.result;
            searchService.save(saveResult).then(function(response) {
                var d = response.data;
                vm.loadResults();
                processingResponse(d);
            });
        };

        vm.loadResults = function() {
            searchService.load().then(function(response) {
                vm.previousResults = response.data;
            });
        };

        vm.loadResult = function(id) {
            searchService.load(id).then(function(response) {
                processingResponse(response.data);
            });
        };

        vm.deleteResult = function(id) {
            searchService.delete(id).then(function(response) {});
        };

        vm.deleteItem = function(item) {
            item.deleted = !item.deleted;
        };

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
            query.count = 10;
            query.startIndex = 1;
            d.queries.request.push(query);

            vm.result = d;
            vm.searchInput = d.name;
            vm.pager = pagerService.getPager(vm.result, 1);
        };

    }
})();
