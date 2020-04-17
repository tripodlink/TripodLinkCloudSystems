import { Component, OnInit } from '@angular/core';
import { IInventoryInTrxHeader } from '../../classes/inventory-management/InventoryIn/IInventoryInTrxHeader.interface';
import { IInventoryInTrxHeaderClass } from '../../classes/inventory-management/InventoryIn/IInventoryInTrxHeaderClass';
import { FormGroup, FormBuilder, Validators, FormArray, Form } from '@angular/forms';
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
import { IitemMasterUnitJoinUnitClass } from '../../classes/data-dictionary/ItemMasterUnit/IitemMasterJoinUnitClass';
import { IitemMasterUnitConversionFactor } from '../../classes/data-dictionary/ItemMasterUnit/IitemMasterUnitConversionFactor.interface';
import { parse } from 'querystring';

@Component({
  selector: 'app-inventory-in',
  templateUrl: './inventory-in.component.html'
 
})

export class InventoryInComponent {

  iinv_in_trx_hdr: IInventoryInTrxHeader = new IInventoryInTrxHeaderClass();
  iinv_in_trx_dtl: IInventoryInTrxDetail = new IInventoryInTrxDetailClass();
  addInventoryInFormGroup: FormGroup;
  trxDate: Date;
  Isupplier: ISupplier[];
  Iitemmaster: IiTemMaster[];
  Iunitcode: IUnitCode[];
  ItemMasterUnitArray: IiTemMasterUnitJoinUnit[];
  ItemMasterUnitArray_convfactor: IiTemMasterUnitJoinUnit[];
  ItemMasterUnitArray_convfactor_Arr: IitemMasterUnitJoinUnitClass[] = new Array();
  conversionFactor: number;

  isReadonly_UC: boolean;
  isReadonly_QTY: boolean;


