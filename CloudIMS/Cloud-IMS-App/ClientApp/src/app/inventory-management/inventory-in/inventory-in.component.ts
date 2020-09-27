import { Component, OnDestroy, OnInit, ViewChild } from "@angular/core";
import { IInventoryInTrxHeader, IListOfUserAccount } from "../../classes/inventory-management/InventoryIn/IInventoryInTrxHeader.interface";
import { IInventoryInTrxHeaderClass } from "../../classes/inventory-management/InventoryIn/IInventoryInTrxHeaderClass";
import { FormGroup, FormBuilder } from "@angular/forms";
import { ToastrService } from "ngx-toastr";
import { InventoryInService } from "../../services/InventoryIn.service";
import { DatePipe, formatDate } from "@angular/common";
import { ISupplier } from "../../classes/data-dictionary/Supplier/ISupplier.interface";
import { IiTemMaster } from "../../classes/data-dictionary/ItemMaster/IitemMaster.interface";
import { IUnitCode } from "../../classes/data-dictionary/UnitCode/IUnitCode.interface";
import {
  IInventoryInTrxDetail,
  IInventoryInTrxList,
} from "../../classes/inventory-management/InventoryIn/IInventoryInTrxDetail.interface";
import { IInventoryInTrxDetailClass } from "../../classes/inventory-management/InventoryIn/IInventoryInTrxDetailClass";
import { IiTemMasterUnitJoinUnit } from "../../classes/data-dictionary/ItemMasterUnit/IitemMasterUnitJoinUnit.interface";
import { IitemMasterUnitJoinUnitClass } from "../../classes/data-dictionary/ItemMasterUnit/IitemMasterJoinUnitClass";
import { UserAuthorizationService } from "../../services/UserAuthorization.service";
import * as XLSX from "xlsx";
import { CookieService } from "ngx-cookie-service";
import { truncate } from "fs";
import "rxjs/add/operator/map";
import { Http, Response } from "@angular/http";
import { Subject } from "rxjs";
import "rxjs/add/operator/map";
import { DataTablesModule } from 'angular-datatables';
import Swal from 'sweetalert2/dist/sweetalert2.js';


@Component({
  selector: "app-inventory-in",
  templateUrl: "./inventory-in.component.html",
})
export class InventoryInComponent implements OnInit, OnDestroy {
  userIDLogin: string;
  excelUpload: ExcelUploadTable[] = [];
  arrayBuffer: any;
  user: string;
  iinv_in_trx_hdr: IInventoryInTrxHeader = new IInventoryInTrxHeaderClass();
  iinv_in_trx_dtl: IInventoryInTrxDetail = new IInventoryInTrxDetailClass();
  iinv_in_trx_list: IInventoryInTrxList[];
  addInventoryInFormGroup: FormGroup;
  addInventoryInFormGroupByBatch: FormGroup;
  trxDate: Date;
  Isupplier: ISupplier[];
  supplierSelectList = [];
  itemasterSelectList = [];
  itemmasteunitSelectList = [];
  Iitemmaster: IiTemMaster[];
  Iunitcode: IUnitCode[];
  ItemMasterUnitArray: IiTemMasterUnitJoinUnit[];
  ItemMasterUnitArray_convfactor: IiTemMasterUnitJoinUnit[];
  ItemMasterUnitArray_convfactor_Arr: IitemMasterUnitJoinUnitClass[] = new Array();
  IlistOfUsers: IListOfUserAccount[];

  conversionFactor: number;

  isReadonly_UC: boolean;
  isReadonly_QTY: boolean;
  isReadonly_LookUp_Btn_ItemUnit: boolean;

  itemmasterId_doc: string;
  itemmasterUnit_doc: string;

  trxNumFunction: string;
  fileName = "ExcelSheet.xlsx";

  isForInvIn: boolean;
  isExpirable: boolean;

