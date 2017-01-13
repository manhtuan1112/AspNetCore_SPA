(function (app) {
    app.controller('productAddController', productAddController);
    productAddController.$inject = ['apiService', '$scope', 'notificationService', '$state', 'commonService'];
    function productAddController(apiService, $scope, notificationService, $state, commonService) {
        $scope.product = {
            addedDate: new Date(),
            status: true
        }
        $scope.ckeditorOptions = {
            language: 'en',
            height: '200px'
        }
        $scope.AddProduct = AddProduct;
        $scope.GetSeoTitle = GetSeoTitle;

        function GetSeoTitle() {
            $scope.product.alias = commonService.getSeoTitle($scope.product.name);
        }

        function AddProduct() {
            $scope.product.moreImages = JSON.stringify($scope.moreImages);
            apiService.post('/api/product/create', $scope.product,
            function (result) {
                notificationService.displaySuccess($scope.product.name + ' is added successfully');
                $state.go('products');
            },
            function (error) {
                notificationService.displayError('Failed to add product');

            });
        }

        function loadProductCategory() {
            apiService.get('/api/productcategory/getallparents', null, function (result) {
                $scope.productCategories = result.data;
            },
            function () {
                console.log('cannot get list parent');
            });
        }

        $scope.ChooseImage = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.product.Image = fileUrl;
                })

            }
            finder.popup();
        };
        $scope.moreImages = [];
        $scope.ChooseMoreImage = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.moreImages.push(fileUrl);
                })

            }
            finder.popup();
        }

        loadProductCategory();
    }
})(angular.module('onlineshop.products'));