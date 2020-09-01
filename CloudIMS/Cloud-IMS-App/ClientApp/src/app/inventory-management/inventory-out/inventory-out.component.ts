import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { CookieService } from 'ngx-cookie-service';
import { ToastrService } from 'ngx-toastr';
import { IDepartment } from '../../classes/data-dictionary/Department/IDepartment.interface';
import { IiTemMasterUnitJoinUnit } from '../../classes/data-dictionary/ItemMasterUnit/IitemMasterUnitJoinUnit.interface';
import { IitemMasterJoinInvIN } from '../../classes/data-dictionary/ItemMasterUnit/IitemMasterJoinInvIN.interface';
import { IinventoryOutDetail } from '../../classes/inventory-management/inventory-out/IinventoryOutDetail.interface';
import { IinventoryOutTable } from '../../classes/inventory-management/inventory-out/IinventoryOutDetail.interface';
import { IinventoryOutDetailClass } from '../../classes/inventory-management/inventory-out/IinventoryOutDetailClass';
import { IinventoryOutTableClass } from '../../classes/inventory-management/inventory-out/IinventoryOutDetailClass';
import { IinventoryOutHeader } from '../../classes/inventory-management/inventory-out/IinventoryOutHeader.interface';
import { IinventoryOutIssuedTrx } from '../../classes/inventory-management/inventory-out/IinventoryOutHeader.interface';
import { IinventoryOutPendingTrx } from '../../classes/inventory-management/inventory-out/IinventoryOutHeader.interface';
import { IinventoryOutHeaderClass } from '../../classes/inventory-management/inventory-out/IinventoryOutHeaderClass';
import { IInventoryInTrxDetail } from '../../classes/inventory-management/InventoryIn/IInventoryInTrxDetail.interface';
import { InventorysServices } from '../../services/inventorys.service';
import { DatePipe } from '@angular/common';
import { ActivatedRoute } from '@angular/router';

import { AppSidebarMenuComponent } from '../../app-sidebar-menu/app-sidebar-menu.component'


@Component({
  selector: 'app-inventory-out',
  templateUrl: './inventory-out.component.html',
  styleUrls: ['./inventory-out.component.css']
})
export class InventoryOutComponent implements OnInit {

  userIDLogin: string;

  appSideBarMenu: AppSidebarMenuComponent;

  InventoryOutHeaderForm: FormGroup;
  InventoryOutDetailForm: FormGroup;
  quantityForm: FormGroup;
  findTrxPending: FormGroup;
  findTrxIssued: FormGroup;
  depListArray: IDepartment[];

  inventoryPendingTrx: IinventoryOutHeader[];
  inventoryInArray: IInventoryInTrxDetail[];
  unitArray: IiTemMasterUnitJoinUnit[];
  lotNumArray: IitemMasterJoinInvIN[];
  Count: IInventoryInTrxDetail[];
  remainingCount: number;

  pendingTrxArray: IinventoryOutPendingTrx[];
  issuedTrxArray: IinventoryOutIssuedTrx[];

  inventHeadOutArray: IinventoryOutHeader = new IinventoryOutHeaderClass();
  inventDetailOutArray: IinventoryOutDetail = new IinventoryOutDetailClass();

  retrieveOutTable: IinventoryOutTable[];
  itemArrayDTL: Array<{
    transactionNo: string, itemID: string, itemName: string, unit: string, unitName:
    string, in_TrxNo: string, lotNumber: string, quantity: number, remarks: string, minCount: number,
    remainingCount: number, tableExpDate: string, convertFactor: number
  }> = [];

  InventoryOutHeaderArray: IinventoryOutHeader = new IinventoryOutHeaderClass();
  checkTrxNumArray: IinventoryOutHeader[];

  formatDate: string;
  isDisabledDeleteButton: boolean = true;

