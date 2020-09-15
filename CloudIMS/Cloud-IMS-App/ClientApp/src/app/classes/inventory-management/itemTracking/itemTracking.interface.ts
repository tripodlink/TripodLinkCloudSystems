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
