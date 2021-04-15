import 'bootstrap/dist/css/bootstrap.min.css';
import { useState, useEffect } from 'react';
import { getProductById } from '../Controllers/ProductControllerTest';
import Product from '../Models/Product';

const Cart = () => {

    const [products, setProducts] = useState<Product[]>([]);
    const [cart, setCart] = useState<Partial<Product>[]>([]);

    function myCookie() {
        console.log(cart);
    }

    function loadCart() {
        var retrievedCartString = localStorage.getItem("myEcommerceCart");
        retrievedCartString ? setCart(JSON.parse(retrievedCartString)) : setCart([]);
    }

    useEffect(() => {
        loadCart();
    }, []);

    async function clickHandler() {
        const productArray: Product[] = await getProductById();
        setProducts(productArray);
        console.log(products);
    }

    return (
        <div>
            <button onClick={myCookie}>Load Cookie</button>
            <button onClick={clickHandler}>Load Data</button>

            
            </div>
    )
}

export default Cart;