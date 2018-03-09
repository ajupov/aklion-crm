import React from 'react';
import ReactDOM from 'react-dom';
import moment from 'moment';
import { DatePicker, version } from 'antd';
import 'antd/dist/antd.css';

const App = () => (
        <div style={{ margin: 24 }}>
                <p style={{ marginBottom: 24 }}>
                        Current antd version: {version} <br />
                        You can change antd version on the left panel (Dependencies section).
    </p>
                <DatePicker defaultValue={moment()} />
        </div>
);

export default App;