  @ViewChild(DataTablesModule, { static: false })
  datatableElement: DataTablesModule;
  min: number;
  max: number;
  dtOptions: DataTables.Settings = {};
  dtTrigger: Subject<IInventoryInTrxList> = new Subject();
  constructor(
    public http: Http,
    public inv_in_service: InventoryInService,
    public cookieService: CookieService,
    public toastr: ToastrService,
    public builder: FormBuilder,
    public auth: UserAuthorizationService,
    public datepipe: DatePipe
  ) {
    this.CreateForm();
  }
 

  ngOnInit() {
    this.addInventoryInFormGroupByBatch.controls.rcvdBy_hdr_by_batch.setValue(
      this.userIDLogin
    );
    this.enabledUnitCode();
    this.loadDataFromDictionary();
    this.ItemMasterUnitArray_convfactor;
    this.loadTrxListInvIn();
    this.getListUser()
    this.getDateTimeNow();
    this.addInventoryInFormGroup.controls.trxNo_hdr.setValue("*********************************");
    this.addInventoryInFormGroupByBatch.controls.trxNo_hdr_by_batch.setValue("*********************************");
    this.isForInvIn = true;
    this.isExpirable = true;
  }
  
  ngOnDestroy(): void {
    // Do not forget to unsubscribe the event
    this.dtTrigger.unsubscribe();
  }
  private extractData(res: Response) {
    const body = res.json();
    return body.data || {};
  }
  checkExpire() {
    if (this.isExpirable == false) {
      this.isExpirable = true;
    } else {
      this.isExpirable = false;
    }
  }
  async loadTrxListInvIn() {
    await this.inv_in_service.getTrxListInvIn().toPromise().then((invInTrxList) => {
      this.iinv_in_trx_list = [];
        this.iinv_in_trx_list = invInTrxList;

        this.dtOptions = {
          pagingType: 'full_numbers',
          pageLength: 1,
          destroy: true,
          retrieve: true,
          responsive: true,
        };
      this.dtTrigger.next();
      console.log(this.iinv_in_trx_list)

      });
  }
  async getTrxInvIn(trxno: string) {
    this.isForInvIn = false;
    let item_id: string;
    let item_unit_id: string;

    await this.inv_in_service.getTrxListInvIn().toPromise().then((invInTrxList) => {
        this.iinv_in_trx_list = invInTrxList;

        for (let invtl of this.iinv_in_trx_list) {
          if (invtl.transactionNo == trxno) {
            // hdr
            this.addInventoryInFormGroup.controls.trxNo_hdr.setValue(invtl.transactionNo);
            this.addInventoryInFormGroup.controls.trxDate_hdr.setValue(invtl.transactionDate);
            this.addInventoryInFormGroup.controls.rcvdDate_hdr.setValue(invtl.receivedDate);
            this.addInventoryInFormGroup.controls.rcvdBy_hdr.setValue(invtl.receivedBy);
            this.addInventoryInFormGroup.controls.PONo_hdr.setValue(invtl.poNumber);
            this.addInventoryInFormGroup.controls.invoiceNo_hdr.setValue(invtl.invoiceNo);
            this.addInventoryInFormGroup.controls.refNo_hdr.setValue(invtl.referenceNo);
            this.addInventoryInFormGroup.controls.docNo_hdr.setValue(invtl.documnetNo);
            this.addInventoryInFormGroup.controls.supplier_hdr.setValue(invtl.supplierName);
            this.addInventoryInFormGroup.controls.remarks_hdr.setValue(invtl.remarks);
            // dtl
            item_id = invtl.itemID;
            item_unit_id = invtl.unit;

            this.enabledLookUpButtonItemUnit();
            this.enabledUnitCode();
            this.enabledQuantity();

            this.itemMasterSelectLookUp(item_id);
            this.itemMasterUnitSelectLookup(item_unit_id);

            this.addInventoryInFormGroup.controls.itemMaster_dtl.setValue(invtl.itemName);
            this.addInventoryInFormGroup.controls.itemUnit_dtl.setValue(invtl.unitName);
            this.addInventoryInFormGroup.controls.quantity_dtl.setValue(invtl.quantity);
            this.addInventoryInFormGroup.controls.lotno_dtl.setValue(invtl.lotNumber);

            let dtstr = "0001-01-01" + "T" + "00:00:00";
            let dtdefault = new Date(dtstr);
            let dtfromdbase = new Date(invtl.expirationDate);

            let DayMonthYear1 = dtdefault.toISOString().slice(0, 10);
            let TimeNow1 = dtdefault.toTimeString().slice(0, 5);
            let CompleteDate1 = DayMonthYear1 + "T" + TimeNow1;

            let DayMonthYear2 = dtfromdbase.toISOString().slice(0, 10);
            let TimeNow2 = dtfromdbase.toTimeString().slice(0, 5);
            let CompleteDate2 = DayMonthYear2 + "T" + TimeNow2;

            if (CompleteDate1 == CompleteDate2) {
              this.isExpirable = false;
              this.addInventoryInFormGroup.controls.checkbox_expdate.setValue(true);
            }
            else {
              this.isExpirable = true;
              this.addInventoryInFormGroup.controls.checkbox_expdate.setValue(false);
            }
            this.addInventoryInFormGroup.controls.expDate_dtl.setValue(invtl.expirationDate);
            this.addInventoryInFormGroup.controls.count_dtl.setValue(invtl.count);
            this.addInventoryInFormGroup.controls.remainingcount_dtl.setValue(invtl.remainigCount);

           
          }
        }
      });
  }

