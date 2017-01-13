var ProductCategory=React.createClass({
    render:function(){
        return(
        <li className="item1">
            <a href="#">{this.props.children}</a>
        </li>
        );
    }
});

var MainPage=React.createClass({
    render:function(){
        return(
         <div className="shoes-grid">
        <div className="wrap-in">
            <div className="wmuSlider example1 slide-grid" >
                <div className="wmuSliderWrapper">
                    <article >
                        <div className="banner-matter">
                            <div className="col-md-5 banner-bag">
                                <img className="img-responsive " src="/Assets/client/images/bag.jpg" alt="Slide1" />
                            </div>
                            <div className="col-md-7 banner-off">
                                <h2>FLAT 50% 0FF</h2>
                                <label>FOR ALL PURCHASE <b>VALUE</b></label>
                                <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et </p>
                                <span className="on-get">GET NOW</span>
                            </div>

                            <div className="clearfix"> </div>
                        </div>

                    </article>
                    <article >
                        <div className="banner-matter">
                            <div className="col-md-5 banner-bag">
                                <img className="img-responsive " src="/Assets/client/images/bag1.jpg" alt="Slide2" />
                            </div>
                            <div className="col-md-7 banner-off">
                                <h2>FLAT 50% 0FF</h2>
                                <label>FOR ALL PURCHASE <b>VALUE</b></label>
                                <p>
                                    Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et
                                    
                                </p>
                            </div>

                            <div className="clearfix"> </div>
                        </div>

                    </article>

                </div>

                <ul className="wmuSliderPagination">
                    <li><a href="#" className="">0</a></li>
                    <li><a href="#" className="">1</a></li>
                </ul>
                <a className="wmuSliderPrev">Previous</a><a className="wmuSliderNext">Next</a><ul className="wmuSliderPagination"><li><a href="#" className="">0</a></li><li><a href="#" className="wmuActive">1</a></li></ul>
            </div>
        </div>
        <div className="shoes-grid-left">
            <a href="single.html">
                <div className="col-md-6 con-sed-grid">
                    <div className="elit-grid">
                        <h4>consectetur  elit</h4>
                        <label>FOR ALL PURCHASE VALUE</label>
                        <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit, </p>
                        <span className="on-get">GET NOW</span>
                    </div>
                    <img className="img-responsive shoe-left" src="/Assets/client/images/sh.jpg" alt=" " />
                    <div className="clearfix"></div>
                </div>
            </a>
            <a href="single.html">
                <div className="col-md-6 con-sed-grid sed-left-top">
                    <div className=" elit-grid">
                        <h4>consectetur  elit</h4>
                        <label>FOR ALL PURCHASE VALUE</label>
                        <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit, </p>
                        <span className="on-get">GET NOW</span>
                    </div>
                    <img className="img-responsive shoe-left" src="/Assets/client/images/wa.jpg" alt=" "/>
                    <div className="clearfix"> </div>
                </div>
            </a>
        </div>
        <div className="products">
            <h5 className="latest-product">Sản Phẩm Mới Nhất</h5>
            <a className="view-all" href="san-pham.html">Xem Tất Cả<span> </span></a>
        </div>
        <div className="product-left">
            <div className="col-md-4 chain-grid grid-top-chain">
                <a href="/san-pham-1.p-1.html"><img className="img-responsive chain" src="/Assets/client/images/ch.jpg" alt="San pham 1" /></a>
                <span className="star"> </span>
                <div className="grid-chain-bottom">
                    <h6><a href="/san-pham-1.p-1.html">San pham 1</a></h6>
                    <div className="star-price">
                        <div className="dolor-grid">
                            <span className="actual">300</span>
                            <span className="reducedfrom">400,000</span>
                           
                        </div>
                        <a className="now-get get-cart btnAddToCart" href="#" data-id="1">Mua hàng</a>
                        <div className="clearfix"> </div>
                    </div>
                </div>
            </div>
            <div className="col-md-4 chain-grid grid-top-chain">
                <a href="/san-pham-2.p-2.html"><img className="img-responsive chain" src="/Assets/client/images/ba.jpg" alt="San pham 2" /></a>
                <span className="star"> </span>
                <div className="grid-chain-bottom">
                    <h6><a href="/san-pham-2.p-2.html">San pham 2</a></h6>
                    <div className="star-price">
                        <div className="dolor-grid">
                            <span className="actual">300</span>
                            <span className="reducedfrom">500,000</span>
                           
                        </div>
                        <a className="now-get get-cart btnAddToCart" href="#" data-id="2">Mua hàng</a>
                        <div className="clearfix"> </div>
                    </div>
                </div>
            </div>
            <div className="col-md-4 chain-grid grid-top-chain">
                <a href="/san-pham-3.p-3.html"><img className="img-responsive chain" src="/Assets/client/images/bo.jpg" alt="San pham 3" /></a>
                <span className="star"> </span>
                <div className="grid-chain-bottom">
                    <h6><a href="/san-pham-3.p-3.html">San pham 3</a></h6>
                    <div className="star-price">
                        <div className="dolor-grid">
                            <span className="actual">300</span>
                            <span className="reducedfrom">400</span>
                           
                        </div>
                        <a className="now-get get-cart btnAddToCart" href="#" data-id="3">Mua hàng</a>
                        <div className="clearfix"> </div>
                    </div>
                </div>
            </div>
            <div className="clearfix"> </div>
        </div>
        <div className="products">
            <h5 className="latest-product">Sản Phẩm Bán Chạy</h5>
            <a className="view-all" href="san-pham-ban-chay.html">Xem Tất Cả<span> </span></a>
        </div>
        <div className="product-left">
            <div className="col-md-4 chain-grid grid-top-chain">
                <a href="/san-pham-4.p-6.html"><img className="img-responsive chain" src="/Assets/client/images/bott.jpg" alt="San pham 4" /></a>
                <span className="star"> </span>
                <div className="grid-chain-bottom">
                    <h6><a href="/san-pham-4.p-6.html">San pham 4</a></h6>
                    <div className="star-price">
                        <div className="dolor-grid">
                            <span className="actual">300</span>
                            <span className="reducedfrom">400</span>
                           
                        </div>
                        <a className="now-get get-cart btnAddToCart" href="#" data-id="6">Mua hàng</a>
                        <div className="clearfix"> </div>
                    </div>
                </div>
            </div>
            <div className="col-md-4 chain-grid grid-top-chain">
                <a href="/san-pham-5.p-5.html"><img className="img-responsive chain" src="/Assets/client/images/bottle.jpg" alt="San pham 5" /></a>
                <span className="star"> </span>
                <div className="grid-chain-bottom">
                    <h6><a href="/san-pham-5.p-5.html">San pham 5</a></h6>
                    <div className="star-price">
                        <div className="dolor-grid">
                            <span className="actual">300</span>
                            <span className="reducedfrom">400</span>
                          
                        </div>
                        <a className="now-get get-cart btnAddToCart" href="#" data-id="5">Mua hàng</a>
                        <div className="clearfix"> </div>
                    </div>
                </div>
            </div>
            <div className="col-md-4 chain-grid grid-top-chain">
                <a href="/san-pham-6.p-4.html"><img className="img-responsive chain" src="/Assets/client/images/baa.jpg" alt="San pham 6" /></a>
                <span className="star"> </span>
                <div className="grid-chain-bottom">
                    <h6><a href="/san-pham-6.p-4.html">San pham 6</a></h6>
                    <div className="star-price">
                        <div className="dolor-grid">
                            <span className="actual">300</span>
                            <span className="reducedfrom">400</span>
                           
                        </div>
                        <a className="now-get get-cart btnAddToCart" href="#" data-id="4">Mua hàng</a>
                        <div className="clearfix"> </div>
                    </div>
                </div>
            </div>
            <div className="clearfix"> </div>
        </div>
    </div>

        );
                                }
                                });


