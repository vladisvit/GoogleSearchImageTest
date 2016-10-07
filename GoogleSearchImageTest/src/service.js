(function () {
    'use strict';

    angular.module("apptest")
       .factory("searchService", searchService);

    searchService.$inject = ["$http"];

    function searchService($http) {

        var searchProcessor = {};
        var cx = "014337679278764266478%3Aqkzs-j2iy1g&key=AIzaSyA1u-0AaFC9fFpMWTSyu6f6ki2wb19na_g";

        searchProcessor.search =
            function (queryStr) {
                return $http.get("https://www.googleapis.com/customsearch/v1?q=" + queryStr + "&cx=" + cx);
            };

        searchProcessor.save = function(resultData) {
            return $http.post("/api/result/", resultData);
        };

        searchProcessor.load = function(id) {
            return $http.get("/api/result/"+id);
        };

        searchProcessor.isSplit = function(str, token) {
            return (str || '').split(token).length > 1;
        };

        return searchProcessor;
    };

})();