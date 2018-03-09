import React from 'react';
import ReactDOM from 'react-dom';
import { Form, Icon, Input, Button, Checkbox } from 'antd';
const FormItem = Form.Item;

class NormalLoginForm extends React.Component {
  handleSubmit = e => {
    e.preventDefault();
    this.props.form.validateFields((err, values) => {
      if (!err) {
        console.log('Received values of form: ', values);
      }
    });
  }
  render() {
    const { getFieldDecorator } = this.props.form;

    return (
      <Form onSubmit={this.handleSubmit} className="login-form">
        <FormItem>
          {
            getFieldDecorator('userName', { rules: [{ required: true, message: 'Введите логин' }] })
              (<Input prefix={<Icon type="user" style={{ color: 'rgba(0,0,0,.25)' }} />} placeholder="Логин" />)
          }
        </FormItem>

        <FormItem>
          {
            getFieldDecorator('password', { rules: [{ required: true, message: 'Введите пароль' }] })
              (<Input prefix={<Icon type="lock" style={{ color: 'rgba(0,0,0,.25)' }} />} type="password" placeholder="Пароль" />)
          }
        </FormItem>

        <FormItem>
          {
            getFieldDecorator('remember', { valuePropName: 'checked', initialValue: true })
              (<Checkbox>Запомнить</Checkbox>)
          }
          <a className="login-form-forgot" href="">Забыли пароль?</a>
          <Button type="primary" htmlType="submit" className="login-form-button">Войти</Button>
          Или <a href="">зарегистрироваться!</a>
        </FormItem>
      </Form>
    );
  }
}
const LoginForm = Form.create()(NormalLoginForm)
export default LoginForm;