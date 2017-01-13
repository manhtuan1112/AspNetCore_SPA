(function (app) {
    app.controller('rootController', rootController);
    rootController.$inject = ['$scope', '$state'];
    function rootController($scope, $state) {
        $scope.logOut = function () {
            //loginService.logOut();
            $state.go('login');
        }
        //$scope.authentication = authData.authenticationData;
        //authenticationService.validateRequest();
    }

})(angular.module('onlineshop'));