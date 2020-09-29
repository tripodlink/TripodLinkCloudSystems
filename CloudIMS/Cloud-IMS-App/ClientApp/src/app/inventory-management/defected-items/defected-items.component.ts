import { Component, OnInit } from '@angular/core';
import { DefectedItemsService } from '../../services/DefectedItems.service'
import { IDefectedItemsFinalClass } from '../../classes/inventory-management/defected-items/IDefectedItemsClass'
import { IDefectedItemsLotNumberClass } from '../../classes/inventory-management/defected-items/IDefectedItemsClass'
import { IDefectedItemsTransactionNumberClass } from '../../classes/inventory-management/defected-items/IDefectedItemsClass'
import { IDefectedItemsNameClass } from '../../classes/inventory-management/defected-items/IDefectedItemsClass'
import { IDefectedItemsUnitIdClass } from '../../classes/inventory-management/defected-items/IDefectedItemsClass'
import { IDefectedItemsToSaveClass } from '../../classes/inventory-management/defected-items/IDefectedItemsClass'
import { IDefectedItemsListClass } from '../../classes/inventory-management/defected-items/IDefectedItemsClass'
import { IDefectedItemsFillAllClass } from '../../classes/inventory-management/defected-items/IDefectedItemsClass'
import { IDefectedItemsRemarksClass } from '../../classes/inventory-management/defected-items/IDefectedItemsClass'
import { IDefectedItemsItemMasterClass } from '../../classes/inventory-management/defected-items/IDefectedItemsClass'

import { FormBuilder, FormGroup, Validators } from "@angular/forms";

import { CookieService } from 'ngx-cookie-service';
import { ToastrService } from 'ngx-toastr';
import { DatePipe } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { each } from 'jquery';
import { IDefectedItemsToSave } from '../../classes/inventory-management/defected-items/IDefectedItems.interface';
import { error } from '@angular/compiler/src/util';
import { get } from 'http';
import { clear } from 'console';
import Swal from 'sweetalert2/dist/sweetalert2.js';


@Component({
  selector: 'app-defected-items',
  templateUrl: './defected-items.component.html',
})
export class DefectedItemsComponent implements OnInit {

  IDefectedItemsFinal: IDefectedItemsFinalClass[];
  IDefectedItemsLot: IDefectedItemsLotNumberClass[];
  IDefectedItemsTrx: IDefectedItemsTransactionNumberClass[];
  IDefectedItemsName: IDefectedItemsNameClass[];
  IDefectedItemsUnit: IDefectedItemsUnitIdClass[];
  IDefectedItemsToList: IDefectedItemsListClass[];
  IDefectedItemsToSave: IDefectedItemsToSave = new IDefectedItemsToSaveClass();
  IDefectedItemsFillAll: IDefectedItemsFillAllClass[];
  IDefectedItemsRemarks: IDefectedItemsRemarksClass[];
  IDefectedItemsItemMaster: IDefectedItemsItemMasterClass[];

  defectedItemFormGroup: FormGroup;

  itemId: string;
  itemName: string;
  lotNo: string;
  status: string;
  deleteItemName: string;

  _delete_id: string;
  _update_id: string;

  unitMultiplier: number;

  //
  status_isReadOnly: boolean;
  item_id_isReadOnly: boolean;
  lot_no_isReadOnly: boolean;
  item_unit_isReadOnly: boolean;
  quantity_isReadOnly: boolean;
  remarks_isReadOnly: boolean;
  trx_no_isReadOnly: boolean;

  isHiddenAdd: boolean;
  isHiddenUpdate: boolean;
  isHiddenDelete: boolean;
  isHiddenClear: boolean;

  trxNumFunction: string;
  finalTrxDefect: string;

  searchString: string = "ALL";

  constructor(private defectedItemsSer: DefectedItemsService, public toastr: ToastrService, public builder: FormBuilder,
    public cookieService: CookieService,
    public datepipe: DatePipe,
    public _activatedRoute: ActivatedRoute) {

    this.CreateForm();
    this.status_isReadOnly = false

  }

  ngOnInit() {
    this.hideUD()
    this.getTolist(this.searchString)
    this.defectedItemFormGroup.controls.status.setValue("IN")
    this.status = "IN"
    this.item_id_isReadOnly = false
    this.itemName = ""

    this.getRemarks()

  }



