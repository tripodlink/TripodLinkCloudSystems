import { IReportInventoryIn } from "./IReportInventoryIn.interface";


export class IReportInventoryInClass implements IReportInventoryIn{
  transactionNo: string = "";
  transactionDate: Date;
  itemID: string;
  unit: string;
  quantity: number;
  lotNumber: string;
  supplier: string = "";
 
}