  async updateInventoryInTrx() {
    // hdr
    this.iinv_in_trx_hdr.transactionNo = this.addInventoryInFormGroup.controls.trxNo_hdr.value;
    this.iinv_in_trx_hdr.transactionDate = this.addInventoryInFormGroup.controls.trxDate_hdr.value;
    this.iinv_in_trx_hdr.receivedDate = this.addInventoryInFormGroup.controls.rcvdDate_hdr.value;
    this.iinv_in_trx_hdr.poNumber = this.addInventoryInFormGroup.controls.PONo_hdr.value;
    this.iinv_in_trx_hdr.invoiceNo = this.addInventoryInFormGroup.controls.invoiceNo_hdr.value;
    this.iinv_in_trx_hdr.referenceNo = this.addInventoryInFormGroup.controls.refNo_hdr.value;
    this.iinv_in_trx_hdr.documnetNo = this.addInventoryInFormGroup.controls.docNo_hdr.value;

    let rcvdby;
    try {
      rcvdby = document.getElementById(this.addInventoryInFormGroup.controls.rcvdBy_hdr.value).innerText;
    } catch{ }
    this.iinv_in_trx_hdr.receivedBy = rcvdby;

    let sup;
    try {
      sup = document.getElementById(this.addInventoryInFormGroup.controls.supplier_hdr.value).innerText;
    } catch {}

    this.iinv_in_trx_hdr.supplier = sup;
    this.iinv_in_trx_hdr.remarks = this.addInventoryInFormGroup.controls.remarks_hdr.value;

    // dtl
    this.iinv_in_trx_dtl.transactionNo = this.addInventoryInFormGroup.controls.trxNo_hdr.value;

    let imid;
    try {
      imid = document.getElementById(this.addInventoryInFormGroup.controls.itemMaster_dtl.value).innerText;
    } catch {}
    this.iinv_in_trx_dtl.itemID = imid;

    let unitid;
    try {
      unitid = document.getElementById(this.addInventoryInFormGroup.controls.itemUnit_dtl.value).innerText;
    } catch {}
    this.iinv_in_trx_dtl.unit = unitid;

    this.iinv_in_trx_dtl.quantity = this.addInventoryInFormGroup.controls.quantity_dtl.value;
    this.iinv_in_trx_dtl.lotNumber = this.addInventoryInFormGroup.controls.lotno_dtl.value;

    if (this.isExpirable == false) {
      let CompleteDate = "0001-01-01" + "T" + "00:00:00";

      this.addInventoryInFormGroup.controls.expDate_dtl.setValue(CompleteDate);
      this.iinv_in_trx_dtl.expirationDate = this.addInventoryInFormGroup.controls.expDate_dtl.value;
    } else {
      this.iinv_in_trx_dtl.expirationDate = this.addInventoryInFormGroup.controls.expDate_dtl.value;
    }

    this.iinv_in_trx_dtl.count = this.addInventoryInFormGroup.controls.count_dtl.value;
    this.iinv_in_trx_dtl.remainigCount = this.addInventoryInFormGroup.controls.remainingcount_dtl.value;

    let errormessage = "Error";
    this.inv_in_service.updateInventoryInTrxHeader(this.iinv_in_trx_hdr).subscribe(
        (data) => {},

        (error) => {
          errormessage = error.error;
          this.toastr.error(errormessage, "Error");
        }
      );

    this.inv_in_service.updateInventoryInTrxDetails(this.iinv_in_trx_dtl).subscribe(
        (data) => {},

        (error) => {
          errormessage = error.error;
          this.toastr.error(errormessage, "Error");
        }
      );

    this.toastr.info("Data Successfully Updated", "Edited");
  }

