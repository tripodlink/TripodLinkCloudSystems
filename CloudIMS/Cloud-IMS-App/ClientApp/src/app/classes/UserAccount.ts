import { UserGroup } from './UserGroup';
import { ProgramFolder } from './ProgramFolder';
import { ProgramMenu } from './ProgramMenu';


export class UserAccount{
  userID: string = "";
  userName: string = "";
  password: string = "";
  isActive: boolean = true;
  createdOn: Date;
  createdBy: string = "";
  updatedOn: Date;
  updatedBy: string = "";
  token: string = "";

  userGroups: UserGroup[] = new Array();
  programFolders: ProgramFolder[] = new Array();
  programMenus: ProgramMenu[] = new Array();
}
