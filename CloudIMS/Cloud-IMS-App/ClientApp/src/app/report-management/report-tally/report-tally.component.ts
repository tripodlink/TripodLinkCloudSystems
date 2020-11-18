import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from "@angular/forms";
import { IReportTally } from '../../classes/report-management/report-tally/IReportTally.interface'
import { DatePipe } from '@angular/common';
import { ReportInventoryOutService } from '../../../app/services/ReportInventoryOut.service';
import * as XLSX from 'xlsx';
import { async } from 'rxjs/internal/scheduler/async';
import { addSyntheticLeadingComment } from 'typescript';
import { asap } from 'rxjs/internal/scheduler/asap';
import { getInterpolationArgsLength } from '@angular/compiler/src/render3/view/util';

@Component({
  selector: 'app-report-tally',
  templateUrl: './report-tally.component.html',
  styleUrls: ['./report-tally.component.css']
})
export class ReportTallyComponent implements OnInit {

  invReportsTally: FormGroup;

  reportTally: IReportTally[];

  reportTallyArray: Array<{
    ItemID: string, ItemName: string, SupplierName: string, DateInventoryIn: string, DateInventoryOut: string,
    InvoiceNumber: string, PONumber: string, LotNumber: string, ReceivedBy: string, Department: string, ItemUnit: string,
    ItemInventoryIn: string, ItemInventoryOut: string, ItemDefect: string
  }> = [];

  Result: string;
  transactionDateFrom: Date;
  transactionDateTo: Date;
  ResultCount: string

  ItemTotalCount: number = 0
  ItemInventoryIn: number = 0
  ItemInventoryOut: number = 0
  ItemDefect: number = 0
  count: number = 0

  constructor(public builder: FormBuilder,
    public datePipe: DatePipe,
    public reportServices: ReportInventoryOutService) {
    this.createForm()
  }

  ngOnInit() {
    this.invReportsTally.get('dateFrom').patchValue(this.getMonth());
    this.invReportsTally.get('dateTo').patchValue(this.getDateNow());
    console.log("Tally Report")
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

  async generateReports() {

    let getTallyReport = this.reportServices.getTally()

    await getTallyReport.toPromise().then(async (array) => {
      this.count = 0
      //this.reportTally = array;

      //this.reportTallyArray = this.reportTally;

      let arr = Object.entries(array)
      arr.map(item => {

        this.reportTallyArray.push({
          ItemID: item[1]['itemID'],
          ItemName: item[1]['itemName'],
          SupplierName: item[1]['supplierName: '],
          DateInventoryIn: item[1]['dateInventoryIn'],
          DateInventoryOut: item[1]['dateInventoryOut'],
          InvoiceNumber: item[1]['invoiceNumber'],
          PONumber: item[1]['poNumber'],
          LotNumber: item[1]['lotNumber'],
          ReceivedBy: item[1]['receivedBy'],
          Department: item[1]['receivedBy'],
          ItemUnit: item[1]['itemUnit'],
          ItemInventoryIn: item[1]['itemInventoryIn'],
          ItemInventoryOut: item[1]['itemInventoryOut'],
          ItemDefect: item[1]['itemDefect']
        })
        this.count++
        this.Result = "Found " + " " + this.count + " " + "Record";
      })
    })
    await getTallyReport.toPromise().then(async () => { await this.exportReport() })

  }

  async getItemTotalCountClass(itemID: string, lotNumber: string) {
    let getItemTotalAccount = this.reportServices.getItemTotalCount(itemID, lotNumber)
    await getItemTotalAccount.toPromise().then((itemtotalcount) => {
      this.ItemTotalCount = itemtotalcount
    })
    return this.ItemTotalCount
  }

  async getItemInventoryInClass(itemID: string, lotNumber: string) {
    let getInventoryIn = this.reportServices.getItemInventoryIn(itemID, lotNumber)
    await getInventoryIn.toPromise().then((inventoryIn) => {
      this.ItemInventoryIn = inventoryIn
    })
    return this.ItemInventoryIn
  }

  async getItemInventoryOutClass(itemID: string, itemTransactionNumber: string) {

    let getItemInventoryOut = this.reportServices.getItemInventoryOut(itemID, itemTransactionNumber)
    await getItemInventoryOut.toPromise().then((inventoryOut) => {
      this.ItemInventoryOut = inventoryOut
    })
    return this.ItemInventoryOut
  }

  async getItemDefectClass(itemID: string, lotNumber: string) {
    let getItemDefect = this.reportServices.getItemDefect(itemID, lotNumber)
    await getItemDefect.toPromise().then((itmDefect) => {
      this.ItemDefect = itmDefect
    })
    return this.ItemDefect
  }


  async exportReport(): Promise<any> {
    try {

      Promise.resolve()
        .then(async () => {
          let ws: XLSX.WorkSheet
          ws = XLSX.utils.json_to_sheet([{A:"ItemID", B:"ItemName", C:"SupplierName", D:"DateInventoryIn", E:"DateInventoryOut", F:"InvoiceNumber", G:"PONumber"
              , H:"LotNumber", I:"ReceivedBy", J:"Department", K:"ItemUnit", L:"ItemInventoryIn", M:"ItemInventoryOut", N:"ItemDefect"
          }], { header: ["A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N"], skipHeader: true });
          XLSX.utils.sheet_add_json(ws,this.reportTallyArray, {skipHeader: true, origin:"A2"});


          /* generate workbook and add the worksheet */
          const wb: XLSX.WorkBook = XLSX.utils.book_new();
          console.log("generated")

          await XLSX.utils.book_append_sheet(wb, ws, 'Sheet1');

          await XLSX.writeFile(wb, "Inventory Tally.xlsx");

        })
    } catch (e) {
      console.log(e)
    }

  }

}
