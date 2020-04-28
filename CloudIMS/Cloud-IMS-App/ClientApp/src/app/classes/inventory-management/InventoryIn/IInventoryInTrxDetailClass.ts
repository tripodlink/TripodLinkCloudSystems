import { IInventoryInTrxDetail } from "./IInventoryInTrxDetail.interface";

export class IInventoryInTrxDetailClass implements IInventoryInTrxDetail{
    transactionNo: string;
    itemID: string;
    unit: string;
    quantity: number;
    lotNumber: string;
    expirationDate: Date;
    count: number;
    remainigCount: number;
    
}
