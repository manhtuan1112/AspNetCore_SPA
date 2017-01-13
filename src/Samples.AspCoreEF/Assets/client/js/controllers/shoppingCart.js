var cart = {
    init: function () {
        cart.loadData();
        cart.registerEvents();
    },
    registerEvents: function () {
        $('#frmPayment').validate({
            rules:{
                name: "required",
                address: "required",
                email: {
                    required: true,
                    email:true
                },
                phone:{
                    required:true,
                    phone:true
                }
            },
            messages: {
                name: "Yêu cầu nhập tên",
                address: "Yêu cập nhập địa chỉ",
                email: {
                    required: "Bạn cần nhập Email",
                    email: "Email không hợp lệ"
                },
                phone: {
                    required: "Yêu cầu nhập số điện thoại",
                    phone: "Số điện thoại không hợp lệ"
                }
            }
        });
        $('.btnAddToCart').off('click').on('click', function (e) {
            e.preventDefault();
            var productId = parseInt($(this).data('id'));
            cart.addItem(productId);
        });
        $('.btnDeleteItem').off('click').on('click', function (e) {
            e.preventDefault();
            var productId = parseInt($(this).data('id'));
            cart.deleteItem(productId);

        });
        $('.txtQuantity').off('keyup change').on('keyup change', function (e) {
            e.preventDefault();
            var quantity = parseInt($(this).val());
            var productId = parseInt($(this).data('id'));
            var price = parseFloat($(this).data('price'));
            if (isNaN(quantity) == false) {
                var amount = quantity * price;
                $('#amount_' + productId).text(cart.changeNumToCurrency(amount));
            }
            else {
                $('#amount_' + productId).text(cart.changeNumToCurrency(0));
            }
            $('#lblTotalOrder').text(cart.getTotalOrder());
            cart.updateAll();
        });
        $('#btnContinue').off('click').on('click', function (e) {
            e.preventDefault();
            window.location.href = "/";
        });
        $('#btnDeleteAll').off('click').on('click', function (e) {
            e.preventDefault();
            cart.deleteAll();
        });
        $('#btnCheckout').off('click').on('click', function (e) {
            e.preventDefault();
            $('#divCheckout').show();
        });
        $('#chkUserLoginInfo').off('click').on('click', function (e) {
            if ($(this).prop('checked'))
                cart.getLoginUser();
            else {
                $('#txtName').val('');
                $('#txtAddress').val('');
                $('#txtEmail').val('');
                $('#txtPhone').val('');
            }
        });
        $('#btnCreateOrder').off('click').on('click', function (e) {
            e.preventDefault();
            var isValid= $('#frmPayment').valid();
            if (isValid) {
                cart.createOrder();
            }
           
        });
    },
    createOrder: function () {
        var order = {
            CustomerName: $('#txtName').val(),
            CustomerAddress: $('#txtAddress').val(),
            CustomerEmail: $('#txtEmail').val(),
            CustomerMobile: $('#txtPhone').val(),
            CustomerMessage: $('#txtMessage').val(),
            PaymentMethod: "Thanh toán tiền mặt",
            Status: false
        }
        $.ajax({
            url: '/ShoppingCart/CreateOrder',
            type: 'POST',
            dataType: 'json',
            data:{
                orderViewModel:JSON.stringify(order)
            },
            success: function (response) {
                if (response.status) {
                    $('#divCheckout').hide();
                    cart.deleteAll();
                    setTimeout(function () {
                        $('.single_grid').html('Cảm ơn bạn đã đặt hàng, chúng tôi sẽ liên hệ với bạn trong thời gian sớm nhất');
                    }, 2000);
                    
                }
            }
        });
    },
    getLoginUser: function () {
        $.ajax({
            url: '/ShoppingCart/GetUser',
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.status) {
                    var user = response.data;
                    $('#txtName').val(user.FullName);
                    $('#txtAddress').val(user.Address);
                    $('#txtEmail').val(user.Email);
                    $('#txtPhone').val(user.PhoneNumber);
                }
            }
        });
    },
    addItem: function (productId) {
        $.ajax({
            url: '/ShoppingCart/Add',
            data: {
                productId: productId
            },
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.status) {
                    alert('Thêm sản phẩm thành công');
                }
                else {
                    alert(response.message);
                }
            }
        });
    },
    deleteItem: function (productId) {
        $.ajax({
            url: '/ShoppingCart/DeleteItem',
            data: {
                productId: productId
            },
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.status) {
                    cart.loadData();
                }
            }
        });
    },
    deleteAll: function () {
        $.ajax({
            url: '/ShoppingCart/DeleteAll',
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.status) {
                    cart.loadData();
                    $('.single_grid').html('Không có sản phẩm nào trong giỏ hàng. ');
                }
            }
        });
    },
    updateAll: function () {
        cartList = [];
        $.each($('.txtQuantity'), function (i, item) {
            cartList.push({
                ProductId: $(item).data('id'),
                Quantity: $(item).val()
            });
        });
        $.ajax({
            url: '/ShoppingCart/Update',
            type: 'POST',
            data: {
                cartData: JSON.stringify(cartList)
            },
            dataType: 'json',
            success: function (response) {
                if (response.status) {
                    cart.loadData();
                    console.log('Update Ok');
                }
            }
        });
    },
    loadData: function () {
        $.ajax({
            url: '/ShoppingCart/GetAll',
            type: 'GET',
            dataType: 'json',
            success: function (res) {
                if (res.status) {
                    var template = $('#tplCart').html();
                    var html = '';
                    var data = res.data;
                    $.each(data, function (i, item) {
                        html += Mustache.render(template,
                            {
                                ProductId: item.ProductId,
                                ProductName: item.Product.Name,
                                Image: item.Product.Image,
                                Price: item.Product.Price,
                                Quantity: item.Quantity,
                                Amount: cart.changeNumToCurrency(item.Quantity * item.Product.Price)
                            });
                    });
                    $('#cartBody').html(html);
                    cart.registerEvents();
                    $('#lblTotalOrder').text(cart.getTotalOrder());
                }
                else {
                    $('.single_grid').html('Không có sản phẩm nào trong giỏ hàng. ');
                }
            }
        });
    },
    changeNumToCurrency: function (num) {
        var newNum = num.toFixed(2).replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,");
        return newNum;
    },
    getTotalOrder: function () {
        var listTextBox = $('.txtQuantity');
        var total = 0;
        $.each(listTextBox, function (i, item) {
            total += parseInt($(item).val()) * parseFloat($(item).data('price'));
        });
        return cart.changeNumToCurrency(total);
    }
}
cart.init();