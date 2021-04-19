import Order from "./Order";
import OrderProduct from "./OrderProduct";
import Product from "./Product";

export default interface PagedResult {
    productPage?: Product[];
    orderProductPage?: Array<OrderProduct>;
    orderPage?: Array<Order>;
    paginationToken: string | null;
}