import React from 'react';
import { Form, Button, ButtonGroup, Row, Col } from 'react-bootstrap';

const initialState = {
  user: {
    username: '',
    password: ''
  },
  validated: false
};

class LoginForm extends React.Component {
  constructor(props) {
    super(props);

    this.state = initialState;

    this.handleChange = this.handleChange.bind(this);
    this.handleSubmit = this.handleSubmit.bind(this);
    this.handleReset = this.handleReset.bind(this);
  }

  handleChange(event) {
    const { name, value } = event.target;

    this.setState(prevState => ({
      user: {
        ...prevState.user,
        [name]: value
      }
    }));
  }

  handleReset(event) {
    event.preventDefault();
    this.setState(initialState);
    this.props.resetForm();
  }

  handleSubmit(event) {
    event.preventDefault();
    const isValid = event.target.checkValidity();

    if (isValid) {
      const { user } = this.state;
      this.props.submitForm(user.username, user.password);
    }

    this.setState({
      validated: !isValid
    });
  }

  render() {
    const { user, validated } = this.state;

    return (
      <Form noValidate validated={validated} onSubmit={this.handleSubmit} onReset={this.handleReset}>
        <Form.Group as={Row} controlId='formLogin_Username'>
          <Col md={3}>
            <Form.Label className='loginFormLabel'>Username</Form.Label>
          </Col>
          <Col md={9}>
            <Form.Control required type='text' name='username' value={user.username} placeholder='username' onChange={this.handleChange} />
            <Form.Control.Feedback type='invalid'>Field is required!</Form.Control.Feedback>
          </Col>
        </Form.Group>

        <Form.Group as={Row} controlId='formLogin_Password'>
          <Col md={3}>
            <Form.Label className='loginFormLabel'>Password</Form.Label>
          </Col>
          <Col md={9}>
            <Form.Control required type='password' name='password' value={user.password} placeholder='password' onChange={this.handleChange} />
            <Form.Control.Feedback type='invalid'>Field is required!</Form.Control.Feedback>
          </Col>
        </Form.Group>

        <ButtonGroup aria-label='login-form-actions'>
          <Button type='submit' variant='success'>Войти</Button>
          <Button type='reset' variant='warning'>Сбросить</Button>
        </ButtonGroup>
      </Form>
    );
  }
}

export { LoginForm };