  deleteInventoryInTrx() {
    let trxno = this.addInventoryInFormGroup.controls.trxNo_hdr.value;
   
      let errormessage = "Error";
      Swal.fire({
        title: 'Are you sure do you want to delete this Transaction ' + trxno + ' ?' , text: 'You will not be able to recover this Transaction '+ trxno, icon: 'warning', showCancelButton: true,
        confirmButtonText: 'Yes', cancelButtonText: 'Cancel',
        closeOnClickOutside: false,
        closeOnEsc: false,
        allowOutsideClick: false,

      }).then((result) => {
        if (result.value) {
          this.inv_in_service.deleteInventoryInTrxHeader(trxno).subscribe((data) => { },
            (error) => {
              errormessage = error.error;
              this.toastr.error(errormessage, "Error");
            }
          );
          this.inv_in_service.deleteInventoryInTrxDetails(trxno).subscribe((data) => { },
            (error) => {
              errormessage = error.error;
              this.toastr.error(errormessage, "Error");
            });
          Swal.fire('Deleted!', 'Transaction has been deleted.', 'success')
          document.getElementById(trxno).remove();
          this.resetScreen();

        } else if (result.dismiss === Swal.DismissReason.cancel) {

          Swal.fire('Cancelled', 'Your data is safe!', 'error')
        }
      })


     
    
  }
  async CreateAutoGeneratedID() {
    await this.inv_in_service
      .getTrxNumFunction()
      .toPromise()
      .then((result) => {
        this.trxNumFunction = result;

        let trxNum = this.trxNumFunction.split("|");
        let type = trxNum[0];
        let year = trxNum[1];
        let convertYear = this.datepipe.transform(
          new Date(),
          year.toLowerCase()
        );
        let num = trxNum[2];

        this.addInventoryInFormGroup.controls.trxNo_hdr.setValue(
          type + convertYear + num
        );
      });
  }

  public getDateTimeNow() {
    let dt = new Date();
    let DayMonthYear = dt.toISOString().slice(0, 10);
    let TimeNow = dt.toTimeString().slice(0, 5);
    let CompleteDate = DayMonthYear + "T" + TimeNow;

    this.addInventoryInFormGroup.controls.trxDate_hdr.setValue(CompleteDate);
    this.addInventoryInFormGroup.controls.rcvdDate_hdr.setValue(CompleteDate);
    this.addInventoryInFormGroup.controls.expDate_dtl.setValue(CompleteDate);

    this.addInventoryInFormGroupByBatch.controls.trxDate_hdr_by_batch.setValue(CompleteDate);
  }
  async getListUser() {
    await this.inv_in_service.getListOfUsers().toPromise().then((loua) => {
      this.IlistOfUsers = loua;
      for (let ua of this.IlistOfUsers) {
        if (ua.userID == this.cookieService.get("userId")) {
          this.addInventoryInFormGroup.controls.rcvdBy_hdr.setValue(ua.userName);
        }
      }
    });
  }

