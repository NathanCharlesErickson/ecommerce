import React, { useState, useEffect } from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';

const Browse = () => {
    const [products, setProducts] = useState([]);

    useEffect(() => {
        getProducts();
        console.log(products);
    }, []);
    
    return (
        <div className="wrapper">
            <p>Browsing Page</p>
            <table class="table">
                <thead>
                    <tr>
                        <th scope="col">Name</th>
                        <th scope="col">Description</th>
                        <th scope="col">Price</th>
                    </tr>
                </thead>
                <tbody>
                    {products.map(product =>
                        <tr key={product.productID}>
                            <td>{product.name}</td>
                            <td>{product.description}</td>
                            <td>${product.price}</td>
                        </tr>)
                    }
                </tbody>
            </table>
        </div>
    )

    function getProducts() {
        fetch("https://localhost:5001/api/product/getAllProd",
            {
                method: 'GET',
                headers: {
                    'Content-Type': 'application/json'
                }
            })
            .then(response => {
                if (!response.ok) {
                    console.error('Failed to fetch. Code: ' + response.status + ', reason: ' + response.statusText);
                } else {
                    console.log('Received response 200 OK');
                    response.json().then(data => {
                        console.log('received Serverless APP response...', data);
                        setProducts(data)
                    })
                }
            })
    }
}



export default Browse;
  