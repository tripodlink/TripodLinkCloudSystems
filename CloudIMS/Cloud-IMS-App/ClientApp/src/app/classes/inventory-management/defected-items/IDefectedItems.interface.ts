export interface IDefectedItemsDetailFinal {
  transactionNo: string;
  transactionDate: Date;
  poNumber: string;
  invoiceNo: string;
  receivedDate: Date;
  receivedBy: string;
  supplier: string;
  referenceNo: string;
  documnetNo: string;
}

export interface IDefectedItemsGetLotNumberFromItemId {
  lotNumber: string;
}

export interface IDefectedItemsGetTransactionNumberFromItemIdLotNumber {
  transactionNo: string;
}

export interface IDefectedItemsGeItemtUnitFromItemID {
  unit: string;
  unitName: string;
  unitConversion: number;
}


export interface IDefectedItemsGeItemtNameFromItemID {
  itemName: string;
}

export interface IDefectedItemsToSave {
  defectTransactionNo: string;
  itemID: string;
  lotNumber: string;
  transactionNo: string;
  itemUnit: string;
  quantity: number;
  transactionDate: Date;
  remarks: string;
  status: string;
}

export interface IDefectedItemsList {
  defTransactionNo: string;
  itemName: string;
  lotNumber: string;
  transactionNo: string;
  transactionDate: Date;
  quantity: string;
  status: string;
}

export interface IDefectedItemsFillAll {
  status: string;
  itemId: string;
  itemName: string;
  lotNo: string;
  itemUnit: string;
  itemUnitName: string;
  itemUnitConversion: number;
  quantity: number;
  remarks: string;
  transactionNo: string;
  transactionDate: Date;
  poNo: string;
  invNo: string;
  receivedDate: Date;
  receivedBy: string;
  supplierName: string;
  refNo: string;
  documentNo: string;
}

export interface IDefectedItemsRemarks {
  remarks: string;
}

export interface IDefectedItemsItemMaster {
  itemId: string;
  itemName: string;
}