  async getListOfUserLookUp(value : string) {

    await this.inv_in_service.getListOfUsers().toPromise().then((loua) => {
      this.IlistOfUsers = loua;
      for (let ua of this.IlistOfUsers) {
        if (ua.userID == value) {
          this.addInventoryInFormGroup.controls.rcvdBy_hdr.setValue(ua.userName);
        }
      }
    });
  }

  public CreateForm() {
    this.addInventoryInFormGroup = this.builder.group({
      trxNo_hdr: [""],
      trxDate_hdr: [""],
      rcvdDate_hdr: [""],
      rcvdBy_hdr: [""],
      PONo_hdr: [""],
      invoiceNo_hdr: [""],
      refNo_hdr: [""],
      docNo_hdr: [""],
      supplier_hdr: [""],
      remarks_hdr: [""],

      itemMaster_dtl: [""],
      itemUnit_dtl: [""],
      quantity_dtl: [""],
      lotno_dtl: [""],
      expDate_dtl: [""],
      NotApplicable: [""],
      count_dtl: [""],
      remainingcount_dtl: [""],

      cnvfactor: [""],
      checkbox_expdate: [""],
    });
    this.addInventoryInFormGroupByBatch = this.builder.group({
      trxNo_hdr_by_batch: [""],
      trxDate_hdr_by_batch: [""],
      rcvdBy_hdr_by_batch: [""],
    });
  }
  async insertInventoryInTrx() {
    let errormessage = "Error";

    console.log("testing");
    let trxdt = this.addInventoryInFormGroup.controls.trxDate_hdr.value;
    let rcvddate = this.addInventoryInFormGroup.controls.rcvdDate_hdr.value;
    let rcvdby;
    try {
      rcvdby = document.getElementById(this.addInventoryInFormGroup.controls.rcvdBy_hdr.value).innerText;
    } catch{}
    let ponumber = this.addInventoryInFormGroup.controls.PONo_hdr.value;
    let invcno = this.addInventoryInFormGroup.controls.invoiceNo_hdr.value;
    let refno = this.addInventoryInFormGroup.controls.refNo_hdr.value;
    let docno = this.addInventoryInFormGroup.controls.docNo_hdr.value;
    let sup;

    try {
      sup = document.getElementById(this.addInventoryInFormGroup.controls.supplier_hdr.value).innerText;
    } catch { }

    let remarks = this.addInventoryInFormGroup.controls.remarks_hdr.value;

    let itemid = this.itemmasterId_doc;
    let unit = this.itemmasterUnit_doc;
    let qty = this.addInventoryInFormGroup.controls.quantity_dtl.value;
    let lotno = this.addInventoryInFormGroup.controls.lotno_dtl.value;

    let expdate;
    if (this.addInventoryInFormGroup.controls.checkbox_expdate.value == true) {
      let CompleteDate = "0001-01-01" + "T" + "00:00:00";
      this.addInventoryInFormGroup.controls.expDate_dtl.setValue(CompleteDate);
      expdate = this.addInventoryInFormGroup.controls.expDate_dtl.value;
    } else {
      expdate = this.addInventoryInFormGroup.controls.expDate_dtl.value;
    }

    let count = this.addInventoryInFormGroup.controls.count_dtl.value;
    let remainingcount = this.addInventoryInFormGroup.controls
      .remainingcount_dtl.value;

    this.iinv_in_trx_hdr.transactionDate = trxdt;
    this.iinv_in_trx_hdr.receivedDate = rcvddate;
    this.iinv_in_trx_hdr.receivedBy = rcvdby;
    this.iinv_in_trx_hdr.poNumber = ponumber;
    this.iinv_in_trx_hdr.invoiceNo = invcno;
    this.iinv_in_trx_hdr.referenceNo = refno;
    this.iinv_in_trx_hdr.documnetNo = docno;
    this.iinv_in_trx_hdr.supplier = sup;
    this.iinv_in_trx_hdr.remarks = remarks;

    this.iinv_in_trx_dtl.itemID = itemid;
    this.iinv_in_trx_dtl.unit = unit;
    this.iinv_in_trx_dtl.quantity = qty;
    this.iinv_in_trx_dtl.lotNumber = lotno;
    this.iinv_in_trx_dtl.expirationDate = expdate;
    this.iinv_in_trx_dtl.count = count;
    this.iinv_in_trx_dtl.remainigCount = remainingcount;

    if (ponumber == "" || invcno == "" || rcvdby == "" || lotno == "" || rcvdby == "" || itemid == "" ||
      itemid == null || unit == null || qty == null || ponumber == null ||  invcno == null || rcvdby == null || lotno == null || rcvdby == null ) {
      Swal.fire({
        title: 'Warning!', text: 'Please input on the required fields!', icon:'error',
        closeOnClickOutside: false,
        closeOnEsc: false,
        allowOutsideClick: false})
      this.dtOptions = {
        pagingType: 'full_numbers',
        pageLength: 1,
        destroy: true,
        retrieve: true,
        responsive: true,
      };
    }
    else {

      Swal.fire({
        title: 'Are you sure do you want to proceed?', text: 'Inventory In', icon: 'warning', showCancelButton: true,
        confirmButtonText: 'Yes', cancelButtonText: 'Cancel',
        closeOnClickOutside: false,
        closeOnEsc: false,
        allowOutsideClick: false,

      }).then((result) => {
        if (result.value) {

          this.CreateAutoGeneratedID();
          let trxnohdr = this.addInventoryInFormGroup.controls.trxNo_hdr.value;
          let trxnodtl = this.addInventoryInFormGroup.controls.trxNo_hdr.value;
          this.iinv_in_trx_hdr.transactionNo = trxnohdr;
          this.iinv_in_trx_dtl.transactionNo = trxnodtl;

          this.inv_in_service
            .insertInventoryInTrxHeader(this.iinv_in_trx_hdr)
            .subscribe(
              (data) => {
                this.toastr.success("Data Saved", "Saved");
              },
              (error) => {
                errormessage = error.error;
                this.toastr.error(errormessage, "Error");
              }
            );

          this.inv_in_service
            .insertInventoryInTrxDetails(this.iinv_in_trx_dtl)
            .subscribe(
              (data) => {
                this.toastr.success("Data Saved", "Saved");
              },
              (error) => {
                errormessage = error.error;
                this.toastr.error(errormessage, "Error");
              }
            );
          Swal.fire('Succesfully Saved!', 'Transaction sucessfull.', 'success')

        } else if (result.dismiss === Swal.DismissReason.cancel) {

          Swal.fire('Cancelled', 'Transaction was cancelled!', 'error')
        }
      })

    }
  }

