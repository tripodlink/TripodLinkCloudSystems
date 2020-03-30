import { UserGroup } from './UserGroup';


export class UserAccount{
  userID: string = "";
  userName: string = "";
  password: string = "";
  isActive: boolean = true;
  createdOn: Date;
  createdBy: string = "";
  updatedOn: Date;
  updatedBy: string = "";

  userGroups: UserGroup[] = new Array();
}
