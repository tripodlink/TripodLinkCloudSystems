import {IReportInventoryOut } from "./IReportInventoryOut.interface";


export class IReportInventoryOutClass implements IReportInventoryOut{
  //Header
  HeaderTransactionNo: string;
  transactionDateFrom: Date;
  transactionDateTo: Date;
  issuedBy: string
  issuedDateFrom: Date;
  issuedDateTo: Date;
  receivedBy: string

  department: string;
  departmentName: string;

  referenceNo: string;
  HeaderRemarks: string;

  //Detail
  DetailTransactionNo: string;

  itemID: string;
  ItemName: string;

  unit: string;
  itemMasterUnitUnit: string;
  description: string;

  in_TrxNo: string;
  lotNumber: string;

  quantity: number
  DetailRemarks: string;
}
