import { UserGroup } from './UserGroup';


export class UserAccount{
  userID: string;
  userName: string;
  password: string;
  isActive: boolean;
  createdOn: Date;
  createdBy: string;
  updatedOn: Date;
  updatedBy: string;

  userGroup: UserGroup[];
}
