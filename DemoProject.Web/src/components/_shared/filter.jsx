import React from 'react';
import { Form } from 'react-bootstrap';
import { Switch } from '.';

class Filter extends React.Component {
  render() {
    const { name, value, placeholder, fields, onChange } = this.props;

    // const checkedkeys = fields && Object.keys(fields).filter(key => fields[key] === true);
    // {checkedkeys && <Form.Text className='text-muted'>{checkedkeys.join(', ')}</Form.Text>}
    return (
      <Form.Group controlId='table-searchbar'>
        <Form.Control type='search' name={name} value={value} placeholder={placeholder} onChange={onChange} />
        <div style={{ marginTop: '5px', marginBottom: '5px' }}>
          {
            fields && Object.keys(fields).map((key, index) => <Switch key={index} name={key} value={fields[key]} inline onChange={onChange} />)
          }
        </div>
      </Form.Group>
    );
  }
}

export { Filter };