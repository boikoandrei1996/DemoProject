// value should be lowercase
export const RoleEnum = Object.freeze({
  Admin: 'admin',
  Moderator: 'moderator'
});

export const RoleAccessesEnum = Object.freeze({
  Admin: [RoleEnum.Admin], // only admin has access
  Moderator: [RoleEnum.Admin, RoleEnum.Moderator] // both admin and moderator have access
});

export const roles = [
  {
    key: '-- Please choose a role --',
    value: ''
  },
  {
    key: 'Admin',
    value: RoleEnum.Admin
  },
  {
    key: 'Moderator',
    value: RoleEnum.Moderator
  },
  {
    key: 'wrong role name',
    value: 'wrong role'
  }
];