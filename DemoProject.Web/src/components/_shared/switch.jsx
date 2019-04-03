import React from 'react';

class Switch extends React.Component {
  render() {
    const { name, value, inline, onChange } = this.props;

    const inlineClass = inline ? ' custom-control-inline' : '';

    return (
      <div className={'custom-control custom-switch' + inlineClass}>
        <input type='checkbox' id={name} name={name} checked={value} className='custom-control-input' onChange={onChange} />
        <label htmlFor={name} className='custom-control-label'>{name}</label>
      </div>
    );
  }
}

export { Switch };