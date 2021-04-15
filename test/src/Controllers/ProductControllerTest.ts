import Product from '../Models/Product';
import config from '../appsettings.json';

var apiPrefix = config.isDev ? config.devURL : config.releaseURL;

export async function getProducts(): Promise<Product[]> {
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

export async function getProductById(): Promise<Product[]> {
    console.log(localStorage.getItem("mycart"))
    let test = JSON.stringify(localStorage.getItem("mycart"));
    const response = await fetch(apiPrefix + "api/product/GetProductById",
        {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.parse(test)

        });
    return await response.json();
}

export async function getProductBy(): Promise<Product[]> {
    var data = helper()

    let myBody = JSON.stringify(data)

    const response = await fetch(apiPrefix + "api/product/GetProductBy",
        {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: myBody

        });
    return await response.json();
}

function helper() {
    var mySearchTerm = {
        searchItem: ""
    }
    mySearchTerm = JSON.parse(localStorage.getItem("searchItem") || '{}')

    var mySearchItem = {
        searchTerm: ""
    }
    mySearchItem = JSON.parse(localStorage.getItem("searchTerm") || '{}')

    var data;
    if (mySearchTerm.searchItem == "Price") {
        data = {
            Price: mySearchItem.searchTerm
        }
    }
    else {
        data = {
            Name: mySearchItem.searchTerm
        }
    }
    return data
}