  quan: number = 1;
  errormessage: string;
  status: string;
  expDate: string;
  isQuantityOver: string = '';
  tableExpDate: string;
  tableItemCount: string;
  quantityValid: boolean = false;
  dateTimeNow: string;
  ExpdateTimeNow: string;
  _trxno_snapshot: string;
  trxInputDisabled: boolean;
  verifyIcon: string;
  trxNumFunction: string;
  isApprover: boolean;

  constructor(public toastr: ToastrService, public builder: FormBuilder, public inventoryServices: InventorysServices,
    public cookieService: CookieService,
    public datepipe: DatePipe,
    public _activatedRoute: ActivatedRoute) {

    this.createHeaderForm();
    this.createDetailForm();
    this.createQuantityForm();

    this.createPendingForm();
    this.createIssuedForm();

  }
  ngOnInit() {
    this.loadData();
    this.userIDLogin = this.cookieService.get('userId');
    this.getIfApprover();
    this.Get_TrxNo_from_Dashboard();
  }

  public getIfApprover() {
    this.inventoryServices.getIfApprover(this.userIDLogin).subscribe((data) => {
      this.isApprover = data;
      
    })
  
  }
  public getDateTimeNow(): string {
    let today = new Date();
    let getDate = today.toISOString().slice(0, 10);
    let cutTime = today.toTimeString().slice(0, 5);
    let dateTime = getDate + "T" + cutTime;
    return dateTime;
  }

  loadData() {
    this.inventoryServices.getDepartmentList().subscribe((depList) => this.depListArray = depList);
    this.inventoryServices.getInventInDetail().subscribe((inventArray) => this.inventoryInArray = inventArray);
    this.inventoryServices.findPendingTrx().subscribe((pending) => this.inventoryPendingTrx = pending);
  }

  public createHeaderForm() {
    this.InventoryOutHeaderForm = this.builder.group({
      transactionNo: ['', Validators.required],
      transactionDate: [this.formatDate, Validators.required],
      department: ['', Validators.required],
      referenceNo: ['', Validators.required],
      remarks: ['']
    })
  }

  public createQuantityForm() {
    this.quantityForm = this.builder.group({
      quantityInput: ['']
    })
  }

  public createDetailForm() {
    this.InventoryOutDetailForm = this.builder.group({
      itemID: ['', Validators.required],
      unit: ['', Validators.required],
      in_TrxNo: ['', Validators.required],
      quantity: ['', Validators.required],
      remarks: [''],
    })
  }

  public createPendingForm() {
    this.findTrxPending = this.builder.group({
      pendingDateFrom: [''],
      pendingDateTo: [''],
      pendingRefNumber: ['']
    })
  }

  public createIssuedForm() {
    this.findTrxIssued = this.builder.group({
      issuedDateFrom: [''],
      issuedDateTo: [''],
      issuedRefNumber: ['']
    })
  }

  public Get_TrxNo_from_Dashboard() {
    this._trxno_snapshot = this._activatedRoute.snapshot.params["trxno"];
    if (this._trxno_snapshot == undefined) {
      console.log("snapshot params empty")
    } else {
      this.InventoryOutHeaderForm.controls.transactionNo.setValue(this._trxno_snapshot);
      this.getTrxNum()
    }
  }

  public clearTrxLookup(trxType) {
    if (trxType == "pending") {
      this.findTrxPending.reset();
    }
    else if (trxType == "issued") {
      this.findTrxIssued.reset();
    }
  }

