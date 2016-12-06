angular.module("imageSearchApp").controller("serarchImageController", [
    "$scope", "serarchImageRepository",
    function($scope, serarchImageRepository) {
        var that = this;
        this.scope = $scope;
        this.serarchImageRepository = serarchImageRepository;
        this.scope.imageList = [];
        this.scope.isFilter = false;
        this.scope.preloader = true;


        var errorCallback = function error(data, status) {
            if (status === 500) {
                console.log("Something gone wrong, internal server error");
            }
        }

        that.scope.initialize = function() {
            initializeFilter();
        };

        that.scope.search = function() {
            var startFrom = 1;
            that.scope.imageList = [];
            loadImages(startFrom);
        };

        that.scope.loadFilter = function(filterId) {
            that.serarchImageRepository.loadFilter(filterId).success(function(items) {
                that.scope.isFilter = true;
                if (items != null) {
                    that.scope.imageList = items;
                }
            }).error(errorCallback);
        };

        that.scope.removeFilter = function(filterId) {

            var indexElement = that.scope.filterList.map(function(x) { return x.id; })
                .indexOf(filterId);

            if (indexElement !== -1) {
                that.scope.filterList.splice(indexElement, 1);
            }

            that.serarchImageRepository.removeFilter(filterId).success(function(items) {
                if (items != null) {
                    that.scope.imageList = items;
                }
            }).error(errorCallback);
        };

        that.scope.showMore = function() {
            var startFrom = +that.scope.imageList.length + 1;
            loadImages(startFrom);
        };
        that.scope.downloadImage = function(url, filename) {
            that.serarchImageRepository.downloadImage(url).success(function(data, status, headers) {
                headers = headers();

                var contentType = headers["content-type"];

                var linkElement = document.createElement("a");
                try {
                    var blob = new Blob([data], { type: contentType });
                    var url = window.URL.createObjectURL(blob);

                    linkElement.setAttribute("href", url);
                    linkElement.setAttribute("download", filename);

                    var clickEvent = new MouseEvent("click", {
                        "view": window,
                        "bubbles": true,
                        "cancelable": false
                    });
                    linkElement.dispatchEvent(clickEvent);
                } catch (ex) {
                    console.log(ex);
                }
            }).error(errorCallback);
        };

        that.scope.remove = function(image) {
            var indexElement = that.scope.imageList.indexOf(image);
            if (indexElement !== -1) {
                that.scope.imageList.splice(indexElement, 1);
            }
        };
        that.scope.saveFilter = function() {
            var imageUrls = [];
            if (!that.scope.imageList.length)
                return;

            that.scope.imageList.forEach(function(item) {
                imageUrls.push(item.link);
            });
            that.serarchImageRepository.saveFilter(imageUrls).success(function() {
                initializeFilter();
            });
        };

        function initializeFilter() {
            that.serarchImageRepository.initializeFilter().success(function(items) {
                if (items != null) {
                    that.scope.filterList = items;
                }
            }).error(errorCallback)
               .finally(function () {
                that.scope.preloader = false;
            });
        }

        function loadImages(startFrom) {
            that.scope.isFilter = false;
            var keyword = that.scope.searchText;

            if (keyword == null)
                return;

            that.serarchImageRepository.getImageByKeyword(keyword, startFrom).success(function(items) {
                if (items != null) {
                    for (var i = 0; i < items.length; i++) {
                        var item = items[i];
                        item.id = that.scope.imageList.length + 1;
                        that.scope.imageList.push(item);
                    }
                }

            }).error(errorCallback);
        }
    }
]);

function imgError(image) {
    image.onerror = "";
    image.parentNode.style.display = "none";
    return true;
}