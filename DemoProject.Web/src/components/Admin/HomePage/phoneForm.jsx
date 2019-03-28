import React from 'react';
import { Form, Button } from 'react-bootstrap';

class PhoneForm extends React.Component {
  constructor(props) {
    super(props);

    this.onClickAdd = this.onClickAdd.bind(this);
  }

  onClickAdd() {
    if (this.refs.phoneInput.value !== '') {
      var itemText = this.refs.phoneInput.value;
      this.refs.phoneInput.value = '';
      return this.props.addPhone(itemText);
    }
  }

  render() {
    return (
      <Form>
        <Form.Group controlId='formAddNewPhoneName'>
          <Form.Control type='text' placeholder='some name' ref='phoneInput' />
        </Form.Group>
        <Button variant='primary' onClick={this.onClickAdd}>Добавить</Button>
      </Form>
    );
  }
}

export { PhoneForm };