  public searchTrx(type) {
     if (type == "pending") {
       this.inventoryServices.getPendingTrx().subscribe((pending) => {
         this.pendingTrxArray = pending;
         let PdateFrom = this.datepipe.transform(this.findTrxPending.controls.pendingDateFrom.value, 'yyyy-MM-dd');
         let PdateTo = this.datepipe.transform(this.findTrxPending.controls.pendingDateTo.value, 'yyyy-MM-dd');
         let PRef = this.findTrxPending.controls.pendingRefNumber.value;
         console.log(PdateFrom);
         console.log(PdateTo);
         if (PdateFrom == null || PdateTo == null) {
           this.pendingTrxArray = this.pendingTrxArray.filter((item) => item.referenceNo == PRef);
         }

         else if (PdateFrom != null && PdateTo != null && PRef == "") {
             this.pendingTrxArray = this.pendingTrxArray.filter((item) =>
               this.datepipe.transform(item.transactionDate, 'yyyy-MM-dd') >= PdateFrom
               && this.datepipe.transform(item.transactionDate, 'yyyy-MM-dd') <= PdateTo);
         }

         else {
             this.pendingTrxArray = this.pendingTrxArray.filter((item) =>
               this.datepipe.transform(item.issuedDate, 'yyyy-MM-dd') >= PdateFrom
               && this.datepipe.transform(item.issuedDate, 'yyyy-MM-dd') <= PdateTo)
               .filter(((item) => item.referenceNo == PRef));
         }
         })
     }
        
    else if (type == "issued") {
      this.inventoryServices.getIssuedTrx().subscribe((issued) => {
      this.issuedTrxArray = issued
      let IdateFrom = this.datepipe.transform(this.findTrxIssued.controls.issuedDateFrom.value, 'yyyy-MM-dd');
      let IdateTo = this.datepipe.transform(this.findTrxIssued.controls.issuedDateTo.value, 'yyyy-MM-dd');
        let IRef = this.findTrxIssued.controls.issuedRefNumber.value;

        if (IdateFrom == null || IdateTo == null) {
          this.issuedTrxArray = this.issuedTrxArray.filter((item) => item.referenceNo == IRef);
        }
        else if (IdateFrom != null && IdateTo != null && IRef == "") {
            this.issuedTrxArray = this.issuedTrxArray.filter((item) =>
              this.datepipe.transform(item.issuedDate, 'yyyy-MM-dd') >= IdateFrom
              && this.datepipe.transform(item.issuedDate, 'yyyy-MM-dd') <= IdateTo)
        }
        else {
            this.issuedTrxArray = this.issuedTrxArray.filter((item) =>
              this.datepipe.transform(item.issuedDate, 'yyyy-MM-dd') >= IdateFrom
              && this.datepipe.transform(item.issuedDate, 'yyyy-MM-dd') <= IdateTo)
              .filter((item) => item.referenceNo == IRef);
        }
      })
      } 
  }

  public tableRowClick(trxNum) {
    this.InventoryOutHeaderForm.controls.transactionNo.setValue(trxNum);
    this.getTrxNum();
  }

  public saveorIssueTransaction(status) {
    let trxNum = this.InventoryOutHeaderForm.controls.transactionNo.value;
    this.status = status;

    this.inventoryServices.findOutHeaderTrxNum(trxNum).subscribe((data) => {
      this.checkTrxNumArray = data;

      if (this.InventoryOutHeaderForm.valid) {
        if (trxNum != "") {
          if (this.checkTrxNumArray == undefined || this.checkTrxNumArray.length == 0) {
            this.getTrxNumFunction();
          }
          else {
            if (this.status == 'P') {
              this.saveOutDetails();
            }
            else {
              this.updateTrxtoIssued();
              this.resetPage();

              //load the notification here madafaka
            }
        }
          }
        else {
          this.toastr.info("Please Complete Important Fields")
        }
      }
    })
    this.InventoryOutHeaderForm.get('transactionDate').patchValue(this.getDateTimeNow());
  }

  public getTrxNumFunction() {
    this.inventoryServices.getTrxNumFunction().subscribe((data) => {
    this.trxNumFunction = data

      let trxNum = this.trxNumFunction.split('|');
      let type = trxNum[0].toString();
      let year = trxNum[1];
      let convertYear = this.datepipe.transform(new Date(), year.toLowerCase());
      let num = trxNum[2];

      this.InventoryOutHeaderForm.controls.transactionNo.setValue(type + convertYear + num);
      this.saveOutHeader();
      this.saveOutDetails();
      if (type == "P") {
        this.toastr.info("Transaction Saved");
      }
      else {
        this.toastr.info("Transaction Issued")
      }

    })  
  }

