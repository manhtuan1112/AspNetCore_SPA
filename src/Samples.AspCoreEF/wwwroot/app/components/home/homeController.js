(function (app) {
    app.controller('homeController', homeController);
    homeController.$inject = ['apiService', '$scope'];
    function homeController(apiService, $scope) {
        //$scope.getTestMethod = getTestMethod;

        //function getTestMethod() {
        //    apiService.get('/api/home/testmethod', null, function (result) {
        //        $scope.message = result.data;
        //        console.log(result.data);
        //    }, function () {
        //        console.log('Load Product  Fail.');

        //    });
        //}
        //$scope.getTestMethod();
    }
})(angular.module('onlineshop.home'));