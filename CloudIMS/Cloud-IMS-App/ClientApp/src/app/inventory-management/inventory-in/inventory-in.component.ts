import { Component, OnInit } from '@angular/core';
import { IInventoryInTrxHeader } from '../../classes/inventory-management/InventoryIn/IInventoryInTrxHeader.interface';
import { IInventoryInTrxHeaderClass } from '../../classes/inventory-management/InventoryIn/IInventoryInTrxHeaderClass';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { InventoryInService } from '../../services/InventoryIn.service';
import { Local } from 'protractor/built/driverProviders';
import { getLocaleDateTimeFormat, DatePipe } from '@angular/common';
import { ISupplier } from '../../classes/data-dictionary/Supplier/ISupplier.interface';
import { IiTemMaster } from '../../classes/data-dictionary/ItemMaster/IitemMaster.interface';
import { IUnitCode } from '../../classes/data-dictionary/UnitCode/IUnitCode.interface';
import { IInventoryInTrxDetail } from '../../classes/inventory-management/InventoryIn/IInventoryInTrxDetail.interface';
import { IInventoryInTrxDetailClass } from '../../classes/inventory-management/InventoryIn/IInventoryInTrxDetailClass';
import { IiTemMasterUnitJoinUnit } from '../../classes/data-dictionary/ItemMasterUnit/IitemMasterUnitJoinUnit.interface';
import { IiTemMasterUnit } from '../../classes/data-dictionary/ItemMasterUnit/IitemMasterUnit.interface';

@Component({
  selector: 'app-inventory-in',
  templateUrl: './inventory-in.component.html'
 
})

export class InventoryInComponent implements OnInit{

  iinv_in_trx_hdr: IInventoryInTrxHeader = new IInventoryInTrxHeaderClass();
  iinv_in_trx_dtl: IInventoryInTrxDetail = new IInventoryInTrxDetailClass();
  addInventoryInFormGroup: FormGroup;
  trxDate: Date;
  Isupplier: ISupplier[];
  Iitemmaster: IiTemMaster[];
  Iunitcode: IUnitCode[];
  ItemMasterUnitArray: IiTemMasterUnitJoinUnit[];
  conversionFactor: number;