  public checkIfRemainingCountisValid() {
    this.inventoryServices.getTrxTable(this.getTrxHeaderForm().transactionNo).subscribe((data) => {
      if (confirm("Do you really want to Issue Item?")) {
        this.retrieveOutTable = data;
        setTimeout(() => {
          Array.from(this.retrieveOutTable).forEach((trx) => {
            if (trx.remainigCount / trx.itemMasterUnitConversion < trx.quantity) {
              this.verifyIcon = "fa-times";
            }
            else {
              this.verifyIcon = "fa-check";
              let minus = (trx.remainigCount) - (trx.itemMasterUnitConversion * trx.quantity);
              this.inventoryServices.updateRemaningCount(trx.in_TrxNo, String(minus)).subscribe(data => {
                this.toastr.info("Transaction" + " " + data + " " + "Issued");
              })
            }
          })
        }, 1000)
      }
      })
  }
  public getTrxHeaderForm() {
    this.InventoryOutHeaderArray.transactionNo = this.InventoryOutHeaderForm.controls.transactionNo.value;
    this.InventoryOutHeaderArray.transactionDate = this.InventoryOutHeaderForm.controls.transactionDate.value as Date;
    this.InventoryOutHeaderArray.issuedBy = this.userIDLogin;
    this.InventoryOutHeaderArray.issuedDate = this.InventoryOutHeaderForm.controls.transactionDate.value as Date;
    this.InventoryOutHeaderArray.receivedBy = this.userIDLogin;
    this.InventoryOutHeaderArray.department = this.InventoryOutHeaderForm.controls.department.value;
    this.InventoryOutHeaderArray.referenceNo = this.InventoryOutHeaderForm.controls.referenceNo.value;
    this.InventoryOutHeaderArray.remarks = this.InventoryOutHeaderForm.controls.remarks.value;
    this.InventoryOutHeaderArray.status = this.status;

    return this.InventoryOutHeaderArray;
  }

  //SAVE THE HEADER INFO TO DB
  public saveOutHeader()  {
    this.inventoryServices.insertOutHeader(this.getTrxHeaderForm()).subscribe(data => {
      },
        error => {
          this.errormessage = error.error;
          this.toastr.error(this.errormessage, "Error");
        }); 
  }

  //SAVE THE DETAILS INFO TO DB
  public saveOutDetails() {

    let trxNum = this.InventoryOutHeaderForm.controls.transactionNo.value;
    this.inventoryServices.deleteAllDetails(trxNum).subscribe(data => {

      Array.from(this.itemArrayDTL).forEach((array) => {
        this.inventDetailOutArray.transactionNo = this.InventoryOutHeaderForm.controls.transactionNo.value;
        this.inventDetailOutArray.itemID = array.itemID;
        this.inventDetailOutArray.unit = array.unit;
        this.inventDetailOutArray.in_TrxNo = array.in_TrxNo;
        this.inventDetailOutArray.quantity = array.quantity as number;
        this.inventDetailOutArray.remarks = array.remarks;
        this.inventDetailOutArray.minCount =array.minCount;

        this.inventoryServices.insertOutDetail(this.inventDetailOutArray).subscribe(data => {
          if (this.status == "P") {
            this.toastr.info("Transaction Saved");
            this.appSideBarMenu.ngOnInit();
          }
          else {
            this.checkIfRemainingCountisValid();
            this.appSideBarMenu.ngOnInit();
          }
        },
          error => {
            this.errormessage = error.error;
            this.toastr.error(this.errormessage, "Error");
            this.appSideBarMenu.ngOnInit();
          });
      })

    });
  }

