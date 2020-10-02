import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from "@angular/forms";
import { IReportTally } from '../../classes/report-management/report-tally/IReportTally.interface'
import { DatePipe } from '@angular/common';
import { ReportInventoryOutService } from '../../../app/services/ReportInventoryOut.service';
import * as XLSX from 'xlsx';
@Component({
  selector: 'app-report-tally',
  templateUrl: './report-tally.component.html',
  styleUrls: ['./report-tally.component.css']
})
export class ReportTallyComponent implements OnInit {

  invReportsTally: FormGroup;

  reportTally: IReportTally[];

  reportTallyArray: Array<{
    ItemID: string, ItemName: string, SupplierName: string, DateInventoryIn: string,
    InvoiceNumber: string, PONumber: string, LotNumber: string, ReceivedBy: string, Department: string, ItemUnit: string,
    ItemTotalCount: number, ItemInventoryIn: number, ItemInventoryOut: number, ItemDefect: number}> = [];

  Result: string;
  transactionDateFrom: Date;
  transactionDateTo: Date;
  ResultCount: string

  constructor(public builder: FormBuilder,
    public datePipe: DatePipe,
    public reportServices: ReportInventoryOutService) {
    this.createForm()
  }

  ngOnInit() {
    this.invReportsTally.get('dateFrom').patchValue(this.getMonth());
    this.invReportsTally.get('dateTo').patchValue(this.getDateNow());
  }

  public getDateNow(): string {
    let today = new Date();
    let getDate = today.toISOString().slice(0, 10);
    return getDate;

  }

  public getMonth(): string {
    let today = new Date(this.getDateNow());
    today.setMonth(today.getMonth() - 1)

    return this.datePipe.transform(today.toLocaleDateString(), "yyyy-MM-dd")

  }

  createForm() {
    this.invReportsTally = this.builder.group({
      dateFrom: [''],
      dateTo: [''],
      reportType: ['']
    })
  }

  public generateReports() {
     this.reportServices.getTallyReport().subscribe((array) => {
       this.reportTally = array;
      var count = 0;
      this.reportTallyArray = [];

       Array.from(this.reportTally).forEach((tally) => {

         this.reportTallyArray.push({
           ItemID: tally.itemID,
           ItemName: tally.itemName,
           SupplierName: tally.supplierName,
           DateInventoryIn: this.datePipe.transform(tally.dateInventoryIn, "yyyy-MM-dd hh:mm:ss"),
           InvoiceNumber: tally.invoiceNumber,
           PONumber: tally.poNumber,
           LotNumber: tally.lotNumber,
           ReceivedBy: tally.recievedBy,
           Department: tally.department,
           ItemUnit: tally.itemUnit,
           ItemTotalCount: tally.itemRemainingCount,
           ItemInventoryIn: tally.inventoryIn,
           ItemInventoryOut: tally.inventoryOut,
           ItemDefect: tally.itemDefect
           })
          
         count++
       })
       this.Result = "Found " + " " + count + " " + "Record";
       this.exportReport()
    })
  }
  exportReport() {
    let ws: XLSX.WorkSheet
    /* table id is passed over here */
    const header = ["Transaction Number", "Transaction Date", "Issued Date", "Received By", "Department", "Reference Number", "Remarks", "Item Name",
      "Description", "Lot Number", "Quantity", "Detail Remarks"]

    ws = XLSX.utils.json_to_sheet(this.reportTallyArray);

    /* generate workbook and add the worksheet */
    const wb: XLSX.WorkBook = XLSX.utils.book_new();

    XLSX.utils.book_append_sheet(wb, ws, 'Sheet1');


    /* save to file */
    let dateFrom = this.datePipe.transform(this.transactionDateFrom, 'MMMM d, y');
    let dateTo = this.datePipe.transform(this.transactionDateTo, 'MMMM d, y');
    XLSX.writeFile(wb, "Inventory Out For" + " " + dateFrom + " " + "to" + " " + dateTo + ".xlsx");
  }

}
