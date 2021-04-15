import { Link } from 'react-router-dom';



const NavBar = () => {

    function loadItemCooke(item: String) {
        localStorage.removeItem("searchItem")
        var myItem = {
           searchItem : item
        }
        localStorage.setItem("searchItem", JSON.stringify(myItem))
        console.log(localStorage.getItem("searchItem"))
    }

    //Will clean up
    function loadSearchTerm() {
        var item = (document.getElementById("search") as HTMLInputElement)?.value
        var myItem = {
            searchTerm : item
        }
        localStorage.setItem("searchTerm", JSON.stringify(myItem))

    }

    



        return (
            <nav className="navbar navbar-expand-lg navbar-light bg-light">
                <a className="navbar-brand" href="#">Navbar</a>
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
                        <input type="checkbox" className="form-check-input" id="NameCheckBox" onClick={() => loadItemCooke("Name")} />
                            <label className="form-check-label" htmlFor="NameCheck">Name</label>
                    </div>

                    <div className="form-check">
                        <input type="checkbox" className="form-check-input" id="PriceCheckBox" onClick={() => loadItemCooke("Price")} />
                        <label className="form-check-label" htmlFor="PriceCheck">Price</label>
                    </div>

                    <form className="form-inline my-2 my-lg-0">
                        <input className="form-control" id = "search" placeholder="Search" aria-label="Search" />
                        <a className="btn btn-danger" href="/searchresult" onClick={() => loadSearchTerm()}  > Search</a>
                    </form>
                </div>
            </nav>
        )
}

export default NavBar