  public updateTrxtoIssued() {
  this.inventoryServices.updatePendingTrx(this.getTrxHeaderForm()).subscribe(data => {

      this.checkIfRemainingCountisValid();
    
    });
  }

  // Get TRX from Table
  public getTrxNum() {
    let trxNum = this.getTrxHeaderForm().transactionNo;
    this.itemArrayDTL = [];
    this.InventoryOutDetailForm.reset();
    this.trxInputDisabled = false;
    this.InventoryOutDetailForm.controls.in_TrxNo.setValue('');
    if (trxNum != '') {
      this.isDisabledDeleteButton = false;
      this.trxInputDisabled = true;
      this.setTrxTable();
    }
  }

  //Generate Trx Number when form is touched
  public generateTrxNumandDate() {
      if (this.InventoryOutHeaderForm.controls.transactionNo.value == "") {
        let random = "T" + new Date().getFullYear() + new Date().getDate() + new Date().getTime();
        this.InventoryOutHeaderForm.controls.transactionNo.setValue("*********************************");
        this.InventoryOutHeaderForm.get('transactionDate').patchValue(this.getDateTimeNow());
        this.trxInputDisabled = true;
    }
  }

  //RETRIEVE TO TABLE THEN SET IT TO ARRAY FROM DB
  public setTrxTable() {
    this.dateTimeNow = this.getDateTimeNow();
    this.inventoryServices.getTrxTable(this.getTrxHeaderForm().transactionNo).subscribe((data) => { 
      this.retrieveOutTable = data;

      Array.from(this.retrieveOutTable).forEach((trx) => { 
        this.itemArrayDTL.push({
          transactionNo: trx.transactionNo,
          itemID: trx.itemID,
          itemName: trx.itemName,
          unit: trx.unit,
          unitName: trx.description,
          in_TrxNo: trx.in_TrxNo,
          lotNumber: trx.lotNumber,
          quantity: trx.quantity,
          remarks: trx.remarks,
          minCount: trx.minCount,
          remainingCount: parseFloat(Number(trx.remainigCount / trx.itemMasterUnitConversion).toFixed(2)),
          tableExpDate: String(trx.expirationDate),
          convertFactor: trx.itemMasterUnitConversion
        })
        this.tableItemCount = String(trx.remainigCount);
           
        this.checkTableItemQuantity();
        this.InventoryOutHeaderForm.get('transactionDate').patchValue(trx.transactionDate);
        this.InventoryOutHeaderForm.controls.department.setValue(trx.department);
        this.InventoryOutHeaderForm.controls.referenceNo.setValue(trx.referenceNo);
        this.InventoryOutHeaderForm.controls.remarks.setValue(trx.remarks);
      })
      
    })
  }

  // CHECK QUANTITY OF THE TABLE IF STILL VALID
  public checkTableItemQuantity() {
 
    this.quantityValid = false;

    Array.from(this.itemArrayDTL).forEach((array) => {
      if (array.remainingCount < array.quantity) {
        this.quantityValid = true;
      }
    })

  }

  //PUSH ARRAY TO TABLE
  public pushToArray() {
    let itemID = this.InventoryOutDetailForm.controls.itemID.value;
    let Name = this.InventoryOutDetailForm.controls.unit.value;
    let lot = this.InventoryOutDetailForm.controls.in_TrxNo.value;
    let unit = this.InventoryOutDetailForm.controls.unit.value;
    let unitCV = document.getElementById(unit).title;

    let item = document.getElementById(itemID).innerHTML;
    let unitName = document.getElementById(Name).innerHTML;
    let lotNum = document.getElementById(lot).innerHTML;
    this.itemArrayDTL.push({
      transactionNo: this.InventoryOutHeaderForm.controls.transactionNo.value,
      itemID: item,
      itemName: this.InventoryOutDetailForm.controls.itemID.value,
      unit: unitName,
      unitName: this.InventoryOutDetailForm.controls.unit.value,
      in_TrxNo: lotNum,
      lotNumber: this.InventoryOutDetailForm.controls.in_TrxNo.value,
      quantity: this.InventoryOutDetailForm.controls.quantity.value,
      remarks: this.InventoryOutDetailForm.controls.remarks.value,
      minCount: this.InventoryOutDetailForm.controls.quantity.value * Number(unitCV),
      remainingCount: this.remainingCount,
      tableExpDate: null,
      convertFactor: Number(unitCV)
    })
  }

