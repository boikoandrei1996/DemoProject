import React from 'react';
import { Pagination } from 'react-bootstrap';

class MyPagination extends React.Component {
  render() {
    const { items, active } = this.props;

    return (
      <Pagination className='justify-content-center'>
        {items.map(x => <Pagination.Item key={x} active={x === active} onClick={() => this.props.onClick(x)}>{x}</Pagination.Item>)}
      </Pagination>
    );
  }
}

export { MyPagination };