  constructor(private inv_in_service: InventoryInService, private toastr: ToastrService, private builder: FormBuilder) {
    this.CreateForm();
  
  }
  ngOnInit() {
    this.loadDataFromDictionary()
   
    let dt = new Date();
    let DayMonthYear = dt.toISOString().slice(0, 10)
    let TimeNow = dt.toTimeString().slice(0,5)
    let CompleteDate = DayMonthYear + "T" + TimeNow

    this.addInventoryInFormGroup.controls.trxDate_hdr.setValue(CompleteDate);
    this.addInventoryInFormGroup.controls.rcvdDate_hdr.setValue(CompleteDate);
    this.addInventoryInFormGroup.controls.expDate_dtl.setValue(CompleteDate);

    let timenow = dt.getTime().toString()
    let fullyearnow = dt.getFullYear()
    let datenow = dt.getDate()
    this.addInventoryInFormGroup.controls.trxNo_hdr.setValue("T" + fullyearnow + datenow + timenow);

  }
  private CreateForm() {
    this.addInventoryInFormGroup = this.builder.group({
      trxNo_hdr: [''],
      trxDate_hdr: [''],
      rcvdDate_hdr: [''],
      rcvdBy_hdr: [''],
      PONo_hdr: [''],
      invoiceNo_hdr: [''],
      refNo_hdr: [''],
      docNo_hdr: [''],
      supplier_hdr: [''],
      remarks_hdr: [''],

      itemMaster_dtl: [''],
      itemUnit_dtl: [''],
      quantity_dtl: [''],
      lotno_dtl: [''],
      expDate_dtl: [''],
      count_dtl: [''],
      remainingcount_dtl: [''],

    })
  }
  private insertInventoryInTrx() {
    let errormessage = "Error";
    this.iinv_in_trx_hdr.transactionNo = this.addInventoryInFormGroup.controls.trxNo_hdr.value;
    //this.iinv_in_trx_hdr.transactionDate = this.addInventoryInFormGroup.controls.trxDate_hdr.value;
    this.iinv_in_trx_hdr.receivedDate = this.addInventoryInFormGroup.controls.rcvdDate_hdr.value;
    this.iinv_in_trx_hdr.receivedBy = this.addInventoryInFormGroup.controls.rcvdBy_hdr.value;
    this.iinv_in_trx_hdr.poNumber = this.addInventoryInFormGroup.controls.PONo_hdr.value;
    this.iinv_in_trx_hdr.invoiceNo = this.addInventoryInFormGroup.controls.invoiceNo_hdr.value;
    this.iinv_in_trx_hdr.referenceNo = this.addInventoryInFormGroup.controls.refNo_hdr.value;
    this.iinv_in_trx_hdr.documnetNo = this.addInventoryInFormGroup.controls.docNo_hdr.value;
    this.iinv_in_trx_hdr.supplier = this.addInventoryInFormGroup.controls.supplier_hdr.value;
    this.iinv_in_trx_hdr.remarks = this.addInventoryInFormGroup.controls.remarks_hdr.value;

    this.iinv_in_trx_dtl.transactionNo = this.addInventoryInFormGroup.controls.trxNo_hdr.value;
    this.iinv_in_trx_dtl.itemID = this.addInventoryInFormGroup.controls.itemMaster_dtl.value;
    this.iinv_in_trx_dtl.unit = this.addInventoryInFormGroup.controls.itemUnit_dtl.value;
    this.iinv_in_trx_dtl.quantity = this.addInventoryInFormGroup.controls.quantity_dtl.value;
    this.iinv_in_trx_dtl.lotNumber = this.addInventoryInFormGroup.controls.lotno_dtl.value;
    this.iinv_in_trx_dtl.expirationDate = this.addInventoryInFormGroup.controls.expDate_dtl.value;
    this.iinv_in_trx_dtl.count = this.addInventoryInFormGroup.controls.count_dtl.value;
    this.iinv_in_trx_dtl.remainigCount = this.addInventoryInFormGroup.controls.remainingcount_dtl.value;

    this.inv_in_service.insertInventoryInTrxHeader(this.iinv_in_trx_hdr).subscribe(data => {

      this.toastr.success("Data Saved", "Saved");
      
    },
      error => {
        errormessage = error.error;
        this.toastr.error(errormessage, "Error");
      });

    this.inv_in_service.insertInventoryInTrxDetails(this.iinv_in_trx_dtl).subscribe(data => {
      this.toastr.success("Data Saved", "Saved");
    },
      error => {
        errormessage = error.error;
        this.toastr.error(errormessage, "Error");
      })
    
  }

  private loadDataFromDictionary()
  {
    this.inv_in_service.getItemMasterData().subscribe((itemmaster)=> this.Iitemmaster= itemmaster)
    //this.inv_in_service.getUnitCodeData().subscribe((unitcode)=> this.Iunitcode= unitcode)
    this.inv_in_service.getSupplierData().subscribe((supplier)=> this.Isupplier= supplier)
  }

  private onSelectedItemMaster(value: string) {
    this.inv_in_service.getItemasterUnit(value).subscribe((itemmaster) => this.ItemMasterUnitArray = itemmaster);
   
   
  }

  private onSelectCoversionFactor(itemMasterId, itemMasterUnitId) {
    this.inv_in_service.getItemasterUnitConversionFactor(itemMasterId, itemMasterUnitId).subscribe((itemmasterunit) => this.ItemMasterUnitArray = itemmasterunit)
    this.ItemMasterUnitArray.forEach((ua) => {
      var cnvfactor = Number(ua.itemMasterUnitConversion)
      this.conversionFactor = cnvfactor
      console.log(cnvfactor);
    })
  }
  private onTextChangedQuantityInput() {
   
    this.addInventoryInFormGroup.controls.count_dtl.setValue(this.conversionFactor);
    this.addInventoryInFormGroup.controls.remainingcount_dtl.setValue(this.conversionFactor);
  }
}
