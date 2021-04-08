import Product from '../Models/Product';
import config from '../appsettings.json';

var apiPrefix = config.isDev ? config.devURL : config.releaseURL;

async function getProducts(): Promise<Product[]> {
    const response = await fetch(apiPrefix + "api/product/getAllProd",
        {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            }
        });
    console.log(apiPrefix);
    return await response.json();
}


export default getProducts;