  public loadDataFromDictionary() {
    this.inv_in_service.getItemMasterData().subscribe((itemmaster) => (this.Iitemmaster = itemmaster));
    this.inv_in_service.getSupplierData().subscribe((supplier) => (this.Isupplier = supplier));
  }

  public onSelectedItemMaster(value: string): void {
    //this.isReadonly_UC = false;
    try {
      this.itemmasterId_doc = document.getElementById(
        this.addInventoryInFormGroup.controls.itemMaster_dtl.value).innerText;
        value = this.itemmasterId_doc;

      this.inv_in_service.getItemasterUnit(value).subscribe((itemmaster) => (this.ItemMasterUnitArray = itemmaster));
      this.enabledUnitCode();
      this.enabledLookUpButtonItemUnit();
    } catch {}
  }

  public onSelectCoversionFactor( itemMasterId: string, itemMasterUnitId: string) {
    try {
      this.itemmasterUnit_doc = document.getElementById(this.addInventoryInFormGroup.controls.itemUnit_dtl.value).innerText;

      itemMasterId = this.itemmasterId_doc;
      itemMasterUnitId = this.itemmasterUnit_doc;

      this.inv_in_service.getItemasterUnitConversionFactor(itemMasterId, itemMasterUnitId).subscribe((itemmasterunit) => {
      this.ItemMasterUnitArray_convfactor = itemmasterunit;

          for (let i of this.ItemMasterUnitArray_convfactor) {
            this.conversionFactor = parseInt(i.itemMasterUnitConversion);
          }
        });
      this.enabledQuantity();
    } catch {}
  }
  public onTextChangedQuantityInput() {
    try {
      this.iinv_in_trx_dtl.quantity = this.addInventoryInFormGroup.controls.quantity_dtl.value;
      let computefactor = this.iinv_in_trx_dtl.quantity * this.conversionFactor;

      this.addInventoryInFormGroup.controls.count_dtl.setValue(
        this.iinv_in_trx_dtl.quantity
      );
      this.addInventoryInFormGroup.controls.remainingcount_dtl.setValue(
        computefactor
      );
    } catch {}
  }

