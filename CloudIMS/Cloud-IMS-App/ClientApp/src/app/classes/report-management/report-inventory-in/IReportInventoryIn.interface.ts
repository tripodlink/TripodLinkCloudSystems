
export interface IReportInventoryIn {
  transactionNo: string;
  transactionDate: Date;
  itemID: string;
  unit: string;
  quantity: number;
  lotNumber: string;
  supplier: string;
}
