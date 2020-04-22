import { IinventoryOutHeader } from "../inventory-out/IinventoryOutHeader.interface";

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
