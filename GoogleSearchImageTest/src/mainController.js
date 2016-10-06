(function () {
    'use strict';

    angular
        .module('apptest')
        .controller('mainController', mainController);

    mainController.$inject = ['searchService'];

    function mainController(searchService) {


        var vm = this;
        vm.title = 'controller';
        vm.result = [];
        vm.searchInput = '';

        function getNeedData(respData) {
            if (Object.keys(respData).length === 0) {
                return;
            }
            var items = respData.items;
            for (var i = 0; i < items.length; i++) {
                var itemObj = {};
                itemObj.title = items[i].title;
                itemObj.htmlTitle = items[i].htmlTitle;
                itemObj.thumbnail = items[i].pagemap.cse_thumbnail;

                if (!items[i].pagemap.hasOwnProperty('cse_image')) {
                    continue;
                }

                var splitChar = "/";
                itemObj.image = items[i].pagemap.cse_image[0];
                if (searchService.isSplit(itemObj.image.src, splitChar)) {
                    itemObj.image.filename = itemObj.image.src.split(splitChar).pop();
                }

                vm.result.push(itemObj);
            }
        };

        vm.doSearch = function () {
            searchService.search(vm.searchInput).then(function (response) {
                vm.result = [];
                getNeedData(response.data);
            });
        };

        vm.saveResult = function() {
            searchService.load().then(function(response) {
                vm.result = [];
                var d = response.data;
                vm.result = d[0].items;
            });
        };

        vm.loadResult = function (id) {
            searchService.load(id).then(function (response) {
                vm.result = [];
                var d = response.data;
                vm.result = d[0].items;
            });
        };
    }
})();
