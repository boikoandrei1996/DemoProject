export const roles = [
  {
    key: '-- Please choose a role --',
    value: ''
  },
  {
    key: 'Admin',
    value: 'Admin'
  },
  {
    key: 'Moderator',
    value: 'Moderator'
  },
  {
    key: 'wrong role name',
    value: 'wrong role'
  }
];

// value should be lowercase
export const RoleEnum = Object.freeze({
  Admin: ['admin'], // only admin has access
  Moderator: ['admin', 'moderator'] // both admin and moderator have access
});