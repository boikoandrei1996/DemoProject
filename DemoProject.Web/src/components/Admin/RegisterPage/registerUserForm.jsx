import React from 'react';
import { fromJS } from 'immutable';
import { Form, Button, ButtonGroup } from 'react-bootstrap';

class RegisterUserForm extends React.Component {
  constructor(props) {
    super(props);

    this.handleSubmit = this.handleSubmit.bind(this);
    this.handleReset = this.handleReset.bind(this);
  }

  handleReset(event) {
    const confirmed = confirm('Are you sure to reset form?');

    if (!confirmed) {
      event.preventDefault();
    }
  }

  handleSubmit(event) {
    event.preventDefault();

    let user = {};
    let isValid = true;

    // username
    let username = this.refs.username.value;
    if (!username) {
      isValid = false;
      alert('Username is required');
    }
    else {
      user.username = username;
    }

    // role
    let role = this.refs.role.value;
    if (!role) {
      isValid = false;
      alert('Role is required');
    }
    else {
      user.role = role;
    }

    // password
    let password = this.refs.password.value;
    if (!password) {
      isValid = false;
      alert('Password is required');
    }
    else {
      user.password = password;
    }

    // confirmPassword
    /*let confirmPassword = this.refs.confirmPassword.value;
    if (!confirmPassword) {
      isValid = false;
      alert('Confirm password is required');
    }
    else if (password !== confirmPassword) {
      isValid = false;
      alert('Password is not the same');
    }
    else {
      this.refs.confirmPassword.value = '';
      user.confirmPassword = confirmPassword;
    }

    // firstName
    let firstName = this.refs.firstName.value;
    if (!firstName) {
      isValid = false;
      alert('FirstName is required');
    }
    else {
      this.refs.firstName.value = '';
      user.firstName = firstName;
    }

    // lastName
    let lastName = this.refs.lastName.value;
    if (!lastName) {
      isValid = false;
      alert('LastName is required');
    }
    else {
      this.refs.lastName.value = '';
      user.lastName = lastName;
    }

    // email
    let email = this.refs.email.value;
    if (!email) {
      isValid = false;
      alert('Email is required');
    }
    else {
      this.refs.email.value = '';
      user.email = email;
    }

    // phoneNumber
    let phoneNumber = this.refs.phoneNumber.value;
    if (!phoneNumber) {
      isValid = false;
      alert('Phone is required');
    }
    else {
      this.refs.phoneNumber.value = '';
      user.phoneNumber = phoneNumber;
    }*/

    if (isValid) {
      this.props.submitUser(fromJS(user));
    }
  }

  render() {
    return (
      <Form onSubmit={this.handleSubmit} onReset={this.handleReset}>
        <Form.Group controlId='formRegisterUser_Username'>
          <Form.Label>Username</Form.Label>
          <Form.Control type='text' placeholder='username' ref='username' />
        </Form.Group>

        <Form.Group controlId='formRegisterUser_Role'>
          <Form.Label>Role</Form.Label>
          <Form.Control type='text' placeholder='role' ref='role' />
        </Form.Group>

        <Form.Group controlId='formRegisterUser_Password'>
          <Form.Label>Password</Form.Label>
          <Form.Control type='password' placeholder='password' ref='password' />
        </Form.Group>

        <ButtonGroup aria-label='register-user-form-actions'>
          <Button type='submit' variant='success'>Зарегистрировать</Button>
          <Button type='reset' variant='warning'>Сбросить</Button>
        </ButtonGroup>
      </Form>
    );
  }
}

export { RegisterUserForm };