  public resetScreen() {
    this.loadDataFromDictionary();
    this.addInventoryInFormGroup.reset();
    this.getDateTimeNow();
    this.addInventoryInFormGroup.controls.trxNo_hdr.setValue(
      "*********************************"
    );
    this.getListUser();
    this.isReadonly_QTY = true;
    this.isReadonly_UC = true;
    this.isReadonly_LookUp_Btn_ItemUnit = true;
    this.conversionFactor = null;
    this.itemmasterId_doc = null;
    this.itemmasterUnit_doc = null;
    this.itemmasteunitSelectList = null;
    this.itemasterSelectList = null;
    this.supplierSelectList = null;
    this.isForInvIn = true;
    this.addInventoryInFormGroup.controls.rcvdBy_hdr.setValue(this.userIDLogin);
    this.isExpirable = true;
  }
  public enabledUnitCode() {
    if (this.addInventoryInFormGroup.controls.itemMaster_dtl.value == "") {
      this.isReadonly_UC = true;
      this.isReadonly_QTY = true;
      this.isReadonly_LookUp_Btn_ItemUnit = true;
      this.itemmasterUnit_doc = null;
      this.addInventoryInFormGroup.controls.itemUnit_dtl.reset();
      this.addInventoryInFormGroup.controls.quantity_dtl.reset();
      this.addInventoryInFormGroup.controls.count_dtl.reset();
      this.addInventoryInFormGroup.controls.remainingcount_dtl.reset();
    } else {
      this.isReadonly_UC = false;
      this.isReadonly_LookUp_Btn_ItemUnit = false;
    }
  }
  public enabledQuantity() {
    if (this.addInventoryInFormGroup.controls.itemUnit_dtl.value == "") {
      this.isReadonly_QTY = true;
      this.addInventoryInFormGroup.controls.quantity_dtl.reset();
      this.addInventoryInFormGroup.controls.count_dtl.reset();
      this.addInventoryInFormGroup.controls.remainingcount_dtl.reset();
    } else {
      this.isReadonly_QTY = false;
    }
  }

  supplierSelectLookUp(supplierId: string) {
    console.log(supplierId);

    this.inv_in_service.getSupplierData().subscribe((supplier) => {
      this.supplierSelectList = supplier;

      for (let sup of this.supplierSelectList) {
        let suppId = sup.id;
        if (supplierId == suppId) {
          this.addInventoryInFormGroup.controls.supplier_hdr.setValue(sup.name);
        } else {
        }
      }
    });
  }

