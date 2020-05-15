
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

export interface IinventoryOutPendingTrx {
  transactionNo: string;
  transactionDate: Date;
  issuedDate: Date;
  department: string;
  referenceNo: string;
}

export interface IinventoryOutIssuedTrx {
  transactionNo: string;
  transactionDate: Date;
  issuedDate: Date;
  department: string;
  referenceNo: string;
}
