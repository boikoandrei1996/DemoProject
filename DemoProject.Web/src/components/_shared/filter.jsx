import React from 'react';
import { Form } from 'react-bootstrap';

class Filter extends React.Component {
  render() {
    const { name, value, placeholder, fields, onChange } = this.props;

    return (
      <Form.Group controlId='table-searchbar'>
        <Form.Control type='text' name={name} value={value} placeholder={placeholder} onChange={onChange} />
        {fields && <Form.Text className='text-muted'>{fields.join(', ')}</Form.Text>}
      </Form.Group>
    );
  }
}

export { Filter };