import Product from '../Models/Product';

async function getProductById(): Promise<Product[]> {
    console.log(localStorage.getItem("mycart"))
    let test = JSON.stringify(localStorage.getItem("mycart"));
    const response = await fetch("https://78bih5lho6.execute-api.us-west-2.amazonaws.com/Prod/api/product/GetProductById",
        {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.parse(test)

        });
    return await response.json();
}

export default getProductById;