import { ProgramMenu } from './ProgramMenu';

export class ProgramFolder{
  id: string = "";
  name: string = "";
  routeAttribute: string = "";
  iconType: string = "";
  iconProvider: string = ""; 
  icon: string = "";
  sequenceNo: number = 0;

  programMenus: ProgramMenu[] = new Array();
}
