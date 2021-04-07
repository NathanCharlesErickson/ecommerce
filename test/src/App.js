import { BrowserRouter as Router, Route } from 'react-router-dom';
import NavBar from './Components/NavBar'
import Browse from './Components/Browse'
import Cart from './Components/Cart'
import Account from './Components/Account'
import Orders from './Components/Orders'
import React, { MouseEvent } from 'react';




function App() {
  return (
      <div className="App">
          <Router>
              <NavBar />
              <Route path="/browse" component={Browse} />
              <Route path="/cart" component={Cart} />
              <Route path="/account" component={Account} />
              <Route path="/orders" component={Orders} />
          </Router>
    </div>
  );
}

export default App;
