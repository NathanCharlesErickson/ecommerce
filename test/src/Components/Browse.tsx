import Product from '../Models/Product';
import { Component, useState } from 'react';
import getProducts from '../Controllers/ProductController';
import { Redirect } from 'react-router';

class productJSON {

    ProductID: String;
    constructor(productID: String) {
        this.ProductID = productID;
    }
}


const Browse = () => {
    const [products, setProducts] = useState<Product[]>([]);

    async function clickHandler() {
        const productArray: Product[] = await getProducts();
        setProducts(productArray);
        console.log(products);
    }

    function storeCookieBuyNow(productId: String) {
        var holder = new productJSON(productId);
        localStorage.setItem("mycart", JSON.stringify(holder))
        console.log(localStorage.getItem("mycart"));
        return "Cart";
    }

    function storeCookieAddToCart(productId: String) {
        var holder = new productJSON(productId);
        localStorage.setItem("mycart", JSON.stringify(holder))
        console.log(localStorage.getItem("mycart"));
        
        
   
    }

   
    return (
        <div className="wrapper">
            <button onClick={clickHandler}>Load Data</button>
            <table className="table">
                <thead>
                    <tr>
                        <th scope="col">Image</th>
                        <th scope="col">Name</th>
                        <th scope="col">Description</th>
                        <th scope="col">Price</th>
                        <th scope="col">Buy Now</th>
                        <th scope="col">Add to Cart</th>



                    </tr>
                </thead>
                <tbody>
                    {products.map(product => (
                        <tr key={product.productID}>
                            <td key={'Image' + product.productID}> <img src={product.imageLink} className="img-thumbnail" width="200" height="100"/>  </td>
                            <td key={'Name' + product.productID}>{product.name}</td>
                            <td key={'Description' + product.productID}>{product.description}</td>
                            <td key={'Price' + product.productID}>{product.price}</td>
                            <td key={'Buy Now' + product.productID}> <a className="btn btn-success" href="/cart" onClick={() => storeCookieAddToCart(product.productID)} role="button"> Buy Now</a> </td>
                            <td key={'Add to Cart' + product.productID}>  <a className="btn btn-danger" onClick={() => storeCookieAddToCart(product.productID)} role="button"> Add to Cart</a> </td>

                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    )
}

export default Browse;
  