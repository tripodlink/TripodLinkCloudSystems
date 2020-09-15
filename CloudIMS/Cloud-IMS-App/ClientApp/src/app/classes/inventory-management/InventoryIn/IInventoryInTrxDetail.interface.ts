
export interface IInventoryInTrxDetail {
  transactionNo: string;
  itemID: string;
  unit: string;
  quantity: number;
  lotNumber: string;
  expirationDate: Date;
  count: number;
  remainigCount: number;
}

export interface IInventoryInDetailByBatch {

  transactionNo: string;
  itemID: string;
  unit: string;
  quantity: number;
  lotNumber: string;
  expirationDate: Date;
  count: number;
  remainigCount: number;
  transactionDate: Date;
  receivedDate: Date;
  receivedBy: string;
  poNumber: string;
  invoiceNo: string;
  referenceNo: string;
  documnetNo: string;
  supplier: string;
  remarks: string;
}

export interface IInventoryInTrxList {
  transactionNo: string;
  itemID: string;
  itemName: string;
  unit: string;
  unitName: string;
  quantity: number;
  lotNumber: string;
  expirationDate: Date;
  count: number;
  remainigCount: number;
  transactionDate: Date;
  receivedDate: Date;
  receivedBy: string;
  poNumber: string;
  invoiceNo: string;
  referenceNo: string;
  documnetNo: string;
  supplierId: string;
  supplierName: string;
  remarks: string;
}
