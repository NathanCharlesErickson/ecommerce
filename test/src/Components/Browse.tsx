import Product from '../Models/Product';
import OrderProduct from '../Models/OrderProduct';
import { useState, useEffect, useRef } from 'react';
import { getProducts } from '../Controllers/ProductControllerTest';
import Loading from './Loading';


const Browse = () => {
    const [products, setProducts] = useState<Product[]>([]);
    const [cart, setCart] = useState<Partial<OrderProduct>[]>([]);

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

    function storeCookieAddToCart(tempProd: Partial<OrderProduct>) {
        if (!cart.some(product => product?.SK == tempProd.SK)) {
            var newCart: Partial<OrderProduct>[] = [...cart, tempProd];
            //Appears that the set functions for useState are async, so currently using newCart to set both - however, we should eventually make the localStorage setting dependent upon the cart state variable.
            setCart(newCart);
            localStorage.setItem("myEcommerceCart", JSON.stringify(newCart));
        } else {
            console.warn("Cart already contains selected item: " + tempProd.SK);
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
                {products.length > 0 &&
                    <tbody>
                        {products.map(product => (
                            <tr key={product.productID}>
                                <td key={'Image' + product.productID}> <img src={product.imageLink} className="img-thumbnail" width="200" height="100" />  </td>
                                <td key={'Name' + product.productID}>{product.name}</td>
                                <td key={'Description' + product.productID}>{product.description}</td>
                                <td key={'Price' + product.productID}>${product.price}</td>
                                <td key={'Buy Now' + product.productID}> <a className="btn btn-success" href="/cart" onClick={() => storeCookieAddToCart({ SK: product.productID, Quantity: "1" })} role="button"> Buy Now</a> </td>
                                <td key={'Add to Cart' + product.productID}>  <a className="btn btn-danger" onClick={() => storeCookieAddToCart({ SK: product.productID, Quantity: "1" })} role="button"> Add to Cart</a> </td>

                            </tr>
                        ))}
                    </tbody>
                }
            </table>
            {!(products.length > 0) && <Loading />}
        </div>
    )
}

export default Browse;
  