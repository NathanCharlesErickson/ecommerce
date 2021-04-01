declare var require: any

var React = require('react');
var ReactDOM = require('react-dom');
var Router = require('react-router-dom')
var Home = require('./compoents/Home.tsx')



export class Hello extends React.Component {
    render() {
        return (

            <h1> hello world </h1>
            );
    }
}

ReactDOM.render(<Hello />, document.getElementById('root'));