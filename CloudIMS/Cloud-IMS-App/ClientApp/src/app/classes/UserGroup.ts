import { ProgramMenu } from "./ProgramMenu";
import { ProgramFolder } from "./ProgramFolder";

export class UserGroup {
  id: string;
  name: string;
  isApprover: string;

  programFolders: ProgramFolder[] = new Array();
  programMenus: ProgramMenu[] = new Array();
}