  constructor(private inv_in_service: InventoryInService, private toastr: ToastrService, private builder: FormBuilder) {
    this.CreateForm();
    
  }
  ngOnInit() {

   

    this.enabledUnitCode();
    this.loadDataFromDictionary()
    this.ItemMasterUnitArray_convfactor;

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

     cnvfactor: ['']

      

    })
  }
  private insertInventoryInTrx() {
    let errormessage = "Error";


    let trxnohdr = this.addInventoryInFormGroup.controls.trxNo_hdr.value;
    let rcvddate = this.addInventoryInFormGroup.controls.rcvdDate_hdr.value;
    let rcvdby = this.addInventoryInFormGroup.controls.rcvdBy_hdr.value;
    let ponumber = this.addInventoryInFormGroup.controls.PONo_hdr.value;
    let invcno = this.addInventoryInFormGroup.controls.invoiceNo_hdr.value;
    let refno = this.addInventoryInFormGroup.controls.refNo_hdr.value;
    let docno = this.addInventoryInFormGroup.controls.docNo_hdr.value;
    let sup = this.addInventoryInFormGroup.controls.supplier_hdr.value;
    let remarks = this.addInventoryInFormGroup.controls.remarks_hdr.value;

    let trxnodtl = this.addInventoryInFormGroup.controls.trxNo_hdr.value;
    let itemid = this.addInventoryInFormGroup.controls.itemMaster_dtl.value;
    let unit = this.addInventoryInFormGroup.controls.itemUnit_dtl.value;
    let qty = this.addInventoryInFormGroup.controls.quantity_dtl.value;
    let lotno = this.addInventoryInFormGroup.controls.lotno_dtl.value;
    let expdate = this.addInventoryInFormGroup.controls.expDate_dtl.value;
    let count = this.addInventoryInFormGroup.controls.count_dtl.value;
    let remainingcount = this.addInventoryInFormGroup.controls.remainingcount_dtl.value;

    this.iinv_in_trx_hdr.transactionNo = trxnohdr
    this.iinv_in_trx_hdr.receivedDate = rcvddate
    this.iinv_in_trx_hdr.receivedBy = rcvdby
    this.iinv_in_trx_hdr.poNumber = ponumber
    this.iinv_in_trx_hdr.invoiceNo = invcno
    this.iinv_in_trx_hdr.referenceNo = refno
    this.iinv_in_trx_hdr.documnetNo = docno
    this.iinv_in_trx_hdr.supplier = sup
    this.iinv_in_trx_hdr.remarks = remarks

    this.iinv_in_trx_dtl.transactionNo = trxnodtl
    this.iinv_in_trx_dtl.itemID = itemid
    this.iinv_in_trx_dtl.unit = unit
    this.iinv_in_trx_dtl.quantity = qty
    this.iinv_in_trx_dtl.lotNumber = lotno
    this.iinv_in_trx_dtl.expirationDate = expdate
    this.iinv_in_trx_dtl.count = count
    this.iinv_in_trx_dtl.remainigCount = remainingcount

    if (ponumber == '' || invcno == '' || rcvdby == '' || refno == '' || lotno == '' || itemid =='' ||  itemid == null || unit == null || qty == null ||
      ponumber == null || invcno == null || rcvdby == null || refno == null || lotno == null) {
      alert("please input on the fileds with red (*)")
    } else {

        this.inv_in_service.insertInventoryInTrxHeader(this.iinv_in_trx_hdr).subscribe(data => {

          this.toastr.success("Data Saved", "Saved");
          this.resetScreen()

        },
          error => {
            errormessage = error.error;
            this.toastr.error(errormessage, "Error");
            this.resetScreen()
          });
    

        this.inv_in_service.insertInventoryInTrxDetails(this.iinv_in_trx_dtl).subscribe(data => {
          this.toastr.success("Data Saved", "Saved");
          this.resetScreen()
        },
          error => {
            errormessage = error.error;
            this.toastr.error(errormessage, "Error");
            this.resetScreen()
          })

        this.resetScreen()
    
    }
  }

  private loadDataFromDictionary()
  {
    this.inv_in_service.getItemMasterData().subscribe((itemmaster)=> this.Iitemmaster= itemmaster)
    this.inv_in_service.getSupplierData().subscribe((supplier) => this.Isupplier = supplier)
  }

  private onSelectedItemMaster(value: string) :void {
    //this.isReadonly_UC = false;
    this.inv_in_service.getItemasterUnit(value).subscribe((itemmaster) => this.ItemMasterUnitArray = itemmaster);
   
    this.enabledUnitCode()
  }

  private onSelectCoversionFactor(itemMasterId: string, itemMasterUnitId: string) {
   
    this.inv_in_service.getItemasterUnitConversionFactor(itemMasterId, itemMasterUnitId).subscribe((itemmasterunit) => {
      this.ItemMasterUnitArray_convfactor = itemmasterunit;

      for (let i of this.ItemMasterUnitArray_convfactor) {
        this.conversionFactor = parseInt(i.itemMasterUnitConversion);
      }
    });
    this.enabledQuantity()
  }
  private onTextChangedQuantityInput() {

    this.iinv_in_trx_dtl.quantity = this.addInventoryInFormGroup.controls.quantity_dtl.value;
    let computefactor = this.iinv_in_trx_dtl.quantity * this.conversionFactor;
  
    this.addInventoryInFormGroup.controls.count_dtl.setValue(computefactor);
    this.addInventoryInFormGroup.controls.remainingcount_dtl.setValue(computefactor);
  }

  public resetScreen() {

    
    this.loadDataFromDictionary()
    this.addInventoryInFormGroup.reset();
    let dt = new Date();
    let DayMonthYear = dt.toISOString().slice(0, 10)
    let TimeNow = dt.toTimeString().slice(0, 5)
    let CompleteDate = DayMonthYear + "T" + TimeNow
    this.addInventoryInFormGroup.controls.trxDate_hdr.setValue(CompleteDate);
    this.addInventoryInFormGroup.controls.rcvdDate_hdr.setValue(CompleteDate);
    this.addInventoryInFormGroup.controls.expDate_dtl.setValue(CompleteDate);
    let timenow = dt.getTime().toString()
    let fullyearnow = dt.getFullYear()
    let datenow = dt.getDate()
    this.addInventoryInFormGroup.controls.trxNo_hdr.setValue("T" + fullyearnow + datenow + timenow);
    this.isReadonly_QTY = true;
    this.isReadonly_UC = true;
   
  }
  public enabledUnitCode() {
    if (this.addInventoryInFormGroup.controls.itemMaster_dtl.value == '') {
      this.isReadonly_UC = true;
      this.isReadonly_QTY = true;
      this.addInventoryInFormGroup.controls.itemUnit_dtl.reset();
      this.addInventoryInFormGroup.controls.quantity_dtl.reset();
      this.addInventoryInFormGroup.controls.count_dtl.reset();
      this.addInventoryInFormGroup.controls.remainingcount_dtl.reset();
    } else {
      this.isReadonly_UC = false;
    }
      
  }
  public enabledQuantity() {
    if (this.addInventoryInFormGroup.controls.itemUnit_dtl.value == '') {
      this.isReadonly_QTY = true;
      this.addInventoryInFormGroup.controls.quantity_dtl.reset();
      this.addInventoryInFormGroup.controls.count_dtl.reset();
      this.addInventoryInFormGroup.controls.remainingcount_dtl.reset();
    } else {
      this.isReadonly_QTY = false;
    }
  }
  
}
