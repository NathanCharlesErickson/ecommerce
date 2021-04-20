import { useEffect, useState } from "react";
import Order from '../Models/Order';
import QueryOptions from '../Models/QueryOptions';
import OrderProduct from '../Models/OrderProduct';
import { getAllOrderByCustId } from '../Controllers/OrderController';

const Orders = () => {

    const [orders, setOrders] = useState<Order[]>([]);
    const [username, setUsername] = useState<string>("");

    const handleUsernameChange = (e: React.FormEvent<HTMLInputElement>) => {
        setUsername(e.currentTarget.value);
    }

    async function loadOrders(username: string) {
        var query: QueryOptions = { Username: username };
        var orderResponse: Order[] = await getAllOrderByCustId(query);
        setOrders(orderResponse);
    }

    return (
        <div className="wrapper">
            <input type="text" value={username} onChange={(e) => handleUsernameChange(e)} />
            <button className="btn btn-success" onClick={() => loadOrders(username)} > Load Orders</ button>
            <table className="table">
                <thead>
                    <tr>
                        <th scope="col">Order Date</th>
                        <th scope="col">Delivery Address</th>
                        <th scope="col">Status</th>
                    </tr>
                </thead>
                <tbody>
                    {orders.map(order => (
                        <tr key={order.sk}>
                            <td key={'Date' + order.sk}>{order.createdDate}</td>
                            <td key={'Address' + order.sk}>{order.address}</td>
                            <td key={'Status' + order.sk}>{order.status}</td>
                        </tr>
                    ))}
                </tbody>
            </table>
            {!(orders.length > 0) &&
                <p>Please enter your username to find a list of your orders</p>
            }
                
        </div>
    )
}

export default Orders;