  itemMasterSelectLookUp(value: string) {
    try {

      this.addInventoryInFormGroup.controls.itemMaster_dtl.reset();
      this.addInventoryInFormGroup.controls.itemUnit_dtl.reset();
      this.addInventoryInFormGroup.controls.quantity_dtl.reset();
      this.addInventoryInFormGroup.controls.count_dtl.reset();
      this.addInventoryInFormGroup.controls.remainingcount_dtl.reset();

      this.inv_in_service.getItemMasterData().subscribe((itemmaster) => {
        this.itemasterSelectList = itemmaster;

        for (let im of this.itemasterSelectList) {
          let imID = im.id;

          if (value == imID) {

            

            this.addInventoryInFormGroup.controls.itemMaster_dtl.setValue(im.itemName);
            this.itemmasterId_doc = document.getElementById(this.addInventoryInFormGroup.controls.itemMaster_dtl.value).innerText;
            value = this.itemmasterId_doc;

            this.inv_in_service.getItemasterUnit(value).subscribe((itemmaster) => (this.ItemMasterUnitArray = itemmaster));
            this.enabledUnitCode();
          } else {
          }
        }
      });
    } catch {}
  }
  itemMasterUnitSelectLookup(itemMasterUnitId: string) {
    try {
      let itemMasterId = document.getElementById(this.addInventoryInFormGroup.controls.itemMaster_dtl.value).innerText;
      this.inv_in_service.getItemasterUnit(itemMasterId).subscribe((itemmaster) => {
          this.itemmasteunitSelectList = itemmaster;

          for (let imu of this.itemmasteunitSelectList) {
            if (itemMasterUnitId == imu.unitCode) {
              this.addInventoryInFormGroup.controls.itemUnit_dtl.setValue(imu.unitDescription);

              this.itemmasterUnit_doc = document.getElementById(this.addInventoryInFormGroup.controls.itemUnit_dtl.value).innerText;
              itemMasterUnitId = this.itemmasterUnit_doc;

              this.inv_in_service
                .getItemasterUnitConversionFactor(
                  itemMasterId,
                  itemMasterUnitId
                )
                .subscribe((itemmasterunit) => {
                  this.ItemMasterUnitArray_convfactor = itemmasterunit;

                  for (let i of this.ItemMasterUnitArray_convfactor) {
                    this.conversionFactor = parseInt(i.itemMasterUnitConversion);
                  }
                });
              this.enabledQuantity();
            }
          }
        });
    } catch {}
  }
  enabledLookUpButtonItemUnit() {
    if (this.addInventoryInFormGroup.controls.itemMaster_dtl.value == "") {
      this.isReadonly_LookUp_Btn_ItemUnit = true;
    } else {
      this.isReadonly_LookUp_Btn_ItemUnit = false;
    }
  }
  file: File;
  public incomingfile(event) {
    this.file = event.target.files[0];
    this.GetData();
  }
  public GetData() {
    let fileReader = new FileReader();
    fileReader.onload = (e) => {
      this.arrayBuffer = fileReader.result;
      let data = new Uint8Array(this.arrayBuffer);
      let arr = new Array();
      for (let i = 0; i != data.length; ++i)
        arr[i] = String.fromCharCode(data[i]);
      let bstr = arr.join("");
      let workbook = XLSX.read(bstr, { type: "binary" });
      let first_sheet_name = workbook.SheetNames[0];
      let worksheet = XLSX.utils.sheet_to_json(
        workbook.Sheets[first_sheet_name]
      );

      Array.from(worksheet).forEach((arr) => {
        this.excelUpload.push({
          SpecimenId: arr["SpecimenId"],
          LastName: arr["LastName"],
          FirstName: arr["FirstName"],
          MiddleName: arr["MiddleName"],
        });
      });
    };
    fileReader.readAsArrayBuffer(this.file);
  }
  public Upload() {
    Array.from(this.excelUpload).forEach((arr) => {
      console.log(
        arr.SpecimenId +
          " " +
          arr.LastName +
          " " +
          arr.FirstName +
          " " +
          arr.MiddleName
      );
    });
  }
}
export interface ExcelUploadTable {
  SpecimenId: string;
  LastName: string;
  FirstName: string;
  MiddleName: string;
}
