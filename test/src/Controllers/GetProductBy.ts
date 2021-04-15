import Product from '../Models/Product';

async function getProductBy(): Promise<Product[]> {
    var data = helper()
    
    let myBody = JSON.stringify(data)

    const response = await fetch("https://localhost:5001/api/product/GetProductBy",
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

export default getProductBy;