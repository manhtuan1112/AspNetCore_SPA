(function (app) {
    app.controller('productListController', productListController);
    productListController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox', '$filter'];
    function productListController($scope, apiService, notificationService, $ngBootbox, $filter) {
        $scope.products = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.getProducts = getProducts;

        $scope.keyword = '';

        $scope.search = search;
        $scope.deleteProduct = deleteProduct;

        $scope.deleteMultiple = deleteMultiple;
        function deleteMultiple() {
            var listId = [];
            $.each($scope.selected, function (i, item) {
                listId.push(item.ID);
            });
            var config = {
                params: {
                    checkedProducts: JSON.stringify(listId)
                }
            }
            apiService.del('/api/product/deletemulti', config, function (result) {
                notificationService.displaySuccess('Xóa thành công ' + result.data + ' bản ghi.')
                search();
            }, function (error) {
                notificationService.displayError('Xóa không thành công');
            });
        }


        $scope.selectAll = selectAll;
        $scope.isAll = false;
        function selectAll() {
            if ($scope.isAll === false) {
                angular.forEach($scope.products, function (item) {
                    item.checked = true;
                });
                $scope.isAll = true;
            }
            else {
                angular.forEach($scope.products, function (item) {
                    item.checked = false;
                });
                $scope.isAll = false;
            }
        }

        $scope.$watch("products", function (n, o) {
            var checked = $filter("filter")(n, { checked: true });
            if (checked.length) {
                $scope.selected = checked;
                $('#btnDelete').removeAttr('disabled');
            } else {
                $('#btnDelete').attr('disabled', 'disabled');
            }
        }, true);

        function deleteProduct(id) {
            $ngBootbox.confirm('Are you sure to delete?').then(function () {
                var config = {
                    params: {
                        id: id
                    }
                }
                apiService.del('/api/product/delete', config, function () {
                    notificationService.displaySuccess('Delete Sucessfully');
                    search();
                }, function (error) {
                    notificationService.displayError('Failed to delete');
                });
            });
        }
        function search() {
            getProducts();
        }
        function getProducts(page) {
            page = page || 0;
            var config = {
                params: {
                    keyword: $scope.keyword,
                    page: page,
                    pageSize: 5
                }
            }
            apiService.get('/api/product/getall', config, function (result) {
                if (result.data.TotalCount == 0) {
                    notificationService.displayWarning('No Records Found');
                }
                else {
                    //notificationService.displaySuccess('Đã tìm thấy ' + result.data.TotalCount + ' bản ghi.')
                }
                $scope.products = result.data.items;
                $scope.page = result.data.page;
                $scope.pagesCount = result.data.totalPages;
                $scope.totalCount = result.data.totalCount;


            }, function () {
                console.log('Load Product  Fail.');

            });
        }

        $scope.getProducts();
    }
})(angular.module('onlineshop.products'));