var ProductCategoryBox=React.createClass({
    getInitialState() {
        return {categories: []};
                                },
                                    componentDidMount: function() {
        var that=this;
        $.get("/api/productcategory/getallparents", function (data) {
            that.setState({
                                    categories: data
                                });
                                });
                                },
                                    render:function(){
        return(
            <div className="top-nav rsidebar span_1_of_left">
                <h3 className="cate">CATEGORIES</h3>
                <ul className="menu">
                    {
                        this.state.categories.map(function(cate){
                            return <ProductCategory>{cate.name}</ProductCategory>
                                })
                                }
                </ul>
            </div>
        );
                                }
    
                                });

var FeatureProduct=React.createClass({
    render:function(){
        return(
        <div className='chain-grid menu-chain'>
            <a href="single.html"><img className="img-responsive chain" src="/Assets/client/images/wat.jpg" alt="" /></a>
            <div className="grid-chain-bottom chain-watch">
                <span className="actual dolor-left-grid">300$</span>
                <span className="reducedfrom">500$</span>
                <h6><a href="single.html">Lorem ipsum dolor</a></h6>
            </div>
        </div>  
        );
    }
});


ReactDOM.render(
    <div>
    <MainPage />
    <div className='sub-cate'>
        <ProductCategoryBox />
        <FeatureProduct />
        <a className="view-all all-product" href="product.html">VIEW ALL PRODUCTS<span></span></a>  
    </div>
        </div>
    ,
    document.getElementById('content')
);