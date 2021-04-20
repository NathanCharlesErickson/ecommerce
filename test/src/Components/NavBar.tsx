import { useState } from 'react';
import { Link } from 'react-router-dom';



const NavBar = () => {

    const [checkPrice, setCheckPrice] = useState<boolean>(false);
    const [checkName, setCheckName] = useState<boolean>(false);

    function loadItemCooke(item: String) {
        if (new String(item).valueOf() == new String("Name").valueOf()) {
            console.log("name")
            setCheckPrice(false)
            setCheckName(true)
        } else {
            setCheckPrice(true)
            setCheckName(false)
        }
   
        localStorage.removeItem("searchItem")
        var myItem = {
           searchItem : item
        }
        localStorage.setItem("searchItem", JSON.stringify(myItem))
        console.log(localStorage.getItem("searchItem"))
    }

    //TODO: clean up
    function loadSearchTerm() {
        var item = (document.getElementById("search") as HTMLInputElement)?.value
        if (item === "") { alert("Search field is empty") }
        else if (localStorage.getItem("searchItem") === null) { alert("search category is empty")}
        else {
            var myItem = {
                searchTerm: item
            }
            localStorage.setItem("searchTerm", JSON.stringify(myItem))
            window.location.replace('/browse');
        }
        

    }

        return (
            <nav className="navbar navbar-expand-lg navbar-light bg-light">
                <a className="navbar-brand" href="#">The Better Amazon</a>
                <button className="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
                    <span className="navbar-toggler-icon"></span>
                </button>

                <div className="collapse navbar-collapse" id="navbarSupportedContent">
                    <ul className="navbar-nav mr-auto">
                        <li className="nav-item">
                            <Link className="nav-link" to="/browse">Browse</Link>
                        </li>
                        <li className="nav-item">
                            <Link className="nav-link" to="/cart">Cart</Link>
                        </li>
                        <li className="nav-item">
                            <Link className="nav-link" to="/account">Account</Link>
                        </li>
                        <li className="nav-item">
                            <Link className="nav-link" to="/orders">Orders</Link>
                        </li>
                    </ul>
                    <div className="form-check">
                        <input type="checkbox" className="form-check-input" id="NameCheckBox" checked={checkName} onClick={() => loadItemCooke("Name")} />
                            <label className="form-check-label" htmlFor="NameCheck">Name</label>
                    </div>

                    <div className="form-check">
                        <input type="checkbox" className="form-check-input" id="PriceCheckBox" checked={checkPrice} onClick={() => loadItemCooke("Price")} />
                        <label className="form-check-label" htmlFor="PriceCheck">Price</label>
                    </div>

                    <form className="form-inline my-2 my-lg-0" onSubmit={() => loadSearchTerm()}>
                        <input className="form-control" disabled={true} id="search" placeholder="Search Coming Soon..." aria-label="Search" />
                        <button className="btn btn-danger" disabled={true} onClick={() => loadSearchTerm()} > Search</button>
                    </form>
                </div>
            </nav>
        )
}

export default NavBar