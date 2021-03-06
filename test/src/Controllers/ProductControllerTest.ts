import Product from '../Models/Product';
import QueryOptions from '../Models/QueryOptions';
import config from '../appsettings.json';
import PagedResult from '../Models/PagedResult';

var apiPrefix = config.isDev ? config.devURL : config.releaseURL;

export async function getProducts(): Promise<Product[]> {
    const response = await fetch(apiPrefix + "api/product/getAllProd",
        {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            }
        });
    return await response.json();
}

export async function getProductsPaged(myInput: QueryOptions): Promise<PagedResult> {
    const response = await fetch(apiPrefix + "api/product/getAllProdPaged",
        {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(myInput)
        });
    return await response.json();
}

export async function getProductsByIDs(myInput: QueryOptions): Promise<Product[]> {
    const response = await fetch(apiPrefix + "api/product/getProductsByIDs",
        {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(myInput)
        });
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

export async function getProductBy(myInput: QueryOptions): Promise<PagedResult> {
    var data = helper()

    console.log("Controller: " + myInput)

    let myBody = JSON.stringify(myInput)

    console.log(myBody)

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