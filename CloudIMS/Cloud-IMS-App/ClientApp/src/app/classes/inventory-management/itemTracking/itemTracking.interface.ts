export interface itemLotNo {
  transactionNo: string;
  itemID: string;
  lotNumber: string;
}

export interface ItemTracking {
  transactionNo: string;
  itemID: string;
  lotNo: string;
  dateUpdated: Date;
  location: string
  locationID: string
}


export interface AuditTrail {
  inTrasactionNo: string,
  itemID: string,
  itemName: string,
  supplierName: string,
  dateInventoryIn: Date,
  invoiceNumber: string,
  poNumber: string,
  lotNumber: string,
  receivedBy: string,
  itemRemainingCount: number,
  inventoryIn: number,
  outTransactionNo: string,
  dateInventoryOut: Date,
  department: string,
  requestedBy: string,
  issuedBy: string,
  itemUnit: string,
  unitID: string,
  inventoryOut: number,
  itemDefect: number
}