  public CreateForm() {
    this.defectedItemFormGroup = this.builder.group({
      status: ['', Validators.required],
      def_trx_no: [''],
      item_id: ['', Validators.required],
      item_name: [''],
      item_unit: [''],
      quantity: ['', Validators.required],
      lot_no: ['', Validators.required],
      count: [''],
      remarks: [''],

      trx_no: ['', Validators.required],
      trx_date: [''],
      po_no: [''],
      inv_no: [''],
      rcvd_date: [''],
      rcvd_by: [''],
      supplier: [''],
      ref_no: [''],
      doc_no: [''],
      date_now: [''],

    })

    this.disableAll()
    this.status_isReadOnly = false;
  }

  //Button Clicks
  addDefectedItem() {
    this.getTrxNumFunction()
  }
  


  updateDefectedItem() {
    this.getDateTimeNow()
  console.log(this._update_id)
    this.IDefectedItemsToSave.defectTransactionNo = this._update_id
    this.IDefectedItemsToSave.itemID = this.defectedItemFormGroup.controls.item_id.value
    this.IDefectedItemsToSave.lotNumber = this.defectedItemFormGroup.controls.lot_no.value
    this.IDefectedItemsToSave.transactionNo = this.defectedItemFormGroup.controls.trx_no.value
    this.IDefectedItemsToSave.itemUnit = this.defectedItemFormGroup.controls.item_unit.value
    this.IDefectedItemsToSave.transactionDate = this.defectedItemFormGroup.controls.date_now.value as Date
    this.IDefectedItemsToSave.quantity = Number(this.defectedItemFormGroup.controls.quantity.value)
    this.IDefectedItemsToSave.remarks = this.defectedItemFormGroup.controls.remarks.value
    this.IDefectedItemsToSave.status = this.defectedItemFormGroup.controls.status.value

    if (this.defectedItemFormGroup.controls.item_id.value.trim() != "") {
      let errormessage = "Error";
      this.defectedItemsSer.updateDefectedItems(this.IDefectedItemsToSave).subscribe((data) => {
        this.toastr.success("Update", "Successfully updated.");

        this.hideUD()
        this.isHiddenClear = false
        this.status_isReadOnly = true
        this.item_id_isReadOnly = true
        this.getTolist(this.searchString)
        this.resetScreen()
        this.IDefectedItemsToSave = new IDefectedItemsToSaveClass()
      },
        error => {
          errormessage = error.error;
          this.toastr.error(errormessage, "Error");
        });
    }

  }

  deleteDefectedItem() {
    let id = this._delete_id

    Swal.fire({
      title: 'Are you sure do you want to delete this Transaction ' + id + ' ?', text: 'You will not be able to recover this Transaction ' + id, icon: 'warning', showCancelButton: true,
      confirmButtonText: 'Yes', cancelButtonText: 'Cancel'

    }).then((result) => {
      if (result.value) {
        let errormessage = "Error";
        this.defectedItemsSer.deleteDefectedItem(id).subscribe(data => {
          this.toastr.info("Successfully deleted item on defected items.", "Deleted");
          this.resetScreen()
          this.getTolist(this.searchString)
          this.getRemarks()

        },
          error => {
            errormessage = error.error;
            this.toastr.error(errormessage, "Item Master Error");
          });

      } else if (result.dismiss === Swal.DismissReason.cancel) {

        Swal.fire('Cancelled', 'Your data is safe!', 'error')
      }
    })
    this.deleteItemName = ""
  }

  btnClear() {
    this.itemName = ""

  }

  showModalAdd() {
    this.hideUD()
    this.isHiddenAdd = false
  }

  async showModalUpdate(deftrx: string, statuss: string) {
    this.hideUD()
    this.isHiddenUpdate = false
    this._update_id = deftrx
    await this.fillAll(this._update_id, statuss)
    this.item_id_isReadOnly = true
    this.status_isReadOnly = true
    this.itemIdTextChange()
    this.lot_no_isReadOnly = false
    this.onupdateED(false)

  }

  showModalDelete(deftrx: string, status: string, iname: string) {
    this.hideUD()
    this.isHiddenDelete = false
    this._delete_id = deftrx
    this.fillAll(this._delete_id, status)
    this.deleteItemName = iname;
    this.item_id_isReadOnly = true
    this.status_isReadOnly = true
    this.deleteItemName = iname;
  }