  public addItem() {
    let itemID = this.InventoryOutDetailForm.controls.itemID.value;
    let lot = this.InventoryOutDetailForm.controls.in_TrxNo.value;

    let result = this.itemArrayDTL.find(({ itemName }) => itemName === itemID);

    let item = document.getElementById(itemID).innerHTML;
    let lotNum = document.getElementById(lot).innerHTML;

    if (result == undefined) {
      this.pushToArray();
    }

    else if (result != undefined) {
      let findID = result.itemID;
      let findLotNum = result.in_TrxNo;
      if (findID != item || findLotNum != lotNum) {
        this.pushToArray();
      }

      else {
        this.toastr.error("Item is Already Entered");
      }
    }
    this.InventoryOutDetailForm.reset();
    this.unitArray = [];
    this.lotNumArray = [];
    this.remainingCount = null;
    this.InventoryOutDetailForm.controls.in_TrxNo.setValue('');
  }

  public removeItem(id, unit, lotNum) {
    if (confirm("Are you sure you want to Remove" + " " + id + "?")) {
      this.itemArrayDTL = this.itemArrayDTL.filter(({ itemID }) => itemID !== id)
        .filter(({ itemName }) => itemName !== unit)
        .filter(({ lotNumber }) => lotNumber !== lotNum);
        ;
    }
  }

  public deleteTransaction() {
    if (this.InventoryOutHeaderForm.controls.transactionNo.value != "") {
      if (confirm("Are you sure you want to delete this transaction?")) {
        let trxNum = this.InventoryOutHeaderForm.controls.transactionNo.value;

        this.inventoryServices.deleteAllHeader(trxNum).subscribe(data => {
          this.inventoryServices.deleteAllDetails(trxNum).subscribe(data => {
            this.cookieService.delete("H" + trxNum);
            this.cookieService.delete("D" + trxNum);

            this.InventoryOutHeaderForm.reset();
            this.itemArrayDTL = [];

            this.createHeaderForm();
            this.createDetailForm();
            this.loadData();
            this.InventoryOutHeaderForm.get('transactionDate').patchValue(this.getDateTimeNow());
            this.toastr.info("Transaction Deleted");
          })
        })
      
      }
    }
  
  }
  //HMTL ONCHANGE CHECK UNIT
  public checkUnit() {
    if (this.InventoryOutHeaderForm.valid) {
      let itemID = this.InventoryOutDetailForm.controls.itemID.value;
      if (itemID == '') {
        this.unitArray = [];
        this.InventoryOutDetailForm.controls.unit.setValue('');
        this.lotNumArray = [];
        this.InventoryOutDetailForm.controls.in_TrxNo.setValue('');
      }

      this.unitArray = [];
      this.lotNumArray = [];
      let itemName = this.InventoryOutDetailForm.controls.itemID.value;
      let itemIDs = document.getElementById(itemName).innerHTML;
      this.inventoryServices.getJoinUnitAndITMU(itemIDs).subscribe((unit) => this.unitArray = unit);
    }
    else {
      this.InventoryOutDetailForm.controls.itemID.setValue('');
      this.toastr.warning("Please Complete Important Fields in Transaction Header First")
    }
  
  }

