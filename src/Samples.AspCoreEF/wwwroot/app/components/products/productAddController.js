(function (app) {
    app.controller('productAddController', productAddController);
    productAddController.$inject = ['apiService','$http', '$scope', 'notificationService', '$state', 'commonService'];
    function productAddController(apiService,$http, $scope, notificationService, $state, commonService) {
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
            var file = $scope.myFile;
            console.log('file is ');
            console.dir(file);
            var fd = new FormData();
            fd.append('file', file);
          
            $http.post('/api/product/PostImage', fd, {
                  transformRequest: angular.identity,
                  headers: {'Content-Type': undefined}
            }).then(function (result) {
                $scope.product.image = result.data;
                apiService.post('/api/product/create', $scope.product,
               function (result) {
                   notificationService.displaySuccess($scope.product.name + ' is added successfully');
                   $state.go('products');
               },
               function (error) {
                   notificationService.displayError('Failed to add product');

               });


                success(result);
            }, function (error) {
               
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