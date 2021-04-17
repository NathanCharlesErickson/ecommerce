import 'bootstrap/dist/css/bootstrap.min.css';
import { useState, useEffect, useRef } from 'react';
import { getProductsByIDs } from '../Controllers/ProductControllerTest';
import Product from '../Models/Product';
import OrderProduct from '../Models/OrderProduct';
import QueryOptions from '../Models/QueryOptions';
import Loading from './Loading';
import Order from '../Models/Order';

const Cart = () => {

    const [products, setProducts] = useState<Partial<OrderProduct>[]>([]);
    const [loading, setLoading] = useState<Boolean>(true);
    const isInit = useRef(true);

    async function loadPage() {
        var retrievedCart: Partial<OrderProduct>[] = JSON.parse(localStorage.getItem("myEcommerceCart") ?? "");

        var ids: string[] = retrievedCart.map(cartItem => { return cartItem.SK as string });
        var query: QueryOptions = { IDs: ids }
        const productArray: Product[] = await getProductsByIDs(query);

        var orderProductArray: Partial<OrderProduct>[] = productArray.map(product => { return { SK: product.productID, ProductName: product.name, Price: product.price, ImageLink: product.imageLink, EntityType: "Product", Quantity: "-1" } as Partial<OrderProduct> });


        //This is gross. O(n^2) has to have a better solution, and we should look at it, but I'm gonna leave it messy for now. It's not running even slightly slowly at tests of up to ~10 items, and I haven't tried more. However, I don't want to leave it this way. -Ian 04/16/21
        orderProductArray.forEach(product => {
            retrievedCart.forEach(cartItem => {
                if (product.SK == cartItem.SK) {
                    product.Quantity = cartItem.Quantity;
                }
            })
        })

        setProducts(orderProductArray);
    }

    function removeFromCart(id: string) {
        setProducts(products.filter(product => product.SK != id));
    }

    function incrementQuantity(id: string) {
        var prodArr: Partial<OrderProduct>[] = [...products];
        const index = prodArr.findIndex(p => p.SK == id);
        prodArr[index].Quantity = (parseInt(prodArr[index].Quantity ?? "0") + 1).toString();
        setProducts(prodArr);
    }

    function decrementQuantity(id: string) {
        var prodArr: Partial<OrderProduct>[] = [...products];
        const index = prodArr.findIndex(p => p.SK == id);
        const targetInt = parseInt(prodArr[index].Quantity ?? "2") - 1
        if (targetInt > 0) {
            prodArr[index].Quantity = targetInt.toString();
            setProducts(prodArr);
        }
    }

    useEffect(() => {
        loadPage();
    }, []);

    useEffect(() => {
        if (isInit.current) {
            isInit.current = false;
        } else {
            var cartStore: Partial<OrderProduct>[] = products.map(product => {
                return { SK: product.SK, Quantity: product.Quantity } as Partial<OrderProduct>;
            });
            localStorage.setItem("myEcommerceCart", JSON.stringify(cartStore));
            setLoading(false);
        }
    }, [products])

     return (
        <div>
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
                             <tr key={product.SK}>
                                 <td key={'Image' + product.SK}> <img src={product.ImageLink} className="img-thumbnail" width="200" height="100" />  </td>
                                 <td key={'Name' + product.SK}>{product.ProductName}</td>
                                 <td key={'Price' + product.SK}>${product.Price}</td>
                                 <td key={'Quantity' + product.SK}>
                                     <div className="input-group mb-3">
                                         <div className="input-group-prepend">
                                             <button key={'QuantityMinus' + product.SK} className="btn btn-danger" onClick={() => decrementQuantity(product.SK ?? "")} > -</button>                                       </div>
                                         <input key={'QuantityField' + product.SK} readOnly type="number" value={product.Quantity} />
                                         <div className="input-group-prepend">
                                             <button key={'QuantityMinus' + product.SK} className="btn btn-success" onClick={() => incrementQuantity(product.SK ?? "")}>+</button>
                                         </div>
                                     </div>
                                 </td>
                                 <td key={'Remove' + product.SK}> <button className="btn btn-danger" onClick={() => removeFromCart(product.SK ?? "")}> Remove </button> </td>
                             </tr>
                         ))}
                     </tbody>
                 }     
             </table>
             {loading && <Loading />}
            
            </div>
    )
}

export default Cart;