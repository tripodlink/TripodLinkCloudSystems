import { IinventoryOutHeader } from "../inventory-out/IinventoryOutHeader.interface";
import { IinventoryOutFindTrx } from "../inventory-out/IinventoryOutHeader.interface";


export class IinventoryOutHeaderClass implements IinventoryOutHeader {
  transactionNo: string;
  transactionDate: Date;
  issuedBy: string
  issuedDate: Date;
  receivedBy: string
  department: string;
  referenceNo: string;
  remarks: string;
  status: string;
}

export class IinventoryOuIssuedTrxClass implements IinventoryOutFindTrx {
  transactionNo: string;
  transactionDate: Date;
  issuedDate: Date;
  receivedBy: string
  department: string;
  referenceNo: string;
  remarks: string;
  status: string;
}
