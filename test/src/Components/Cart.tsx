import { Query } from '@testing-library/dom';
import 'bootstrap/dist/css/bootstrap.min.css';
import { useState, useEffect } from 'react';
import { getProductsByIDs } from '../Controllers/ProductControllerTest';
import Product from '../Models/Product';
import QueryOptions from '../Models/QueryOptions'

const Cart = () => {

    const [products, setProducts] = useState<Product[]>([]);
    const [cart, setCart] = useState<Partial<Product>[]>([]);

    function myCookie() {
        console.log(cart);
    }

    async function loadPage() {
        var retrievedCart: Partial<Product>[] = JSON.parse(localStorage.getItem("myEcommerceCart") ?? "");
        retrievedCart ? setCart(retrievedCart) : setCart([]);

        var ids: string[] = retrievedCart.map(cartItem => { return cartItem.productID as string });
        var query: QueryOptions = { IDs: ids }
        const productArray: Product[] = await getProductsByIDs(query);
        setProducts(productArray);
    }

    function removeFromCart(id: string) {
        setCart(cart.filter(cartItem => cartItem.productID != id));
        setProducts(products.filter(product => product.productID != id));
    }

    useEffect(() => {
        loadPage();
    }, []);

    useEffect(() => {
        localStorage.setItem("myEcommerceCart", JSON.stringify(cart));
    }, [cart])

    return (
        <div>
            <button onClick={myCookie}>Load Cookie</button>
            <table className="table">
                <thead>
                    <tr>
                        <th scope="col">Image</th>
                        <th scope="col">Name</th>
                        <th scope="col">Description</th>
                        <th scope="col">Price</th>
                        <th scope="col">Remove From Cart</th>
                    </tr>
                </thead>
                <tbody>
                    {products.map(product => (
                        <tr key={product.productID}>
                            <td key={'Image' + product.productID}> <img src={product.imageLink} className="img-thumbnail" width="200" height="100" />  </td>
                            <td key={'Name' + product.productID}>{product.name}</td>
                            <td key={'Description' + product.productID}>{product.description}</td>
                            <td key={'Price' + product.productID}>{product.price}</td>
                            <td key={'Remove' + product.productID}> <button className="btn btn-danger" onClick={() => removeFromCart(product.productID)}> Remove </button> </td>
                        </tr>
                    ))}
                </tbody>
            </table>
            
            </div>
    )
}

export default Cart;