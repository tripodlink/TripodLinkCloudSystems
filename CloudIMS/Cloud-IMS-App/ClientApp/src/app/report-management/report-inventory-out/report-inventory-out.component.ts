import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { FormBuilder, FormControl, FormGroup, Validators } from "@angular/forms";
import { ReportInventoryOutService } from '../../../app/services/ReportInventoryOut.service';
import { IReportInventoryOut } from '../../classes/report-management/report-inventory-out/IReportInventoryOut.interface';
import { IReportInventoryOutClass } from '../../classes/report-management/report-inventory-out/IReportInventoryOutClass';
import { IiTemMaster } from '../../classes/data-dictionary/ItemMaster/IitemMaster.interface';
import { IUnitCode } from '../../classes/data-dictionary/UnitCode/IUnitCode.interface';
import { IDepartment } from '../../classes/data-dictionary/Department/IDepartment.interface';
import * as XLSX from 'xlsx';
import * as FileSaver from 'file-saver';
import { DatePipe } from '@angular/common';
import { trimTrailingNulls } from '@angular/compiler/src/render3/view/util';

@Component({
  selector: 'app-report-inventory-out',
  templateUrl: './report-inventory-out.component.html',
  styleUrls: ['./report-inventory-out.component.css']
})
export class ReportInventoryOutComponent implements OnInit {

  invInReports: FormGroup;
  itemArray: IiTemMaster[];
  unitArray: IUnitCode[];
  depArray: IDepartment[];

  reportOutArray: IReportInventoryOut[];
  reportOutNewArray: Array<{
    TransactionNumber: string, TransactionDate: string, IssuedDate: string, IssuedBy: string, ReceivedBy: string,
    DepartmentName: string, ReferenceNumber: string, Remarks, ItemName: string, Description: string,
    LotNumber: string, Quantity: number, DetailRemarks: string }> = [];

  Result: string;
    transactionDateFrom: Date;
    transactionDateTo: Date;
  errormessage: string;
  constructor(public builder: FormBuilder, public reportServices: ReportInventoryOutService,
  public datePipe: DatePipe) {
    this.createForm()
  }

  ngOnInit() {
    this.loadData();
    console.log("Report Out")
  }
  createForm() {
    this.invInReports = this.builder.group({
      TransactionNumber: [''],
      ItemName: [''],
      ItemUnit: [''],
      department: [''],
      dateFrom: [''],
      dateTo: [''],
      reportType: ['']
    })
  }

  public loadData() {
    this.invInReports.get('dateFrom').patchValue(this.getDateNow());
    this.invInReports.get('dateTo').patchValue(this.getDateNow());

    this.reportServices.getItem().subscribe((data) => this.itemArray = data);
    this.reportServices.getUnit().subscribe((data) => this.unitArray = data);
    this.reportServices.getDepartment().subscribe((data) => this.depArray = data);
  }

  public getDateNow(): string {
    let today = new Date();
    let getDate = today.toISOString().slice(0, 10);
    return getDate;
  
  }
  public generateReports() {
    let HeaderTransactionNo = this.invInReports.controls.TransactionNumber.value;

    let itemID = this.invInReports.controls.ItemName.value;
    let itemInnerHtml;
    if (itemID != "") {
      itemInnerHtml = document.getElementById(itemID).innerHTML;
    } else {
      itemInnerHtml = "%";
    }

    let unitcode = this.invInReports.controls.ItemUnit.value;
    let unitcodeInnerHtml
    if (unitcode != "") {
      unitcodeInnerHtml= document.getElementById(unitcode).innerHTML;
    } else {
      unitcodeInnerHtml= "%";
    }

    let department = this.invInReports.controls.department.value;
    let departmentInnerHtml;
    if (department != "") {
      departmentInnerHtml = document.getElementById(department).innerHTML;
    } else {
      departmentInnerHtml = "%";
    }
    this.transactionDateFrom = this.invInReports.controls.dateFrom.value;
    this.transactionDateTo = this.invInReports.controls.dateTo.value;
    
    this.reportServices.getReport(HeaderTransactionNo, itemInnerHtml, unitcodeInnerHtml,
      departmentInnerHtml, this.transactionDateFrom, this.transactionDateTo,
      this.invInReports.controls.reportType.value).subscribe((data) => {
        this.reportOutArray = data;
        if (this.reportOutArray == null || this.reportOutArray.length == 0) {
          this.Result = "No Data Found";
        }
        else {

          var count = 0;
          this.reportOutNewArray = [];
         

          Array.from(this.reportOutArray).forEach((arr) => {
            this.reportOutNewArray.push({
              TransactionNumber: arr.headerTransactionNo,
              TransactionDate: this.datePipe.transform(arr.transactionDate, "yyyy-MM-dd hh:mm:ss"),
              IssuedDate: this.datePipe.transform(arr.issuedDate, "yyyy-MM-dd hh:mm:ss"),
              IssuedBy: arr.issuedBy,
              ReceivedBy: arr.receivedBy,
              DepartmentName: arr.departmentName,
              ReferenceNumber: arr.referenceNo,
              Remarks: arr.headerRemarks,
              ItemName: arr.itemName,
              Description: arr.description,
              LotNumber: arr.lotNumber,
              Quantity: arr.quantity,
              DetailRemarks: arr.detailRemarks
            });
            count++;
          });
          this.Result = "Found " + " " + count + " " + "Record";
          this.exportReport();
        }
      },
     
        error => {
          this.errormessage = error.error;
          this.Result = this.errormessage + " " + "Error";
        });
  }

  exportReport() {
    let ws: XLSX.WorkSheet
    /* table id is passed over here */
    const header = ["Transaction Number", "Transaction Date", "Issued Date", "Received By", "Department", "Reference Number", "Remarks", "Item Name",
      "Description", "Lot Number", "Quantity", "Detail Remarks"]

    ws = XLSX.utils.json_to_sheet(this.reportOutNewArray);

    /* generate workbook and add the worksheet */
    const wb: XLSX.WorkBook = XLSX.utils.book_new();

    XLSX.utils.book_append_sheet(wb, ws, 'Sheet1');


    /* save to file */
    let dateFrom = this.datePipe.transform(this.transactionDateFrom, 'MMMM d, y');
    let dateTo = this.datePipe.transform(this.transactionDateTo, 'MMMM d, y');
    XLSX.writeFile(wb, "Inventory Out For" + " " + dateFrom + " " + "to" + " " + dateTo + ".xlsx");
  }
  
}

