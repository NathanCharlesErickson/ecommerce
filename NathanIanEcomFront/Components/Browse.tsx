import React, { useState, useEffect } from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';

const Browse = () => {
    useEffect(() => {
        getProducts()
    }, [])

    return (
        <p>Browsing Page</p>
    )
}

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
                })
            }
        })
}

export default Browse;
  