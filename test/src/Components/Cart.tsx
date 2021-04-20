import 'bootstrap/dist/css/bootstrap.min.css';
import { v4 as uuidv4 } from 'uuid';
import { useState, useEffect, useRef } from 'react';
import { getProductsByIDs } from '../Controllers/ProductControllerTest';
import { loadOrder } from '../Controllers/OrderController';
import { loadOrderProducts } from '../Controllers/OrderProductController';
import { Link } from 'react-router-dom';
import Product from '../Models/Product';
import OrderProduct from '../Models/OrderProduct';
import QueryOptions from '../Models/QueryOptions';
import Loading from './Loading';
import Order from '../Models/Order';

const Cart = () => {

    const [products, setProducts] = useState<Partial<OrderProduct>[]>([]);
    const [loading, setLoading] = useState<boolean>(true);
    const [totalCost, setTotalCost] = useState<number>(0);
    const [username, setUsername] = useState<string>("");
    const [firstName, setFirstName] = useState<string>("");
    const [lastName, setLastName] = useState<string>("");
    const [email, setEmail] = useState<string>("");
    const [deliveryAddress, setDeliveryAddress] = useState<string>("");
    const isInit = useRef(true);

    async function loadPage() {
        if (!localStorage.getItem("myEcommerceCart")) {
            var emptyCart: Partial<OrderProduct>[] = [];
            localStorage.setItem("myEcommerceCart", JSON.stringify(emptyCart));
        }

        var retrievedCart: Partial<OrderProduct>[] = JSON.parse(localStorage.getItem("myEcommerceCart") ?? '{[]}');

        var ids: string[] = retrievedCart.map(cartItem => { return cartItem.sk as string });
        var query: QueryOptions = { IDs: ids }
        const productArray: Product[] = await getProductsByIDs(query);

        var orderProductArray: Partial<OrderProduct>[] = productArray.map(product => { return { sk: product.productID, productName: product.name, price: product.price, imageLink: product.imageLink, entityType: "Product", quantity: "-1" } as Partial<OrderProduct> });

        //This is gross. O(n^2) has to have a better solution, and we should look at it, but I'm gonna leave it messy for now. It's not running even slightly slowly at tests of up to ~10 items, and I haven't tried more. However, I don't want to leave it this way. -Ian 04/16/21
        orderProductArray.forEach(product => {
            retrievedCart.forEach(cartItem => {
                if (product.sk == cartItem.sk) {
                    product.quantity = cartItem.quantity;
                }
            })
        })

        setProducts(orderProductArray);
    }

    function removeFromCart(id: string) {
        setProducts(products.filter(product => product.sk != id));
    }

    function incrementQuantity(id: string) {
        var prodArr: Partial<OrderProduct>[] = [...products];
        const index = prodArr.findIndex(p => p.sk == id);
        prodArr[index].quantity = (parseInt(prodArr[index].quantity ?? "0") + 1).toString();
        setProducts(prodArr);
    }

    function decrementQuantity(id: string) {
        var prodArr: Partial<OrderProduct>[] = [...products];
        const index = prodArr.findIndex(p => p.sk == id);
        const targetInt = parseInt(prodArr[index].quantity ?? "2") - 1
        if (targetInt > 0) {
            prodArr[index].quantity = targetInt.toString();
            setProducts(prodArr);
        }
    }

    function generateOrder() {
        setLoading(true);
        
        var orderId: string = "o#" + uuidv4();

        var order: Order = { pk: orderId, sk: orderId, entityType: "Order", username: username, status: "Pending", address: deliveryAddress, createdDate: (new Date().getUTCFullYear() + "-" + (new Date().getUTCMonth() + 1) + "-" + new Date().getUTCDate()) }

        loadOrder(order);
        
        var productsForOrder: OrderProduct[] = [];
        var orderProducts: Partial<OrderProduct>[] = [...products];
        orderProducts.forEach(product => {
            product.pk = orderId;
            product.entityType = "Product";
            var fullProd: OrderProduct = { pk: product.pk ?? "", sk: product.sk ?? "", entityType: product.entityType ?? "", productName: product.productName ?? "", price: product.price ?? "", quantity: product.quantity ?? "", imageLink: product.imageLink ?? "" };
            productsForOrder.push(fullProd);
        });
        var query: QueryOptions = { OrderProducts: productsForOrder }

        loadOrderProducts(query);
        localStorage.removeItem("myEcommerceCart");
    }

    const handleFirstNameChange = (e: React.FormEvent<HTMLInputElement>) => {
        setFirstName(e.currentTarget.value);
    }

    const handleLastNameChange = (e: React.FormEvent<HTMLInputElement>) => {
        setLastName(e.currentTarget.value);
    }

    const handleEmailChange = (e: React.FormEvent<HTMLInputElement>) => {
        setEmail(e.currentTarget.value);
    }

    const handleDeliveryAddressChange = (e: React.FormEvent<HTMLInputElement>) => {
        setDeliveryAddress(e.currentTarget.value);
    }

    const handleUsernameChange = (e: React.FormEvent<HTMLInputElement>) => {
        setUsername(e.currentTarget.value);
    }

    useEffect(() => {
        loadPage();
    }, []);

    useEffect(() => {
        if (isInit.current) {
            isInit.current = false;
        } else {
            var cartStore: Partial<OrderProduct>[] = products.map(product => {
                return { sk: product.sk, quantity: product.quantity } as Partial<OrderProduct>;
            });
            localStorage.setItem("myEcommerceCart", JSON.stringify(cartStore));
            setLoading(false);
            var total: number = 0;
            products.forEach(product => {
                total += parseInt(product.quantity ?? "1") * parseFloat(product.price ?? "-1");
            });
            setTotalCost(total);
        }
    }, [products])

     return (
         <div>
             <div className="row">
                 <div className="col-10">
                     <table className="table">
                         <thead>
                             <tr>
                                 <th scope="col">Image</th>
                                 <th scope="col">Name</th>
                                 <th scope="col">Price</th>
                                 <th scope="col">Quantity</th>
                                 <th scope="col">Remove From Cart</th>
                             </tr>
                         </thead>
                         {!loading &&
                             <tbody>
                                 {products.map(product => (
                                     <tr key={product.sk}>
                                         <td key={'Image' + product.sk}> <img src={product.imageLink} className="img-thumbnail" width="200" height="100" />  </td>
                                         <td key={'Name' + product.sk}>{product.productName}</td>
                                         <td key={'Price' + product.sk}>${product.price}</td>
                                         <td key={'Quantity' + product.sk}>
                                             <div className="input-group mb-3">
                                                 <div className="input-group-prepend">
                                                     <button key={'QuantityMinus' + product.sk} className="btn btn-danger" onClick={() => decrementQuantity(product.sk ?? "")} > -</button>                                       </div>
                                                 <input key={'QuantityField' + product.sk} readOnly type="number" value={product.quantity} />
                                                 <div className="input-group-prepend">
                                                     <button key={'QuantityMinus' + product.sk} className="btn btn-success" onClick={() => incrementQuantity(product.sk ?? "")}>+</button>
                                                 </div>
                                             </div>
                                         </td>
                                         <td key={'Remove' + product.sk}> <button className="btn btn-danger" onClick={() => removeFromCart(product.sk ?? "")}> Remove </button> </td>
                                     </tr>
                                 ))}
                             </tbody>
                         }
                     </table>
                     {(loading && JSON.parse(localStorage.getItem("myEcommerceCart") ?? "[]").length != 0) && <Loading />}
                     {(!loading && JSON.parse(localStorage.getItem("myEcommerceCart") ?? "[]").length == 0) &&
                         <p>Cart is currently empty. Please browse and find something to buy!</p>
                     }
                 </div>
                 <div className="d-flex col-2 bg-light flex-column">
                     <div className="row justify-content-center">
                         <h1>Checkout</h1>
                     </div>
                     <div className="row justify-content-center">
                         <p><strong>Total:</strong> ${totalCost.toFixed(2)}</p>
                     </div>
                     <div className="d-flex flex-wrap justify-content-center">
                         <div className="d-flex flex-column">
                             <small className="d-flex justify-content-center"><strong>Username*</strong></small>
                             <input type="text" value={username} onChange={(e) => handleUsernameChange(e)} />
                         </div>
                         <div className="d-flex flex-column">
                             <small className="d-flex justify-content-center"><strong>First Name*</strong></small>
                             <input type="text" value={firstName} onChange={(e) => handleFirstNameChange(e)} />
                         </div>
                         <div className="d-flex flex-column">
                             <small className="d-flex justify-content-center"><strong>Last Name*</strong></small>
                             <input type="text" value={lastName} onChange={(e) => handleLastNameChange(e)} />
                         </div>
                         <div className="d-flex flex-column">
                             <small className="d-flex justify-content-center"><strong>Email*</strong></small>
                             <input type="text" value={email} onChange={(e) => handleEmailChange(e)} />
                         </div>
                         <div className="d-flex flex-column">
                             <small className="d-flex justify-content-center"><strong>Delivery Address*</strong></small>
                             <input type="text" value={deliveryAddress} onChange={(e) => handleDeliveryAddressChange(e)} />
                         </div>
                         {(username != "" && firstName != "" && lastName != "" && email != "" && deliveryAddress != "" && (JSON.parse(localStorage.getItem("myEcommerceCart") ?? "[]").length != 0)) &&
                             <div className="w-50 mt-2 mb-5">
                                <Link className="btn btn-success w-100" to="/orders" onClick={generateOrder} > Checkout</Link>
                             </div>
                         }
                         {(!(username != "" && firstName != "" && lastName != "" && email != "" && deliveryAddress != "") || (JSON.parse(localStorage.getItem("myEcommerceCart") ?? "[]").length == 0)) &&
                             <div className="w-50 mt-2">
                                <button className="btn btn-danger w-100" disabled={true}>Checkout</button>
                             {(!(username != "" && firstName != "" && lastName != "" && email != "" && deliveryAddress != "") && (JSON.parse(localStorage.getItem("myEcommerceCart") ?? "[]").length != 0)) &&
                                 <p>Please fill out required (*) fields</p>
                             }
                             {(JSON.parse(localStorage.getItem("myEcommerceCart") ?? "[]").length == 0) &&
                                 <p>Please add something to your cart before attempting to check out</p>
                             }
                             </div>
                         }
                     </div>
                 </div>
             </div>
            
            
            </div>
    )
}

export default Cart;