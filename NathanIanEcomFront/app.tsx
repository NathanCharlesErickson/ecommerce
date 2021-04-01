import React from 'react';
import ReactDOM from 'react-dom';
import { BrowserRouter as Router, Route } from 'react-router-dom';
import Browse from './Components/Browse';
import Cart from './Components/Cart';
import Account from './Components/Account';
import Orders from './Components/Orders';
import NavBar from './Components/NavBar';

export class App extends React.Component {
    render() {
        return (
            <Router>
                <div>
                    <NavBar />
                    <Route path="/browse" component={Browse} />
                    <Route path="/cart" component={Cart} />
                    <Route path="/account" component={Account} />
                    <Route path="/orders" component={Orders} />
                </div>
            </Router>
        );
    }
}

ReactDOM.render(<App />, document.getElementById('root'));