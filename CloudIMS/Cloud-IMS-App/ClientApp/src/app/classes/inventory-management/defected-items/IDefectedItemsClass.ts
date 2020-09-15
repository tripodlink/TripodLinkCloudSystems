import { IDefectedItemsDetailFinal } from "./IDefectedItems.interface";
import { IDefectedItemsGetLotNumberFromItemId } from "./IDefectedItems.interface";
import { IDefectedItemsGetTransactionNumberFromItemIdLotNumber } from "./IDefectedItems.interface";
import { IDefectedItemsGeItemtUnitFromItemID } from "./IDefectedItems.interface";
import { IDefectedItemsGeItemtNameFromItemID } from "./IDefectedItems.interface";
import { IDefectedItemsToSave } from "./IDefectedItems.interface";
import { IDefectedItemsList } from "./IDefectedItems.interface";
import { IDefectedItemsFillAll } from "./IDefectedItems.interface";
import { IDefectedItemsRemarks } from "./IDefectedItems.interface";
import { IDefectedItemsItemMaster } from "./IDefectedItems.interface";


export class IDefectedItemsFinalClass implements IDefectedItemsDetailFinal {
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

export class IDefectedItemsLotNumberClass implements IDefectedItemsGetLotNumberFromItemId {
  lotNumber: string;
}

export class IDefectedItemsTransactionNumberClass implements IDefectedItemsGetTransactionNumberFromItemIdLotNumber {
  transactionNo: string;
}

export class IDefectedItemsUnitIdClass implements IDefectedItemsGeItemtUnitFromItemID {
  unit: string;
  unitName: string;
  unitConversion: number;
}

export class IDefectedItemsNameClass implements IDefectedItemsGeItemtNameFromItemID {
  itemName: string;
}

export class IDefectedItemsToSaveClass implements IDefectedItemsToSave {
  defectTransactionNo: string;
  itemID: string;
  itemUnit: string;
  quantity: number;
  lotNumber: string;
  transactionNo: string;
  transactionDate: Date;
  remarks: string;
  status: string;
}

export class IDefectedItemsListClass implements IDefectedItemsList {
  defTransactionNo: string;
  itemName: string;
  lotNumber: string;
  transactionNo: string;
  transactionDate: Date;
  quantity: string;
  status: string;
}

export class IDefectedItemsFillAllClass implements IDefectedItemsFillAll {
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

export class IDefectedItemsRemarksClass implements IDefectedItemsRemarks {
  remarks: string;
}

export class IDefectedItemsItemMasterClass implements IDefectedItemsItemMaster {
  itemId: string;
  itemName: string;
}



