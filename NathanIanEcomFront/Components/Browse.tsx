import React, { useState, useEffect } from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import { Product } from '../Models/Product';
import 'regenerator-runtime/runtime'

const Browse = () => {
    const corn: Product = {
        "ProductID": "p#corn", "Description": "Delicious corn", "ImageLink": "https://testlink.com", "Name": "Corn", "Price": "2.99", "Category": "Food"}
    const [products, setProducts] = useState<Product>({
        "ProductID": "p#corn", "Description": "Delicious corn", "ImageLink": "https://testlink.com", "Name": "Corn", "Price": "2.99", "Category": "Food"
    });

    async function clickHandler() {
        const productArray = await getProducts();
        console.log(products);
        setProducts(productArray[0]);
        console.log(products);
    }
    
    return (
        <div className="wrapper">
            <button onClick={clickHandler}>Load Data</button>
            <table className="table">
                <thead>
                    <tr>
                        <th scope="col">Name</th>
                        <th scope="col">Description</th>
                        <th scope="col">Price</th>
                    </tr>
                </thead>
                <tbody>
                    
                </tbody>
            </table>
        </div>
    )

    async function getProducts(): Promise<Product[]> {
        const response = await fetch("https://localhost:5001/api/product/getAllProd",
            {
                method: 'GET',
                headers: {
                    'Content-Type': 'application/json'
                }
            });
        return await response.json();
    }
}



export default Browse;
  