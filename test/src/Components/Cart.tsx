import 'bootstrap/dist/css/bootstrap.min.css';

const myTest = { helo: "hi" };

function myCookie() {
    console.log( localStorage.getItem("mycart"));

}

const Cart = () => {


    return (
        <div>
            <button onClick={myCookie}>Load Cookie</button>
            <p>Cart Page</p>
            </div>
    )
}

export default Cart;