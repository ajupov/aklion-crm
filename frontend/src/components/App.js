import React, { Component } from 'react';
import { HashRouter, Route } from 'react-router-dom'

import Index from './Home/Index';
import Stuff from './Home/Stuff';
import Contact from './Home/Contact';

class App extends Component {
    render() {
        return (
            <HashRouter>
                <div>
                    <Route path="/" component={Index} />
                    <Route path="/stuff" component={Stuff} />
                    <Route path="/contact" component={Contact} />
                </div>
            </HashRouter >
        );
    }
}

export default App;