  itemMasterModalClick() {
    let errormessage = "Fetching items error"
    this.defectedItemsSer.getItems().subscribe((data) => {
      this.IDefectedItemsItemMaster = data

    },
      error => {
        errormessage = error.error;
        this.toastr.error(errormessage, "Error")
      });

  
  }

  async itemMasterSelectLookUp(id: string) {
    this.itemId = id
    this.defectedItemFormGroup.controls.item_id.setValue(this.itemId)
    //////////////////
    //////////////////
    this.itemIdTextChange()
    if (this.itemName == "") {
      this.disableItemID(true)
      this.clearAll()
      this.clearTransactionDetails()
      this.disableLotNumber(true)

    } else {

      this.defectedItemFormGroup.controls.lot_no.setValue("")
      this.disableLotNumber(true)
      await this.getLotNumber(this.itemId)
    }
  }



  hideUD() {
    this.isHiddenClear = true
    this.isHiddenAdd = true
    this.isHiddenUpdate = true
    this.isHiddenDelete = true
  }

  resetScreen() {
    this.itemName = ""
    this.status = "IN"
    this.status_isReadOnly = false
    this.defectedItemFormGroup.controls.status.setValue("IN")
    this.defectedItemFormGroup.controls.item_id.setValue("")
    this.defectedItemFormGroup.controls.item_name.setValue(this.itemName)
    this.item_id_isReadOnly = false
    this.clearAll()
    this.disableAll()
    this.hideUD()
    this.isHiddenAdd = false
  }

  //Disable
  disableAll() {
    this.lot_no_isReadOnly = true
    this.item_unit_isReadOnly = true
    this.quantity_isReadOnly = true
    this.disableLotNumber(true)
  }

  disableItemID(stat: boolean) {
    this.defectedItemFormGroup.controls.item_id.setValue("")
    this.defectedItemFormGroup.controls.item_name.setValue("")
    this.lot_no_isReadOnly = stat
    this.disableLotNumber(stat)
    this.clearAll()
  }

  disableLotNumber(stat: boolean) {
    this.disableItemUnit(stat)
    this.disableTransactionNumber(stat)
    this.clearLotNumber()
  }

  disableItemUnit(stat: boolean) {
    this.item_unit_isReadOnly = stat
    this.clearItemUnit()
    this.remarks_isReadOnly = stat
    this.quantity_isReadOnly = stat
  }

  disableTransactionNumber(stat: boolean) {
    this.trx_no_isReadOnly = stat
    this.clearTransactionDetails()
  }

  //ENABLE AND DISABLE
  onupdateED(s: boolean) {
    this.item_unit_isReadOnly = s
    this.quantity_isReadOnly = s
    this.remarks_isReadOnly = s
    this.trx_no_isReadOnly = s
  }

  //Clear
  clearAll() {
    this.defectedItemFormGroup.controls.def_trx_no.setValue("")
    this.defectedItemFormGroup.controls.quantity.setValue("")
    this.defectedItemFormGroup.controls.count.setValue("")
    this.defectedItemFormGroup.controls.lot_no.setValue("")
    this.clearLotNumber

  }

  clearTransactionDetails() {
    this.defectedItemFormGroup.controls.trx_date.setValue("")
    this.defectedItemFormGroup.controls.po_no.setValue("")
    this.defectedItemFormGroup.controls.inv_no.setValue("")
    this.defectedItemFormGroup.controls.rcvd_date.setValue("")
    this.defectedItemFormGroup.controls.rcvd_by.setValue("")
    this.defectedItemFormGroup.controls.supplier.setValue("")
    this.defectedItemFormGroup.controls.ref_no.setValue("")
    this.defectedItemFormGroup.controls.doc_no.setValue("")
  }

  clearItemUnit() {
    this.defectedItemFormGroup.controls.quantity.setValue("")
    this.defectedItemFormGroup.controls.remarks.setValue("")
  }


  clearLotNumber() {
    this.defectedItemFormGroup.controls.item_unit.setValue("")
    this.clearItemUnit()
    this.defectedItemFormGroup.controls.trx_no.setValue("")
    this.clearTransactionDetails()
  }

  //Tools

