import { IReportTally } from "./IReportTally.interface";

export class IReportTallyClass implements IReportTally{
  itemID: string
  itemName: string
  supplierName: string
  dateInventoryIn: Date
  invoiceNumber: string
  poNumber: string
  lotNumber: string
  inTransactionNumber: string
  recievedBy: string
  department: string
  itemUnit: string
  unitID: string
  itemRemainingCount: number
  inventoryIn: number
  inventoryOut: number
  itemDefect: number
}

