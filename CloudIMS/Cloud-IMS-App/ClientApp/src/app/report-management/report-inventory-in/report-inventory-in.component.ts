import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ReportInventoryInService } from '../../services/ReportInventoryIn.service';
import { IiTemMaster } from '../../classes/data-dictionary/ItemMaster/IitemMaster.interface';
import { ISupplier } from '../../classes/data-dictionary/Supplier/ISupplier.interface';
import { IUnitCode } from '../../classes/data-dictionary/UnitCode/IUnitCode.interface';
import { IReportInventoryIn } from '../../classes/report-management/report-inventory-in/IReportInventoryIn.interface';

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

  public generateReportInvIn() {
    let fromDT = new Date();
    let toDT = new Date();
    fromDT = this.rptInvInFormGroup.controls.from_DT.value;
    toDT = this.rptInvInFormGroup.controls.To_DT.value;
    let itemID = document.getElementById(this.rptInvInFormGroup.controls.itemMaster_dtl.value).innerText;
    let itemunitID = document.getElementById(this.rptInvInFormGroup.controls.itemUnit_dtl.value).innerText;
    let supID = document.getElementById(this.rptInvInFormGroup.controls.supplier_hdr.value).innerText;

    
    this.rptservice.getReportInventoryIn(itemID, itemunitID, supID,fromDT,toDT).subscribe((getreport)=>this.rptInvInArray = getreport)
  }
}
