import React from 'react';
import { fromJS } from 'immutable';
import { Form, Button, ButtonGroup, Row, Col } from 'react-bootstrap';
import { roles } from '@/_helpers';

const initialState = {
  user: {
    username: '',
    role: '',
    password: '',
    confirmPassword: '',
    firstName: '',
    lastName: '',
    email: '',
    phoneNumber: ''
  },
  validated: false
};

class RegisterUserForm extends React.Component {
  constructor(props) {
    super(props);

    this.state = initialState;

    this.handleChange = this.handleChange.bind(this);
    this.handleSubmit = this.handleSubmit.bind(this);
    this.handleReset = this.handleReset.bind(this);
  }

  handleChange(event) {
    const { name, value, type, checked } = event.target;

    this.setState(prevState => ({
      user: {
        ...prevState.user,
        [name]: type === 'checkbox' ? checked : value
      }
    }));
  }

  handleReset(event) {
    event.preventDefault();
    const confirmed = confirm('Are you sure to reset form?');

    if (confirmed) {
      this.setState(initialState);
      this.props.resetForm();
    }
  }

  handleSubmit(event) {
    event.preventDefault();
    const { user } = this.state;
    let isValid = event.target.checkValidity();

    // confirmPassword
    if (isValid && user.password !== user.confirmPassword) {
      isValid = false;
      alert('Password is not the same');
    }

    if (isValid) {
      this.props.submitUser(fromJS(user));
      this.setState({
        validated: false
      });
    }
    else {
      this.setState({
        validated: true
      });
    }
  }

  render() {
    const { user, validated } = this.state;

    return (
      <Form noValidate validated={validated} onSubmit={this.handleSubmit} onReset={this.handleReset}>
        <Form.Group as={Row} controlId='formRegisterUser_Username'>
          <Form.Label column md={2}>Username</Form.Label>
          <Col md={10}>
            <Form.Control required type='text' name='username' value={user.username} placeholder='username' onChange={this.handleChange} />
            <Form.Control.Feedback type='invalid'>Field is required!</Form.Control.Feedback>
          </Col>
        </Form.Group>

        <Form.Group as={Row} controlId='formRegisterUser_Role'>
          <Form.Label column md={2}>Role</Form.Label>
          <Col md={10}>
            <Form.Control required as='select' name='role' value={user.role} onChange={this.handleChange}>
              {
                roles.map(role => <option key={role.key} value={role.value}>{role.key}</option>)
              }
            </Form.Control>
            <Form.Control.Feedback type='invalid'>Field is required!</Form.Control.Feedback>
          </Col>
        </Form.Group>

        <Form.Row>
          <Form.Group as={Col} controlId='formRegisterUser_Password'>
            <Form.Label>Password</Form.Label>
            <Form.Control type='text' name='password' value={user.password} placeholder='password' onChange={this.handleChange} />
          </Form.Group>

          <Form.Group as={Col} controlId='formRegisterUser_ConfirmPassword'>
            <Form.Label>Confirm password</Form.Label>
            <Form.Control type='password' name='confirmPassword' value={user.confirmPassword} placeholder='Confirm password' onChange={this.handleChange} />
          </Form.Group>
        </Form.Row>

        <Form.Row>
          <Form.Group as={Col} controlId='formRegisterUser_FirstName'>
            <Form.Label>First name</Form.Label>
            <Form.Control type='text' name='firstName' value={user.firstName} placeholder='first name' onChange={this.handleChange} />
          </Form.Group>

          <Form.Group as={Col} controlId='formRegisterUser_LastName'>
            <Form.Label>Last name</Form.Label>
            <Form.Control type='text' name='lastName' value={user.lastName} placeholder='last name' onChange={this.handleChange} />
          </Form.Group>
        </Form.Row>

        <Form.Row>
          <Form.Group as={Col} controlId='formRegisterUser_Email'>
            <Form.Label>Email</Form.Label>
            <Form.Control type='email' name='email' value={user.email} placeholder='email@example.com' onChange={this.handleChange} />
          </Form.Group>

          <Form.Group as={Col} controlId='formRegisterUser_Phone'>
            <Form.Label>Phone number</Form.Label>
            <Form.Control type='text' name='phoneNumber' value={user.phoneNumber} placeholder='375(29)123-45-67' onChange={this.handleChange} />
          </Form.Group>
        </Form.Row>

        <ButtonGroup aria-label='register-user-form-actions'>
          <Button type='submit' variant='success'>Зарегистрировать</Button>
          <Button type='reset' variant='warning'>Сбросить</Button>
        </ButtonGroup>
      </Form>
    );
  }
}

export { RegisterUserForm };