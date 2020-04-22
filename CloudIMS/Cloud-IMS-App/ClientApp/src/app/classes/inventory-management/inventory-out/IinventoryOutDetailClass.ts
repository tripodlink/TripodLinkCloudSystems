import { IinventoryOutDetail } from "../inventory-out/IinventoryOutDetail.interface";

export class IinventoryOutDetailClass implements IinventoryOutDetail {
  transactionNo: string;
  itemID: string;
  unit: string;
  in_TrxNo: string;
  quantity: number
  remarks: string;
  minCount: number;
}
