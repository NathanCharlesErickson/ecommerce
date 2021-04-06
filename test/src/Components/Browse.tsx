import Product from '../Models/Product';
import { useState } from 'react';
import getProducts from '../Controllers/ProductController';

const Browse = () => {
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
}

export default Browse;
  