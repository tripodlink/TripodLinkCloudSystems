import { IInventoryInTrxHeader } from "./IInventoryInTrxHeader.interface";

export class IInventoryInTrxHeaderClass implements IInventoryInTrxHeader{
  transactionNo: string = "";
  transactionDate: Date;
  receivedDate: Date;
  receivedBy: string = "";
  poNumber: string = "";
  invoiceNo: string = "";
  referenceNo: string = "";
  documnetNo: string = "";
  supplier: string = "";
  remarks: string = "";
 
}
