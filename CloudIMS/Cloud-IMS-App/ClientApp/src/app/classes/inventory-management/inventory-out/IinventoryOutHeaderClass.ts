import { IinventoryOutHeader } from "../inventory-out/IinventoryOutHeader.interface";
import { IinventoryOutPendingTrx } from "../inventory-out/IinventoryOutHeader.interface";
import { IinventoryOutIssuedTrx } from "../inventory-out/IinventoryOutHeader.interface";

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

export class IinventoryOuPendingTrxClass implements IinventoryOutPendingTrx {
  transactionNo: string;
  transactionDate: Date;
  issuedDate: Date;
  department: string;
  referenceNo: string;
}

export class IinventoryOuIssuedTrxClass implements IinventoryOutIssuedTrx {
  transactionNo: string;
  transactionDate: Date;
  issuedDate: Date;
  department: string;
  referenceNo: string;
}
