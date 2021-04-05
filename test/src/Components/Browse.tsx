import Product from '../Models/Product';
import { useState } from 'react';

const Browse = () => {
    const corn: Product = {
        "productID": "p#corn", "description": "Delicious corn", "imageLink": "https://testlink.com", "name": "Corn", "price": "2.99", "category": "Food"
    }
    const corn2: Product = {
        "productID": "p#corn2", "description": "Delicious corn2", "imageLink": "https://testlink.com", "name": "Corn", "price": "3.99", "category": "Food"
    }
    const [products, setProducts] = useState<Product[]>([]);

    async function clickHandler() {
        const productArray: Product[] = await getProducts();
        setProducts(productArray);
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
                    {products.map(product => (
                        <tr key={product.productID}>
                            <td key={'Name' + product.productID}>{product.name}</td>
                            <td key={'Description' + product.productID}>{product.description}</td>
                            <td key={'Price' + product.productID}>{product.price}</td>
                        </tr>
                    ))}
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
  