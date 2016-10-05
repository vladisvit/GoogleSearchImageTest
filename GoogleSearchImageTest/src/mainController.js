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
        vm.searchInput = '';
        vm.doSearch = function () {
            searchService.search(vm.searchInput).then(function (response) {
                vm.result = response.data;
            });
        };
    }
})();
