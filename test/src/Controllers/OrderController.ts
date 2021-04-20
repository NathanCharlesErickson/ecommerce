import config from '../appsettings.json';
import QueryOptions from '../Models/QueryOptions';
import Product from "../Models/Product";
import Order from '../Models/Order';

var apiPrefix = config.isDev ? config.devURL : config.releaseURL;

export async function loadOrder(myOrder: Order) {
    const response = await fetch(apiPrefix + "api/order/loadOrder",
        {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(myOrder)
        });
}

export async function getAllOrderByCustId(myQuery: QueryOptions): Promise<Order[]> {
    const response = await fetch(apiPrefix + "api/order/getAllOrderByCustId",
        {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(myQuery)
        });
    return await response.json();
}