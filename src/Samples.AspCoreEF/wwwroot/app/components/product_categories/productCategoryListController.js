(function (app) {
    app.controller('productCategoryListController', productCategoryListController);
    productCategoryListController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox', '$filter'];
    function productCategoryListController($scope, apiService, notificationService, $ngBootbox, $filter) {
        $scope.productCategories = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.getProductCategories = getProductCategories;
        $scope.keyword = '';

        $scope.search = search;
        $scope.deleteProductCategory = deleteProductCategory;

        $scope.deleteMultiple = deleteMultiple;
        function deleteMultiple() {
            var listId = [];
            $.each($scope.selected, function (i, item) {
                listId.push(item.ID);
            });
            var config = {
                params: {
                    checkedProductCategories: JSON.stringify(listId)
                }
            };
            apiService.del('/api/productcategory/deletemulti', config, function (result) {
                notificationService.displaySuccess('Delete Successful! ' + result.data + ' record.');
                search();
            }, function (error) {
                notificationService.displayError('Delete Failed');
            });
        }


        $scope.selectAll = selectAll;
        $scope.isAll = false;
        function selectAll() {
            if ($scope.isAll === false) {
                angular.forEach($scope.productCategories, function (item) {
                    item.checked = true;
                });
                $scope.isAll = true;
            }
            else {
                angular.forEach($scope.productCategories, function (item) {
                    item.checked = false;
                });
                $scope.isAll = false;
            }
        }

        $scope.$watch("productCategories", function (n, o) {
            var checked = $filter("filter")(n, { checked: true });
            if (checked.length) {
                $scope.selected = checked;
                $('#btnDelete').removeAttr('disabled');
            } else {
                $('#btnDelete').attr('disabled', 'disabled');
            }
        }, true);

        function deleteProductCategory(id) {
            $ngBootbox.confirm('Are you sure to delete?').then(function () {
                var config = {
                    params: {
                        id: id
                    }
                };
                apiService.del('/api/productcategory/delete', config, function () {
                    notificationService.displaySuccess('Delete Successful');
                    search();
                }, function (error) {
                    notificationService.displayError('Delete Failed');
                });
            });
        }
        function search() {
            getProductCategories();
        }
        function getProductCategories(page) {
            page = page || 0;
            var config = {
                params: {
                    keyword: $scope.keyword,
                    page: page,
                    pageSize: 3
                }
            };
            apiService.get('/api/productcategory/getall', config, function (result) {
                if (result.data.TotalCount === 0) {
                    notificationService.displayWarning('No Records Found');
                }
                else {
                    //notificationService.displaySuccess('Found ' + result.data.totalCount + ' records.')
                }
                $scope.productCategories = result.data.items;
                $scope.page = result.data.page;
                $scope.pagesCount = result.data.totalPages;
                $scope.totalCount = result.data.totalCount;


            }, function () {
                console.log('Load Product Categories Fail.');

            });
        }
        getProductCategories();
    }
})(angular.module('onlineshop.product_categories'));