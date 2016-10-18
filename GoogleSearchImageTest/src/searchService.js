(function () {
    'use strict';

    angular.module("apptest")
       .factory("searchService", searchService);

    searchService.$inject = ["$http"];

    function searchService($http) {

        var searchProcessor = {};
        var cx = "014337679278764266478%3Aqkzs-j2iy1g&key=AIzaSyA1u-0AaFC9fFpMWTSyu6f6ki2wb19na_g";
        searchProcessor.numberOfResult = 8;

        searchProcessor.search =
            function (queryStr, index) {
                return $http.get("https://www.googleapis.com/customsearch/v1?q=" + queryStr + "&cx=" + cx
                    + "&num=" + searchProcessor.numberOfResult + "&fields=items,queries&start=" + index);
            };

        searchProcessor.save = function (resultData) {
            delete resultData.queries;
            delete resultData.items.hide;
            return $http.post("/api/result/", resultData);
        };

        searchProcessor.load = function (id) {
            if (!id) {
                id = "";
            }
            return $http.get("/api/result/" + id);
        };

        searchProcessor.delete = function(id) {
            return $http.delete("/api/result/"+ id);
        };

        searchProcessor.refresh = function() {
            window.location.reload();
        };

        searchProcessor.isSplit = function(str, token) {
            return (str || '').split(token).length > 1;
        };

        return searchProcessor;
    };

})();