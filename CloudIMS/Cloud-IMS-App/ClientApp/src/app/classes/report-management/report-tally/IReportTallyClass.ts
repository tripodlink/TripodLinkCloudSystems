import { IReportTally } from "./IReportTally.interface";

export class IReportTallyClass implements IReportTally{
  ItemID: string
  ItemName: string
  SupplierName: string
  DateInventoryIn: string
  DateInventoryOut: string
  InvoiceNumber: string
  PONumber: string
  LotNumber: string
  ReceivedBy: string
  Department: string
  ItemUnit: string
  ItemInventoryIn: string
  ItemInventoryOut: string
  ItemDefect: string
}

