import React, { useState } from 'react'
import 'bootstrap/dist/css/bootstrap.min.css';
import { Product } from '../Models/Product';

const Account = () => {
    //const [product, setProduct] = React.useState<Number>(5);
    const test = useState(5);
    const test2 = useState<Product>({
        "ProductID": "p#corn", "Description": "Delicious corn", "ImageLink": "https://testlink.com", "Name": "Corn", "Price": "2.99", "Category": "Food"
    });

    const [test3, setTest] = useState<Product>({});

    function clickHandler() {
        console.log(test)
    }

    function clickHandler2() {
        console.log(test2)
    }

    return (
        <div className='wrapper'>
            <button onClick={clickHandler}>Test</button>
            <button onClick={clickHandler2}>Test2</button>
        </div>
    )
}

export default Account;