  multiplyUnitConversionToQuantity() {
    console.log(this.unitMultiplier)
    let quantity = parseInt(this.defectedItemFormGroup.controls.quantity.value)
    if (quantity < 0) {
      quantity = 0
      this.defectedItemFormGroup.controls.quantity.setValue(quantity.toString())
    } else {
      let productCount = this.unitMultiplier * quantity
      this.defectedItemFormGroup.controls.count.setValue(productCount.toString())
    }
  }

  public getDateTimeNow() {
    let today = new Date();
    let getDate = today.toISOString().slice(0, 10);
    let cutTime = today.toTimeString().slice(0, 5);

    let CompleteDate = getDate + "T" + cutTime

    this.defectedItemFormGroup.controls.date_now.setValue(CompleteDate);
  }



  //Text Change


  statusChanged() {
    let stat = this.defectedItemFormGroup.controls.status.value
    if (stat == "IN" || stat == "OUT") {
      this.status = stat
      this.item_id_isReadOnly = false
    } else {
      this.item_id_isReadOnly = true
      this.disableItemID(true)
    }
  }

  statusTextChanged() {
    let stat = this.defectedItemFormGroup.controls.status.value
    if (stat == "IN" || stat == "OUT") {
      this.status = stat
      this.item_id_isReadOnly = false
    } else {
      this.item_id_isReadOnly = true
      this.disableItemID(true)
    }
  }


  getTransactionIDs() {
    this.lotNo = this.defectedItemFormGroup.controls.lot_no.value
    this.getTransactionNumbers(this.itemId, this.lotNo, this.status)
    this.disableLotNumber(true)
  }

  lotNumberTextChanged() {
    if (this._update_id != "") {
      this.itemId = this.defectedItemFormGroup.controls.item_id.value
    }
    this.getItemUnit(this.itemId)
    this.lotNo = this.defectedItemFormGroup.controls.lot_no.value
    this.getTransactionNumbers(this.itemId, this.lotNo, this.status)
    this.disableLotNumber(true)
  }

  getFinalDetail() {
    this.itemId = this.defectedItemFormGroup.controls.item_id.value
    this.lotNo = this.defectedItemFormGroup.controls.lot_no.value
    this.status = this.defectedItemFormGroup.controls.status.value
    this.getFinalDetails(this.itemId, this.lotNo, this.status)
  }

  transactionNumberTextChange() {
    this.itemId = this.defectedItemFormGroup.controls.item_id.value
    this.lotNo = this.defectedItemFormGroup.controls.lot_no.value
    this.status = this.defectedItemFormGroup.controls.status.value
    this.getFinalDetails(this.itemId, this.lotNo, this.status)
  }

  getUnitConversion() {
    for (let u of this.IDefectedItemsUnit) {
      this.unitMultiplier = parseInt(u.unitConversion.toString())
    }
    this.quantity_isReadOnly = false
  }

  itemUnitTextChange() {
    this.itemId = this.defectedItemFormGroup.controls.item_id.value
    let unit = this.defectedItemFormGroup.controls.item_unit.value
    if (unit == "") {
      this.getItemUnit(this.itemId)
    }

    this.clearItemUnit()
  }

  //Async Tasks
  async getStringItemName(itemId: string) {
    let item = this.defectedItemsSer.getSingleName(itemId)


    await item.toPromise().then((strName) => {
      this.itemName = strName;
      this.defectedItemFormGroup.controls.item_name.setValue(this.itemName)
    }, error => {
      this.toastr.error("No item found on item id", "NO ITEM");
    })

  }

  async itemIdTextChange() {
    this.itemId = this.defectedItemFormGroup.controls.item_id.value
    console.log(this.itemId)
      await this.getStringItemName(this.itemId)

      if (this.itemName == "") {
        this.disableItemID(true)
        this.clearAll()
        this.clearTransactionDetails()
        this.disableLotNumber(true)
        this.defectedItemFormGroup.controls.def_trx_no.setValue("")
        this.defectedItemFormGroup.controls.item_name.setValue("")
        this.itemName = ""
        this.clearAll()
        this.clearTransactionDetails()
        this.lot_no_isReadOnly = true
        this.disableLotNumber(true)

      } else {
        this.defectedItemFormGroup.controls.item_id.setValue(this.itemId)
        this.getLotNumber(this.itemId)
        this.lot_no_isReadOnly = false


      }

    if (this.itemId.length > 0) {
      this.isHiddenClear = false
    } else {
      this.isHiddenClear = true
    }

  }

