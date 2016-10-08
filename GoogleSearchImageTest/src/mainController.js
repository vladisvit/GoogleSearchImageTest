(function () {
    'use strict';

    angular
        .module('apptest')
        .controller('mainController', mainController);

    mainController.$inject = ['searchService'];

    function mainController(searchService) {
        var vm = this;
        vm.title = 'controller';
        vm.result = {};
        vm.result.items = [];
        vm.searchInput = '';

        function getNeedData(respData) {
            if (Object.keys(respData).length === 0) {
                return;
            }

            vm.result.name = vm.searchInput;
            var items = respData.items;
            for (var i = 0; i < items.length; i++) {
                if (!items[i].pagemap.hasOwnProperty('cse_image')) {
                    continue;
                }
                var itemObj = {};
                itemObj.title = items[i].title;
                itemObj.htmlTitle = items[i].htmlTitle;

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
            searchService.search(vm.searchInput).then(function (response) {
                vm.result = {};
                vm.result.items = [];
                getNeedData(response.data);
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
                vm.result = d;
            });
        };

        vm.deleteItem = function (item) {
            item.Deleted = !item.Deleted;
        };
    }
})();
