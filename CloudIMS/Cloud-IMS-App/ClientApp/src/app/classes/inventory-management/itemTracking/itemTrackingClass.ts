import { itemLotNo,ItemTracking } from "../itemTracking/itemTracking.interface";

export class itemLotNoClass implements itemLotNo {
  transactionNo: string;
  itemID: string;
  lotNumber: string;

}

export class itemTracking implements ItemTracking {
  transactionNo: string;
  itemID: string;
  lotNo: string;
  dateUpdated: Date;
  location: string
  locationID: string
}



