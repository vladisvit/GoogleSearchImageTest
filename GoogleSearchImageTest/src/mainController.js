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

        vm.pager = {};
        vm.setPage = setPage;


        function setPage(page) {
            if (page < 1 || page > vm.pager.totalPages) {
                return;
            }
            var startIndex = vm.pager.startIndex;
            if (vm.result.queries.nextPage) {
                if (vm.pager.currentPage < page) {
                    startIndex = vm.result.queries.nextPage[0].startIndex;
                } else if (vm.pager.currentPage > page) {
                    startIndex = vm.result.queries.previousPage[0].startIndex;
                }

                searchService.search(vm.searchInput, startIndex).then(function(response) {
                    //vm.result = {};
                    //vm.result.items = [];
                    getNeedData(response.data);

                    vm.pager = pagerService.getPager(vm.result, page);
                });
            } else {
                if (vm.pager.currentPage < page) {
                    vm.result.queries.request[0].startIndex = vm.pager.startIndex + vm.result.queries.request[0].count;
                } else if (vm.pager.currentPage > page) {
                    vm.result.queries.request[0].startIndex = vm.pager.startIndex - vm.result.queries.request[0].count;
                }
                
                vm.pager = pagerService.getPager(vm.result, page);
            }
        }

        function getNeedData(respData) {
            if (Object.keys(respData).length === 0) {
                return;
            }

            vm.result.name = vm.searchInput;
            var queries = respData.queries;
            vm.result.queries = queries;
            var items = respData.items;
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

        vm.doSearch = function () {
            searchService.search(vm.searchInput, vm.pager.startIndex || 1).then(function (response) {
                //vm.result = {};
                //vm.result.items = [];
                getNeedData(response.data);
                // get pager object from service
                vm.pager = pagerService.getPager(vm.result, 1);
            });
        };

        vm.saveResult = function () {
            var saveResult = {};
            saveResult = vm.result;
            searchService.save(saveResult).then(function (response) {
                var d = response.data;
                searchService.refresh();
            });
        };

        vm.loadResult = function (id) {
            searchService.load(id).then(function (response) {
                var d = response.data;
                d.queries = {};
                d.queries.request = [];
                var query = {};
                query.totalResults = d.items.length;
                query.count = 10;
                query.startIndex = 1;
                d.queries.request.push(query);

                vm.result = d;
                vm.pager = pagerService.getPager(vm.result, 1);
            });
        };

        vm.deleteItem = function (item) {
            item.deleted = !item.deleted;
        };
    }
})();
