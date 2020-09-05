import {IReportInventoryOut } from "./IReportInventoryOut.interface";


export class IReportInventoryOutClass implements IReportInventoryOut{
  //Header
  //Header
  headerTransactionNo: string;
  transactionDate: Date
  issuedDate: Date
  issuedBy: string
  receivedBy: string
  department: string;
  departmentName: string;
  referenceNo: string;
  headerRemarks: string;

  //Detail
  itemName: string;
  unit: string;
  itemMasterUnitUnit: string;
  description: string;
  lotNumber: string;
  quantity: number
  detailRemarks: string;
}
