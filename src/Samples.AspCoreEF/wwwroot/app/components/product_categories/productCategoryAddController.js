(function (app) {
    app.controller('productCategoryAddController', productCategoryAddController);
    productCategoryAddController.$inject = ['apiService', '$scope', 'notificationService', '$state', 'commonService'];
    function productCategoryAddController(apiService, $scope, notificationService, $state, commonService) {
        $scope.productCategory = {
            addedDate: new Date(),
            modifiedDate: new Date(),
            status:true
        }
        $scope.AddProductCategory = AddProductCategory;
        $scope.GetSeoTitle = GetSeoTitle;

        function GetSeoTitle() {
            $scope.productCategory.alias = commonService.getSeoTitle($scope.productCategory.name);
        }

        function AddProductCategory() {
            apiService.post('/api/productcategory/create', $scope.productCategory,
            function (result) {
                notificationService.displaySuccess($scope.productCategory.name + ' is added successful');
                $state.go('product_categories');
            },
            function (error) {
                notificationService.displayError('Failed to add new product category');

            });
        }

        function loadParentCategory() {
            apiService.get('/api/productcategory/getallparents', null, function (result) {
                $scope.parentCategories = result.data;
            },
            function () {
                console.log('cannot get list parent');
            });
        }
        loadParentCategory();
    }
})(angular.module('onlineshop.product_categories'));