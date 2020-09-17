
export interface IinventoryOutHeader {
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


export interface IinventoryOutFindTrx {
  transactionNo: string;
  transactionDate: Date;
  issuedDate: Date;
  receivedBy: string
  department: string;
  referenceNo: string;
  remarks: string;
  status: string;
}
