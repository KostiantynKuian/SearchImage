(function () {
    "use strict";

    angular.module("imageSearchApp", ['ngLoader', 'ngImageCompress']).config(function ($compileProvider, $httpProvider) {
        $compileProvider.aHrefSanitizationWhitelist(/^\s*(https?|file|ftp|blob):|data:image\//);
       // $httpProvider.interceptors.push('httpMovedInterceptor');
    });

    //angular.module("imageSearchApp", []).config(function ($httpProvider) {
    //    $httpProvider.interceptors.push('httpMovedInterceptor');
    //});

}());