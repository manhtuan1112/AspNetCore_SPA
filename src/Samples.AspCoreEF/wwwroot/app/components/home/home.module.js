/// <reference path="/Assests/admin/libs/angular/angular.js" />
(function () {
    angular.module('onlineshop.home', ['onlineshop.common']).config(config);
    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state("home", {
                url: "/home",
                parent: "base",
                templateUrl: "/app/components/home/homeView.html",
                controller: "homeController"
            });
        $urlRouterProvider.otherwise('/login');
    }
})();
