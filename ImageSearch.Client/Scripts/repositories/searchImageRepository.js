angular.module("imageSearchApp").service("serarchImageRepository", function($http) {
    this.httpService = $http;
    var that = this;
    that._api = "http://localhost:61705/api/searchImage/";
    var config = { params: {} };

    return {
        getImageByKeyword: function(keyword, startFrom) {
            var encodeKeyword = encodeURIComponent(keyword);
            config.params = {};
            config.params.keyword = encodeKeyword;
            config.params.startFrom = startFrom;

            return that.httpService.get(that._api + "searchImage/searchImage", config);
        },

        saveFilter: function(urls) {
            return that.httpService.post(that._api + "saveFilter", urls);
        },

        initializeFilter: function() {
            return that.httpService.get(that._api + "initializeFilter");
        },

        loadFilter: function(id) {
            config.params = {};
            config.params.filterId = id;

            return that.httpService.get(that._api + "loadFilter", config);
        },

        removeFilter: function(id) {
            return that.httpService.post(that._api + "removeFilterItem", id);
        },

        downloadImage: function(link) {
            var encodeUrl = encodeURIComponent(link);

            return that.httpService.get(that._api + "downloadImage?url=" + encodeUrl, {
                responseType: "arraybuffer"
            });
        }
    };
});