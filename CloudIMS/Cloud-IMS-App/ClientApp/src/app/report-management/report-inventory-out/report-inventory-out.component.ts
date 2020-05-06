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

  formValue: IReportInventoryOut = new IReportInventoryOutClass();
  reportOutArray: IReportInventoryOut[];

  constructor(public builder: FormBuilder, public reportServices: ReportInventoryOutService,
  public datePipe: DatePipe) {
    this.createForm()
  }

  ngOnInit() {
    this.loadData();
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
    this.invInReports.get('dateFrom').patchValue(this.getDateTimeNow());
    this.invInReports.get('dateTo').patchValue(this.getDateTimeNow());

    this.reportServices.getItem().subscribe((data) => this.itemArray = data);
    this.reportServices.getUnit().subscribe((data) => this.unitArray = data);
    this.reportServices.getDepartment().subscribe((data) => this.depArray = data);
  }

  public getDateTimeNow(): string {
    let today = new Date();
    let getDate = today.toISOString().slice(0, 10);
    let cutTime = today.toTimeString().slice(0, 5);
    let dateTime = getDate + "T" + cutTime;
    return dateTime;
  
  }

  public getFormValue() {
    this.formValue.HeaderTransactionNo = this.invInReports.controls.TransactionNumber.value;

    let itemID = this.invInReports.controls.ItemName.value;
    if (itemID != "") {
      this.formValue.itemID = document.getElementById(itemID).innerHTML;
    } else {
      this.formValue.itemID = "%";
    }

    let unitcode = this.invInReports.controls.ItemUnit.value;
    if (unitcode != "") {
      this.formValue.unit = document.getElementById(unitcode).innerHTML;
    } else {
      this.formValue.unit = "%";
    }

    let department = this.invInReports.controls.department.value;
    if (department != "") {
      this.formValue.department = document.getElementById(department).innerHTML;
    } else {
      this.formValue.department = "%";
    }

    this.formValue.transactionDateFrom = this.invInReports.controls.dateFrom.value;
    this.formValue.transactionDateTo = this.invInReports.controls.dateTo.value;

    return this.formValue;
  }

  public generateReports() {
   

    this.reportServices.getReport(this.getFormValue().HeaderTransactionNo, this.getFormValue().itemID, this.getFormValue().unit,
      this.getFormValue().department, this.getFormValue().transactionDateFrom, this.getFormValue().transactionDateTo,
      this.invInReports.controls.reportType.value).subscribe((data) => {
        this.reportOutArray = data;
        this.exportReport();
      });
  }

  exportReport() {
  /* table id is passed over here */
      const ws: XLSX.WorkSheet = XLSX.utils.json_to_sheet(this.reportOutArray);

      /* generate workbook and add the worksheet */
      const wb: XLSX.WorkBook = XLSX.utils.book_new();

      XLSX.utils.book_append_sheet(wb, ws, 'Sheet1');


      /* save to file */
    let dateFrom = this.datePipe.transform(this.formValue.transactionDateFrom, 'MMMM d, y');
    let dateTo = this.datePipe.transform(this.formValue.transactionDateTo, 'MMMM d, y');
      XLSX.writeFile(wb,"Inventory Out For" + " " +dateFrom + " " + "to" + " " + dateTo + ".xlsx");
    }
  
}

