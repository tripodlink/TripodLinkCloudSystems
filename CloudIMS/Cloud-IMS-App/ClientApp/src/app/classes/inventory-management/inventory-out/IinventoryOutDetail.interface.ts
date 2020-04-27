
export interface IinventoryOutDetail {
  transactionNo: string;
  itemID: string;
  unit: string;
  in_TrxNo: string;
  quantity: number
  remarks: string;
  minCount: number;
}


export interface IinventoryOutTable {
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
  remarks: string;
  minCount: number;

  expirationDate: Date;
  remainigCount: number;
}
