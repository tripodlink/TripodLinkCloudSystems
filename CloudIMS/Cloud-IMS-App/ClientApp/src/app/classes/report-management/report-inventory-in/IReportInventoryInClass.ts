import { IReportInventoryIn } from "./IReportInventoryIn.interface";


export class IReportInventoryInClass implements IReportInventoryIn{
  trxno: string;
  trxdate: Date;
  itemname: string;
  itemunit: string;
  qty: number;
  lotno: string;
  supplier: string;
 
}
