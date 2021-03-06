import OrderProduct from "./OrderProduct";

export default interface QueryOptions {
    PK?: string;
    SK?: string;
    ProductID?: string;
    Username?: string;
    Description?: string;
    Category?: string;
    Price?: string;
    Name?: string;
    IDs?: string[];
    PaginationToken?: string | null;
    OrderProducts?: OrderProduct[];
}