  async getLotNumber(itemId: string) {
    let errormessage = "Error getting lot number"
    await this.defectedItemsSer.getLotNumber(itemId).toPromise().then((lotnumber) => {
      this.IDefectedItemsLot = lotnumber;
      if (this.IDefectedItemsLot.length > 0) {
        this.lot_no_isReadOnly = false

      } else {
        this.toastr.error("Item has no lot number.", "Error");
        this.lot_no_isReadOnly = true
      }

    }, error => {
      errormessage = error.error;
      this.toastr.error(errormessage, "Error lot number");
    })

  }

  async getTransactionNumbers(itemId: string, lotno: string, status: string) {
    let errormessage = "Error getting transaction numbers"

    await this.defectedItemsSer.getTrxNumber(itemId, lotno, status).toPromise().then((trx) => {
      this.IDefectedItemsTrx = trx;
      if (this.IDefectedItemsTrx.length < 1) {
        this.toastr.error("There is no " + status + " record for the selected Lot Number.", "Warning");
        this.trx_no_isReadOnly = true
        this.disableTransactionNumber(true)
      } else {
        this.trx_no_isReadOnly = false
        this.disableTransactionNumber(false)
        this.item_unit_isReadOnly = false
        this.disableItemUnit(false)
      }

    }, error => {
      errormessage = error.error;
      this.toastr.error("There is no " + status + " record for the selected Lot Number.", "Warning");
    })

  }


  async getItemUnit(itemId: string) {
    await this.defectedItemsSer.getUnit(itemId).toPromise().then((unit) => {
      this.IDefectedItemsUnit = unit;
      if (this.IDefectedItemsUnit.length <= 0){
        this.toastr.error("There is no item unit for the selected item.", "Warning");
      } 
    })

  }

  async getTolist(itemName: string) {
    await this.defectedItemsSer.getDfToList(itemName).toPromise().then((df) => {
      this.IDefectedItemsToList = df;

    })

  }

  async getFinalDetails(itemId: string, lotno: string, status: string) {
    let errormessage = "Error getting final details"
    console.log(itemId)
    console.log(lotno)
    console.log(status)
    await this.defectedItemsSer.getFinalDetails(itemId, lotno, status).toPromise().then((finals) => {
      this.IDefectedItemsFinal = finals;
      if (this.IDefectedItemsFinal.length > 0) {
        for (let final of this.IDefectedItemsFinal) {
          if (this.defectedItemFormGroup.controls.trx_no.value == final.transactionNo) {
            this.defectedItemFormGroup.controls.trx_no.setValue(final.transactionNo)
            this.defectedItemFormGroup.controls.trx_date.setValue(final.transactionDate)
            this.defectedItemFormGroup.controls.po_no.setValue(final.poNumber)
            this.defectedItemFormGroup.controls.inv_no.setValue(final.invoiceNo)
            this.defectedItemFormGroup.controls.rcvd_date.setValue(final.receivedDate)
            this.defectedItemFormGroup.controls.rcvd_by.setValue(final.receivedBy)
            this.defectedItemFormGroup.controls.supplier.setValue(final.supplier)
            this.defectedItemFormGroup.controls.ref_no.setValue(final.referenceNo)
            this.defectedItemFormGroup.controls.doc_no.setValue(final.documnetNo)
          } else {
            this.getTransactionNumbers(this.itemId, this.lotNo, this.status)
            this.clearTransactionDetails()
            this.toastr.error("Transaction number incorrect.", "");
          }

        }

      } else {
        this.getTransactionNumbers(this.itemId, this.lotNo, this.status)
        this.clearTransactionDetails()
        this.toastr.error("No Transaction", "");
      }
        

    }, error => {
      errormessage = error.error;
      this.toastr.error(errormessage, "Error final details.");
    });

  }

