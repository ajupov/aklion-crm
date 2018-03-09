import React from 'react';
import ReactDOM from 'react-dom';
import { LocaleProvider, DatePicker, message } from 'antd';
// The default locale is en-US, but we can change it to other language
import ruRU from 'antd/lib/locale-provider/ru_RU';
import moment from 'moment';
import 'moment/locale/ru';
import LoginForm from './components/LoginForm';
import style from './index.css';

moment.locale('fr');

class App extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      date: '',
    };
  }
  handleChange(date) {
    message.info('Selected Date: ' + date.toString());
    this.setState({ date });
  }
  render() {
    return (
      <div>
        <LocaleProvider locale={ruRU}>
        <div style={{ width: 400, margin: '100px auto' }}>
          <DatePicker onChange={value => this.handleChange(value)} locale={ruRU}/>
          <div style={{ marginTop: 20 }}>Date: {this.state.date.toString()}</div>

        </div>
      </LocaleProvider>
      <LoginForm />
      </div>
    );
  }
}

ReactDOM.render(<App />, document.getElementById('root'));