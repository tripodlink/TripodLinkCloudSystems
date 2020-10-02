import {IReportTally } from "./IReportTally.interface";


export class IReportTallyClass implements IReportTally{
  itemID: string
  itemName: string
  supplierName: string
  dateInventoryIn: Date
  invoiceNumber: string
  poNumber: string
  lotNumber
  recievedBy: string
  department: string
  itemUnit: string
  itemRemainingCount: number
  inventoryIn: number
  inventoryOut: number
  itemDefect: number
}
