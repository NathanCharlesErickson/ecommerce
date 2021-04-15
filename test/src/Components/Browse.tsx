import Product from '../Models/Product';
import { Component, useState, useEffect } from 'react';
import { getProducts } from '../Controllers/ProductControllerTest';
import { Redirect } from 'react-router';

const Browse = () => {
    const [products, setProducts] = useState<Product[]>([]);
    const [cart, setCart] = useState<Partial<Product>[]>([]);

    async function loadProducts() {
        const productArray: Product[] = await getProducts();
        setProducts(productArray);
    }

    function loadCart() {
        var retrievedCartString = localStorage.getItem("myEcommerceCart");
        retrievedCartString ? setCart(JSON.parse(retrievedCartString)) : setCart([]);
    }

    //UseEffect hook is called whenever an involved variable is updated, OR whenever a variable specified in its second parameter is updated. Thus, we can target functions based on certain variables being updated like an onUpdate, or we can pass an empty array to act like an onLoad because it doesn't watch any variables.
    useEffect(() => {
        loadProducts();
        loadCart();
    }, []);

    function storeCookieAddToCart(tempProd: Partial<Product>) {
        if (!cart.some(product => product?.productID == tempProd.productID)) {
            var newCart: Partial<Product>[] = [...cart, tempProd];
            //Appears that the set functions for useState are async, so currently using newCart to set both - however, we should eventually make the localStorage setting dependent upon the cart state variable.
            setCart(newCart);
            localStorage.setItem("myEcommerceCart", JSON.stringify(newCart));
        } else {
            console.warn("Cart already contains selected item: " + tempProd.productID);
        }
    }

    function printProducts() {
        console.log(products);
    }

    function printCart() {
        var retrievedCartString = localStorage.getItem("myEcommerceCart");
        retrievedCartString ? console.log(JSON.parse(retrievedCartString)) : console.warn("No cart stored in cookie.");
    }

    return (
        <div className="wrapper">
            <button onClick={printProducts}>Current Products Value</button>
            <button onClick={printCart}>Current Cart Value</button>
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
                            <td key={'Buy Now' + product.productID}> <a className="btn btn-success" href="/cart" onClick={() => storeCookieAddToCart({ productID: product.productID })} role="button"> Buy Now</a> </td>
                            <td key={'Add to Cart' + product.productID}>  <a className="btn btn-danger" onClick={() => storeCookieAddToCart({ productID: product.productID })} role="button"> Add to Cart</a> </td>

                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    )
}

export default Browse;
  