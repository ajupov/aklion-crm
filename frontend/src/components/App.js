import React from 'react';
import { BrowserRouter, Route, Link } from 'react-router-dom';
import Home from './Home/Index';
import About from './Home/About';
import Topics from './Home/Topics';

const App = () => (
    <BrowserRouter>
        <div>
            <ul>
                <li>
                    <Link to="/">Home</Link>
                </li>
                <li>
                    <Link to="/about">About</Link>
                </li>
                <li>
                    <Link to="/topics">Topics</Link>
                </li>
            </ul>

            <hr />

            <Route exact path="/" component={Home} />
            <Route path="/about" component={About} />
            <Route path="/topics" component={Topics} />
        </div>
    </BrowserRouter>
);

export default App;