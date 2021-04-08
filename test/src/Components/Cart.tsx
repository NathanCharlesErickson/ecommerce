import 'bootstrap/dist/css/bootstrap.min.css';
import { useState } from 'react';
import getProductById from '../Controllers/GetProductByID';
import Product from '../Models/Product';

const myTest = { helo: "hi" };

function myCookie() {
    var data = {
        ProductID : "h"
    }
    let test = JSON.stringify(localStorage.getItem("mycart"));
    data = JSON.parse(test)
    console.log(data)
}

const Cart = () => {

    const [products, setProducts] = useState<Product[]>([]);

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