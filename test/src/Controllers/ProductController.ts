import Product from '../Models/Product';

async function getProducts(): Promise<Product[]> {
    const response = await fetch("https://78bih5lho6.execute-api.us-west-2.amazonaws.com/Prod/api/product/getAllProd",
        {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            }
        });
    return await response.json();
}

export default getProducts;