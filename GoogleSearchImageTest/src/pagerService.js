(function () {
    'use strict';

    angular.module('apptest')
       .factory('pagerService', pagerService);

    function pagerService() {
        // service definition
        var service = {};

        service.getPager = getPager;

        return service;

        // service implementation
        function getPager(result, currentPage) {
            // default to first page
            currentPage = currentPage || 1;
            var queries = result.queries;
            var totalItems = result.items.length;
            // default page size is 10
            var pageSize = queries.request[0].count || 8;

            var totalPages = Math.ceil(totalItems / pageSize);
            var startPage, endPage;
            if (totalPages <= 10) {
                // less than 10 total pages so show all
                startPage = 1;
                endPage = totalPages;
            } else {
                // more than 10 total pages so calculate start and end pages
                if (currentPage <= 6) {
                    startPage = 1;
                    endPage = 10;
                } else if (currentPage + 4 >= totalPages) {
                    startPage = totalPages - 9;
                    endPage = totalPages;
                } else {
                    startPage = currentPage - 5;
                    endPage = currentPage + 4;
                }
            }
            var startIndex = (currentPage-1) * pageSize + 1;
            var endIndex = (startIndex + pageSize) - 1;
            if (endIndex > totalItems) {
                endIndex = totalItems;
            }

            for (var k = 0; k < totalItems; k++) {
                result.items[k].hide = true;
            }

            var startShow = startIndex - 1;
            var endShow = endIndex;
            for (var j = startShow; j < endShow; j++) {
                result.items[j].hide = false;
            }

            var pages = [];
            for (var i = startPage; i < endPage+1; i++) {
                pages.push(i);
            }

            return {
                totalItems: totalItems,
                currentPage: currentPage,
                pageSize: pageSize,
                totalPages: totalPages,
                startPage: startPage,
                endPage: endPage,
                startIndex: startIndex,
                endIndex: endIndex,
                pages: pages
            };
        }
    }


})();