import config from '../appsettings.json';
import QueryOptions from '../Models/QueryOptions';

var apiPrefix = config.isDev ? config.devURL : config.releaseURL;

export async function loadOrderProducts(queryOptions: QueryOptions) {
    const response = await fetch(apiPrefix + "api/orderProduct/loadOrderProducts",
        {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(queryOptions)
        });
}