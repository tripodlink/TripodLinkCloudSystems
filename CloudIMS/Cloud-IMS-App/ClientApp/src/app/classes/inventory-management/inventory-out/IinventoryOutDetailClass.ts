import { IinventoryOutDetail } from "../inventory-out/IinventoryOutDetail.interface";
import { IinventoryOutTable } from "../inventory-out/IinventoryOutDetail.interface";

export class IinventoryOutDetailClass implements IinventoryOutDetail {
  transactionNo: string;
  itemID: string;
  unit: string;
  in_TrxNo: string;
  quantity: number
  remarks: string;
  minCount: number;
}

export class IinventoryOutTableClass implements IinventoryOutTable {
  itoh_ID: string;
  transactionNo: string;
  transactionDate: Date;
  department: string;
  referenceNo: string;
  headremarks: string;

  itemID: string;
  itemName: string;

  itemMasterUnitConversion: number;

  unit: string;
  description: string;

  in_TrxNo: string;
  lotNumber: string;

  quantity: number
  remarksID: string
  remarks: string;
  minCount: number;

  expirationDate: Date;
  remainigCount: number;
}
