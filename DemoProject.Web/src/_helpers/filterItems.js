import { List } from 'immutable';

export function filterItems(items, fields, filter) {
  const _items = List(items);
  const _fields = List(fields);
  const _filter = filter || '';

  if (_items.isEmpty() || _fields.isEmpty() || _filter === '') {
    return _items;
  }

  const regExp = new RegExp(_filter, 'i');

  // add tracking field
  let filteredItems = _items.map(item => item.set('valid', false));

  _fields.forEach(field => {
    filteredItems = filteredItems.map(item => _mapper(item, field, regExp));
  });

  filteredItems = filteredItems.filter(item => item.get('valid'));

  // remove tracking field
  filteredItems = filteredItems.map(item => item.delete('valid'));

  return filteredItems;
}

function _mapper(item, field, regExp) {
  if (item.get('valid') || item.get(field).search(regExp) === -1) {
    // already valid or invalid according to regular expression
    return item;
  }

  return item.set('valid', true);
}