  async markedAsDefected(trs: string) {
    this.getDateTimeNow()

    this.defectedItemFormGroup.controls.def_trx_no.setValue(trs)

    this.IDefectedItemsToSave.defectTransactionNo = trs
    this.IDefectedItemsToSave.itemID = this.defectedItemFormGroup.controls.item_id.value
    this.IDefectedItemsToSave.lotNumber = this.defectedItemFormGroup.controls.lot_no.value
    this.IDefectedItemsToSave.transactionNo = this.defectedItemFormGroup.controls.trx_no.value
    this.IDefectedItemsToSave.itemUnit = this.defectedItemFormGroup.controls.item_unit.value
    this.IDefectedItemsToSave.quantity = this.defectedItemFormGroup.controls.quantity.value
    this.IDefectedItemsToSave.transactionDate = this.defectedItemFormGroup.controls.date_now.value as Date
    this.IDefectedItemsToSave.remarks = this.defectedItemFormGroup.controls.remarks.value
    this.IDefectedItemsToSave.status = this.status

    if (this.defectedItemFormGroup.controls.item_id.value.trim() != "") {
      let errormessage = "Error";
      this.defectedItemsSer.addDefectedItems(this.IDefectedItemsToSave).subscribe((data) => {
        this.toastr.success("Insert", "New defected item inserted.");

        this.hideUD()
        this.isHiddenClear = false
        this.status_isReadOnly = true
        this.item_id_isReadOnly = true
        this.getTolist(this.searchString)
        this.resetScreen()
        this.IDefectedItemsToSave = new IDefectedItemsToSaveClass()
        this.getRemarks()
      },
        error => {
          errormessage = error.error;
          this.toastr.error(errormessage, "Error");
        });
    }
  }


  async getTrxNumFunction() {
    let errormessage = "Transaction Number Error"
    this.defectedItemsSer.getTrxNumFunction().subscribe((data) => {
      this.trxNumFunction = data


      let trxNum = this.trxNumFunction.split('|');
      let type = trxNum[0].toString();
      let year = trxNum[1];
      let convertYear = this.datepipe.transform(new Date(), year.toLowerCase());
      let num = trxNum[2];

      let deftrx = type + convertYear + num
      this.markedAsDefected(deftrx)
    },
      error => {
        errormessage = error.error;
        this.toastr.error(errormessage, "Error")
      });
  }

  async fillAll(deftrx: string, statuss: string) {
    let errormessage = "Error in getting defected item transactions"
    await this.defectedItemsSer.fillAll(deftrx, statuss).toPromise().then((fill_all) => {
      this.IDefectedItemsFillAll = fill_all;

      if (this.IDefectedItemsFillAll.length > 0) {

        for (let final of this.IDefectedItemsFillAll) {
          this.defectedItemFormGroup.controls.status.setValue(final.status)
          this.defectedItemFormGroup.controls.item_id.setValue(final.itemId)
          this.defectedItemFormGroup.controls.item_name.setValue(final.itemName)
          this.defectedItemFormGroup.controls.item_unit.setValue(final.itemUnit)
          this.defectedItemFormGroup.controls.quantity.setValue(final.quantity)
          this.defectedItemFormGroup.controls.lot_no.setValue(final.lotNo)
          this.defectedItemFormGroup.controls.count.setValue(final.itemUnitConversion * final.quantity)
          this.defectedItemFormGroup.controls.remarks.setValue(final.remarks)
          this.defectedItemFormGroup.controls.trx_no.setValue(final.transactionNo)
          this.defectedItemFormGroup.controls.trx_date.setValue(final.transactionDate)
          this.defectedItemFormGroup.controls.po_no.setValue(final.poNo)
          this.defectedItemFormGroup.controls.inv_no.setValue(final.invNo)
          this.defectedItemFormGroup.controls.rcvd_date.setValue(final.receivedDate)
          this.defectedItemFormGroup.controls.rcvd_by.setValue(final.receivedBy)
          this.defectedItemFormGroup.controls.supplier.setValue(final.supplierName)
          this.defectedItemFormGroup.controls.ref_no.setValue(final.refNo)
          this.defectedItemFormGroup.controls.doc_no.setValue(final.documentNo)

          //SET UNIT CONVERSION
          this.unitMultiplier = final.itemUnitConversion
        }

      } else {
        this.toastr.error("No data", "No data");
      }

    }, error => {
      errormessage = error.error;
      this.toastr.error(errormessage, "Error defected item");
    })



  }


  async getRemarks() {
    let errormessage = "Fetching remaks error"
    this.defectedItemsSer.getRemarks().subscribe((data) => {
      this.IDefectedItemsRemarks = data

    },
      error => {
        errormessage = error.error;
        this.toastr.error(errormessage, "Error")
      });

  }


}