  //HTML ONCHANGE OF CHECK LOT NUMBER
  public checkLotNum() {
    if (this.InventoryOutHeaderForm.valid) {
      this.isQuantityOver = '';
      this.lotNumArray = [];
      let itemName = this.InventoryOutDetailForm.controls.itemID.value;
      let itemIDs = document.getElementById(itemName).innerHTML;
      let unit = this.InventoryOutDetailForm.controls.unit.value;
      let unitName = document.getElementById(unit).innerHTML;

      this.inventoryServices.getLotNum(itemIDs, unitName).subscribe((lotNUm) => this.lotNumArray = lotNUm);
    }
      else {
      this.InventoryOutDetailForm.controls.unit.setValue('');
      this.toastr.warning("Please Complete Important Fields in Transaction Header First");
      }
  }

  //FOR CHECKING REMAINING COUNT
  public checkRemainingCount() {
    let itemName = this.InventoryOutDetailForm.controls.itemID.value;
    let itemID = document.getElementById(itemName).innerHTML;
    let unit = this.InventoryOutDetailForm.controls.unit.value;
    let unitName = document.getElementById(unit).innerHTML;
    let cvFactor = document.getElementById(unit).title;

    let lotNum = this.InventoryOutDetailForm.controls.in_TrxNo.value;
    this.inventoryServices.getRemainingCount(itemID, lotNum).subscribe((count) => {
      this.Count = count;
      Array.from(this.Count).forEach((data) => {
        let rC = Number(data.remainigCount) / Number(cvFactor);
        this.remainingCount = parseFloat(rC.toFixed(2));
      })

      Array.from(this.Count).forEach((expDate) => {
        let getExpDate = String(expDate.expirationDate);
        let convertDate = this.datepipe.transform(getExpDate, 'MMMM d, y')
        if (getExpDate < this.getDateTimeNow()) {
          this.expDate = convertDate + " " + "EXPIRED";
         
        }
      })
    });


  }

  public checkInputQuantity() {
    this.isQuantityOver = '';
    let quantity = this.InventoryOutDetailForm.controls.quantity.value;
      if (quantity > this.remainingCount) {
        this.isQuantityOver = 'Yes';
    }
   
  }

  public minusQuantity(itemID,unit,lotNum) {
   let quantity = this.itemArrayDTL.find(array => array.itemID === itemID && array.unitName === unit && array.lotNumber === lotNum).quantity;
    var minus = quantity - this.quan;
    this.itemArrayDTL.find(array => array.itemID === itemID && array.unitName === unit && array.lotNumber === lotNum).quantity = minus;

    this.checkTableItemQuantity();

    }

  public addQuantity(itemID, unit, lotNum) {
    let quantity = this.itemArrayDTL.find(array => array.itemID === itemID && array.unitName === unit && array.lotNumber === lotNum).quantity;
    var add = quantity + this.quan;
    this.itemArrayDTL.find(array => array.itemID === itemID && array.unitName === unit && array.lotNumber === lotNum).quantity = add;

    this.checkTableItemQuantity();

  }

  public tableQuantityChanges(itemID, unit, lotNum) {
  
    let quantity = parseInt((<HTMLInputElement>document.getElementById(itemID)).value);
    this.itemArrayDTL.find(array => array.itemID === itemID && array.unitName === unit && array.lotNumber === lotNum).quantity = Number(quantity);
    this.checkTableItemQuantity();
  }

  public resetDetials() {
    this.remainingCount = null;
    this.InventoryOutDetailForm.reset();
    this.InventoryOutDetailForm.controls.in_TrxNo.setValue('');
  }

  public resetPage() {
    this.InventoryOutDetailForm.reset();
    this.InventoryOutHeaderForm.reset();
    this.createHeaderForm();
    this.createDetailForm();
    this.trxInputDisabled = false;
    this.InventoryOutHeaderForm.get('transactionDate').patchValue(this.getDateTimeNow());
    this.itemArrayDTL = [];
    this.remainingCount = null;
    this.InventoryOutDetailForm.controls.in_TrxNo.setValue('');
   
  }

}
