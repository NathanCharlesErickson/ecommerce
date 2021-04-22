import Product from '../Models/Product';
import OrderProduct from '../Models/OrderProduct';
import PagedResult from '../Models/PagedResult';
import QueryOptions from '../Models/QueryOptions';
import { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import { getProductBy, getProducts, getProductsPaged } from '../Controllers/ProductControllerTest';
import Loading from './Loading';


const Browse = () => {
    const [products, setProducts] = useState<Product[]>([]);
    const [cart, setCart] = useState<Partial<OrderProduct>[]>([]);
    const [pages, setPages] = useState<(string | null)[]>([null]);
    const [currentPage, setCurrentPage] = useState<number>(-1)
    const [forward, setForward] = useState<boolean>(false);
    const [backward, setbackward] = useState<boolean>(false);


    async function loadPage(nextPage: boolean = true) {
        if (localStorage.getItem("searchItem") != null && localStorage.getItem("searchTerm") != null) {

            //Super confusing. TODO: better names
            var mySearchTerm = { //checkboxes
                searchItem: ""
            }
            mySearchTerm = JSON.parse(localStorage.getItem("searchItem") || '{}')

            var mySearchItem = { //What is put in textbox
                searchTerm: ""
            }
            mySearchItem = JSON.parse(localStorage.getItem("searchTerm") || '{}')

           
                console.log(mySearchItem.searchTerm)
                if (mySearchTerm.searchItem == "Price") {
                    var query: QueryOptions = { PaginationToken: null, Price: mySearchItem.searchTerm }
                    const pagedProducts: PagedResult = await getProductBy(query);
                    const productArray: Product[] = pagedProducts.productPage ?? [];
                    setProducts(productArray)
                } else {
                    var query: QueryOptions = { PaginationToken: null, Name: mySearchItem.searchTerm }
                    const pagedProducts: PagedResult = await getProductBy(query);
                    const productArray: Product[] = pagedProducts.productPage ?? [];
                    setProducts(productArray)
            }

            if (products.length < 20) {
                setForward(true)
                setbackward(true)
                localStorage.removeItem("searchTerm")
                localStorage.removeItem("searchItem")
            }
            //TODO: Paging for Search
            
            
        }
        else if (nextPage) {
            var query: QueryOptions = { PaginationToken: pages[currentPage + 1] }
            const pagedProducts: PagedResult = await getProductsPaged(query);
            const productArray: Product[] = pagedProducts.productPage ?? [];
            if (productArray.length == 0) {
                alert("This is the last page")
                setForward(true)
            }
            else {
                setbackward(false)
                setProducts(productArray);
                if (!pages.includes(pagedProducts.paginationToken)) {
                    setPages([...pages, pagedProducts.paginationToken]);
                }
                setCurrentPage(currentPage + 1);
            }


        } else {
            if (currentPage < 1) {
                alert("Cannot go into negative pages!")
                setbackward(true)
            }
            else {
                setForward(false)
                var query: QueryOptions = { PaginationToken: pages[currentPage - 1] }
                const pagedProducts: PagedResult = await getProductsPaged(query);
                const productArray: Product[] = pagedProducts.productPage ?? [];
                setProducts(productArray);
                setCurrentPage(currentPage - 1);
            }
        }
    }

    


    function loadCart() {
        var retrievedCartString = localStorage.getItem("myEcommerceCart");
        retrievedCartString ? setCart(JSON.parse(retrievedCartString)) : setCart([]);
    }

    //UseEffect hook is called whenever an involved variable is updated, OR whenever a variable specified in its second parameter is updated. Thus, we can target functions based on certain variables being updated like an onUpdate, or we can pass an empty array to act like an onLoad because it doesn't watch any variables.
    useEffect(() => {
        loadPage();
        loadCart();
    }, []);

    function storeCookieAddToCart(tempProd: Partial<OrderProduct>) {
        if (!cart.some(product => product?.sk == tempProd.sk)) {
            var newCart: Partial<OrderProduct>[] = [...cart, tempProd];
            //Appears that the set functions for useState are async, so currently using newCart to set both - however, we should eventually make the localStorage setting dependent upon the cart state variable.
            setCart(newCart);
            localStorage.setItem("myEcommerceCart", JSON.stringify(newCart));
        } else {
            console.warn("Cart already contains selected item: " + tempProd.sk);
        }
    }

    return (
        <div className="wrapper">
            <button className="btn btn-danger" disabled={backward} onClick={() => loadPage(false)}>Prev</button>
            <button className="btn btn-success" disabled={forward} onClick={() => loadPage()}>Next</button>
            <table className="table">
                <thead>
                    <tr>
                        <th scope="col">Image</th>
                        <th scope="col">Name</th>
                        <th scope="col">Description</th>
                        <th scope="col">Price</th>
                        <th scope="col">Add to Cart</th>
                    </tr>
                </thead>
                {products.length > 0 &&
                    <tbody>
                        {products.map(product => (
                            <tr key={product.productID}>
                                <td key={'Image' + product.productID}> <img src={product.imageLink} className="img-thumbnail" width="200" height="100" />  </td>
                                <td key={'Name' + product.productID}>{product.name}</td>
                                <td key={'Description' + product.productID}>{product.description}</td>
                                <td key={'Price' + product.productID}>${product.price}</td>
                                <td key={'Add to Cart' + product.productID}>  <button className="btn btn-danger" onClick={() => storeCookieAddToCart({ sk: product.productID, quantity: "1" })} role="button"> Add to Cart</button> </td>

                            </tr>
                        ))}
                    </tbody>
                }
            </table>
            {!(products.length > 0) && <Loading />}
        </div>
)}


export default Browse;
  