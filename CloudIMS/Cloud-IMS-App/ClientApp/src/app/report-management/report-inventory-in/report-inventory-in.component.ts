import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ReportInventoryInService } from '../../services/ReportInventoryIn.service';
import { IiTemMaster } from '../../classes/data-dictionary/ItemMaster/IitemMaster.interface';
import { ISupplier } from '../../classes/data-dictionary/Supplier/ISupplier.interface';
import { IUnitCode } from '../../classes/data-dictionary/UnitCode/IUnitCode.interface';
import { IReportInventoryIn } from '../../classes/report-management/report-inventory-in/IReportInventoryIn.interface';
import * as XLSX from 'xlsx';
import * as FileSaver from 'file-saver';
import { IReportInventoryInClass } from '../../classes/report-management/report-inventory-in/IReportInventoryInClass';

const EXCEL_TYPE = 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;charset=UTF-8';
const EXCEL_EXTENSION = '.xlsx';

@Component({
  selector: 'app-report-inventory-in',
  templateUrl: './report-inventory-in.component.html'
})
export class ReportInventoryInComponent implements OnInit {

  rptInvInFormGroup: FormGroup;
  itemasterArray: IiTemMaster[];
  itemUnitCodeArray: IUnitCode[];
  supplierArray: ISupplier[];
  rptInvInArray: IReportInventoryIn[];

  /*name of the excel-file which will be downloaded. */
  fileName = 'ExcelSheet.xlsx';

  constructor(public builder: FormBuilder,public rptservice: ReportInventoryInService) {
    this.createForm();  
  }

  ngOnInit() {
    this.getDateTimeNow();
    this.loadAllData();
  }

  createForm() {
    this.rptInvInFormGroup = this.builder.group({
      supplier_hdr: [''],
      itemMaster_dtl: [''],
      itemUnit_dtl: [''],
      from_DT: [''],
      To_DT: [''],
    })
  }

  getDateTimeNow() {
    let dt = new Date();
    let DayMonthYear = dt.toISOString().slice(0, 10)
    let TimeNow = dt.toTimeString().slice(0, 5)
    let CompleteDate = DayMonthYear + "T" + TimeNow

    this.rptInvInFormGroup.controls.from_DT.setValue(CompleteDate)
    this.rptInvInFormGroup.controls.To_DT.setValue(CompleteDate)
    
  }

  loadAllData() {
    this.rptservice.getItemMasterData().subscribe((itemaster)=> this.itemasterArray =itemaster)
    this.rptservice.getUnitCodeData().subscribe((itemunit) => this.itemUnitCodeArray = itemunit)
    this.rptservice.getSupplierData().subscribe((supplier) => this.supplierArray = supplier)


  }

  async generateReportInvIn() {
    let fromDT = new Date();
    let toDT = new Date();
    fromDT = this.rptInvInFormGroup.controls.from_DT.value;
    toDT = this.rptInvInFormGroup.controls.To_DT.value;
    let itemID = document.getElementById(this.rptInvInFormGroup.controls.itemMaster_dtl.value).innerText;
    let itemunitID = document.getElementById(this.rptInvInFormGroup.controls.itemUnit_dtl.value).innerText;
    let supID = document.getElementById(this.rptInvInFormGroup.controls.supplier_hdr.value).innerText;

    
    await this.rptservice.getReportInventoryIn(itemID, itemunitID, supID, fromDT, toDT)
      .toPromise().then((getreport) => {
        this.rptInvInArray = getreport;
      this.ExportFile();
      })
    //this.exportAsExcelFile(this.rptInvInArray, 'Report Inventory In');
   
  }

  ExportFile() {
    /* table id is passed over here */
    let element = document.getElementById('rptInvInTable');
    const ws: XLSX.WorkSheet = XLSX.utils.table_to_sheet(element);
   
    /* generate workbook and add the worksheet */
    const wb: XLSX.WorkBook = XLSX.utils.book_new();
  
    XLSX.utils.book_append_sheet(wb, ws, 'Sheet1');

    /* save to file */
    XLSX.writeFile(wb, this.fileName);
  }


  public exportAsExcelFile(json: any[], excelFileName: string): void {
    const worksheet: XLSX.WorkSheet = XLSX.utils.json_to_sheet(json);
    const workbook: XLSX.WorkBook = { Sheets: { 'data': worksheet }, SheetNames: ['data'] };
    const excelBuffer: any = XLSX.write(workbook, { bookType: 'xlsx', type: 'array' });
    this.saveAsExcelFile(excelBuffer, excelFileName);
  }
  public saveAsExcelFile(buffer: any, fileName: string): void {
    const data: Blob = new Blob([buffer], { type: EXCEL_TYPE });
    FileSaver.saveAs(data, fileName + '_export_' + new Date().getTime() + EXCEL_EXTENSION);
  }
}
