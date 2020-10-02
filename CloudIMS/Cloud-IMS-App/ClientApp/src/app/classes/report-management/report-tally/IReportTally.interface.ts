
export interface IReportTally {
  itemID: string,
  itemName: string,
  supplierName: string,
  dateInventoryIn: Date,
  invoiceNumber: string,
  poNumber: string,
  lotNumber: string,
  recievedBy: string,
  department: string,
  itemUnit: string,
  itemRemainingCount: number,
  inventoryIn: number,
  inventoryOut: number,
  itemDefect: number
}
