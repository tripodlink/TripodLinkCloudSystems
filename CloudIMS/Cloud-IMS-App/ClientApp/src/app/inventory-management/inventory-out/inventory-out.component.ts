import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { CookieService } from 'ngx-cookie-service';
import { ToastrService } from 'ngx-toastr';
import { IDepartment } from '../../classes/data-dictionary/Department/IDepartment.interface';
import { IiTemMasterUnitJoinUnit } from '../../classes/data-dictionary/ItemMasterUnit/IitemMasterUnitJoinUnit.interface';
import { IinventoryOutDetail } from '../../classes/inventory-management/inventory-out/IinventoryOutDetail.interface';
import { IinventoryOutDetailClass } from '../../classes/inventory-management/inventory-out/IinventoryOutDetailClass';
import { IinventoryOutHeader } from '../../classes/inventory-management/inventory-out/IinventoryOutHeader.interface';
import { IinventoryOutHeaderClass } from '../../classes/inventory-management/inventory-out/IinventoryOutHeaderClass';
import { IInventoryInTrxDetail } from '../../classes/inventory-management/InventoryIn/IInventoryInTrxDetail.interface';
import { InventorysServices } from '../../services/inventorys.service';
import { IInventoryInTrxDetailClass } from '../../classes/inventory-management/InventoryIn/IInventoryInTrxDetailClass';
import { element } from 'protractor';
import { NumericLiteral } from 'typescript';
@Component({
  selector: 'app-inventory-out',
  templateUrl: './inventory-out.component.html',
  styleUrls: ['./inventory-out.component.css']
})
export class InventoryOutComponent implements OnInit {

  userIDLogin: string;

  InventoryOutHeaderForm: FormGroup;
  InventoryOutDetailForm: FormGroup;
  quantityForm: FormGroup;
  depListArray: IDepartment[];

  inventoryPendingTrx: IinventoryOutHeader[];
  inventoryInArray: IInventoryInTrxDetail[];
  unitArray: IiTemMasterUnitJoinUnit[];
  lotNumArray: IInventoryInTrxDetail[];
  Count: IInventoryInTrxDetail[];
  remainingCount: number;

  inventHeadOutArray: IinventoryOutHeader = new IinventoryOutHeaderClass();
  inventDetailOutArray: IinventoryOutDetail = new IinventoryOutDetailClass();

  itemArrayDTL: Array<{ transactionNo: string, itemID: string, itemName:string, unit: string, unitName: string, in_TrxNo: string, lotNumber: string, quantity: number, remarks: string }> = [];

  InventoryOutHeaderArray: IinventoryOutHeader = new IinventoryOutHeaderClass();
  checkTrxNumArray: IinventoryOutHeader[];

  formatDate: string;
  isDisabledDeleteButton: boolean = true;

  quan: number = 1;
  errormessage: string;
  status: string;

  constructor(private toastr: ToastrService, private builder: FormBuilder, private inventoryServices: InventorysServices,
    private cookieService: CookieService) {
    this.createHeaderForm();
    this.createDetailForm();
    this.createQuantityForm();

  }
   ngOnInit() {
    this.loadData();
     this.userIDLogin = this.cookieService.get('userId');
  }
  private getDateTimeNow() {
    let today = new Date();
    let getDate = today.toISOString().slice(0, 10);
    let cutTime = today.toTimeString().slice(0, 5);
    let dateTime = getDate + "T" + cutTime;

    this.InventoryOutHeaderForm.get('transactionDate').patchValue(dateTime);
  }

  loadData() {
    this.inventoryServices.getDepartmentList().subscribe((depList) => this.depListArray = depList);
    this.inventoryServices.getInventInDetail().subscribe((inventArray) => this.inventoryInArray = inventArray);
    this.inventoryServices.findPendingTrx().subscribe((pending) => this.inventoryPendingTrx = pending);
  }

  private createHeaderForm() {
    this.InventoryOutHeaderForm = this.builder.group({
      transactionNo: ['', Validators.required],
      transactionDate: [this.formatDate, Validators.required],
      department: ['', Validators.required],
      referenceNo: ['', Validators.required],
      remarks: ['']
    })
  }

  private createQuantityForm() {
    this.quantityForm = this.builder.group({
      quantityInput: ['']
    })
  }

  private createDetailForm() {
    this.InventoryOutDetailForm = this.builder.group({
      itemID: ['', Validators.required],
      unit: ['', Validators.required],
      in_TrxNo: ['', Validators.required],
      quantity: ['', Validators.required],
      remarks: [''],
    })
  }

  private issueTransaction() {

 
  }

  private saveorIssueTransaction(status) {
    let trxNum = this.InventoryOutHeaderForm.controls.transactionNo.value;
    let itemID = this.InventoryOutDetailForm.controls.itemID.value;
    this.cookieService.set("D" + trxNum, JSON.stringify(this.itemArrayDTL), 365);
    this.cookieService.set("H" + trxNum, JSON.stringify(this.InventoryOutHeaderForm.value), 365);

    this.status = status;

    this.inventoryServices.findOutHeaderTrxNum(trxNum).subscribe((data) => {
      this.checkTrxNumArray = data;

      if (this.InventoryOutHeaderForm.valid) {
        if (trxNum != "" || itemID != "")
          if (this.checkTrxNumArray == undefined || this.checkTrxNumArray.length == 0) {
            this.saveOutHeader();
            this.saveOutDetails();
            if (this.status == 'P') {
              this.toastr.success("Transaction Saved");
            }
            else {
              this.toastr.show('Transaction Issued');
            }
          }
          else {
            if (this.status == 'P') {
              this.saveOutDetails();
            }
            else {
              this.updateTrxtoIssued();
            }
            this.toastr.success("Transaction Saved");
          }
        else {
          this.toastr.info("Please Complete Important Fields")
        }
      }
    })
    this.getDateTimeNow();
  }

  private saveOutHeader() {
    this.InventoryOutHeaderArray.transactionNo = this.InventoryOutHeaderForm.controls.transactionNo.value;
    this.InventoryOutHeaderArray.transactionDate = this.InventoryOutHeaderForm.controls.transactionDate.value as Date;
    this.InventoryOutHeaderArray.issuedBy = this.userIDLogin;
    this.InventoryOutHeaderArray.issuedDate = this.InventoryOutHeaderForm.controls.transactionDate.value as Date;
    this.InventoryOutHeaderArray.receivedBy = this.userIDLogin;
    this.InventoryOutHeaderArray.department = this.InventoryOutHeaderForm.controls.department.value;
    this.InventoryOutHeaderArray.referenceNo = this.InventoryOutHeaderForm.controls.referenceNo.value;
    this.InventoryOutHeaderArray.remarks = this.InventoryOutHeaderForm.controls.remarks.value;
    this.InventoryOutHeaderArray.status = this.status;

      this.inventoryServices.insertOutHeader(this.InventoryOutHeaderArray).subscribe(data => {

      },
        error => {
          this.errormessage = error.error;
          this.toastr.error(this.errormessage, "Error");
        }); 
  }

  private saveOutDetails() {

    let trxNum = this.InventoryOutHeaderForm.controls.transactionNo.value;
    this.inventoryServices.deleteAllDetails(trxNum).subscribe(data => {

      Array.from(this.itemArrayDTL).forEach((array) => {
        this.inventDetailOutArray.transactionNo = array.transactionNo;
        this.inventDetailOutArray.itemID = array.itemID;
        this.inventDetailOutArray.unit = array.unit;
        this.inventDetailOutArray.in_TrxNo = array.in_TrxNo;
        this.inventDetailOutArray.quantity = array.quantity as number;
        this.inventDetailOutArray.remarks = array.remarks;
        this.inventDetailOutArray.minCount = 100;

        this.inventoryServices.insertOutDetail(this.inventDetailOutArray).subscribe(data => {
        

        },
          error => {
            this.errormessage = error.error;
            this.toastr.error(this.errormessage, "Error");
          });
      })

    });
  }

  private updateTrxtoIssued() {
    this.InventoryOutHeaderArray.transactionNo = this.InventoryOutHeaderForm.controls.transactionNo.value;
    this.InventoryOutHeaderArray.transactionDate = this.InventoryOutHeaderForm.controls.transactionDate.value as Date;
    this.InventoryOutHeaderArray.issuedBy = this.userIDLogin;
    this.InventoryOutHeaderArray.issuedDate = this.InventoryOutHeaderForm.controls.transactionDate.value as Date;
    this.InventoryOutHeaderArray.receivedBy = this.userIDLogin;
    this.InventoryOutHeaderArray.department = this.InventoryOutHeaderForm.controls.department.value;
    this.InventoryOutHeaderArray.referenceNo = this.InventoryOutHeaderForm.controls.referenceNo.value;
    this.InventoryOutHeaderArray.remarks = this.InventoryOutHeaderForm.controls.remarks.value;
    this.InventoryOutHeaderArray.status = this.status;
    this.inventoryServices.updatePendingTrx(this.InventoryOutHeaderArray).subscribe(data => {
      this.toastr.show('Transaction Issued');
      this.resetPage();
    });
  }

  private getTrxNum() {
    
    let trxNum = this.InventoryOutHeaderForm.controls.transactionNo.value;
    this.itemArrayDTL = JSON.parse(this.cookieService.get("D" + trxNum));
    this.inventHeadOutArray = JSON.parse(this.cookieService.get("H" + trxNum));

    this.InventoryOutHeaderForm.get('transactionDate').patchValue(this.inventHeadOutArray.transactionDate); 
    this.InventoryOutHeaderForm.controls.department.setValue(this.inventHeadOutArray.department);
    this.InventoryOutHeaderForm.controls.referenceNo.setValue(this.inventHeadOutArray.referenceNo);
    this.InventoryOutHeaderForm.controls.remarks.setValue(this.inventHeadOutArray.remarks);

    if (trxNum != '') {
      this.isDisabledDeleteButton = false;

    }
  }

  private generateTrxNumandDate() {
    if (this.InventoryOutHeaderForm.valueChanges) {
      if (this.InventoryOutHeaderForm.controls.transactionNo.value == "") {
        let random = "T" + new Date().getFullYear() + new Date().getDate() + new Date().getTime();
        this.InventoryOutHeaderForm.controls.transactionNo.setValue(random);
        this.getDateTimeNow();
      }
    }
  }

  private addItem() {


    let itemID = this.InventoryOutDetailForm.controls.itemID.value;
    let result = this.itemArrayDTL.find(({ itemName }) => itemName === itemID);

    let Name = this.InventoryOutDetailForm.controls.unit.value;
    let lot = this.InventoryOutDetailForm.controls.in_TrxNo.value;

    let item = document.getElementById(itemID).innerHTML;
    let unitName = document.getElementById(Name).innerHTML;
    let lotNum = document.getElementById(lot).innerHTML;

    if (result == undefined) {
      this.itemArrayDTL.push({
        transactionNo: this.InventoryOutHeaderForm.controls.transactionNo.value,
        itemID: item ,
        itemName: this.InventoryOutDetailForm.controls.itemID.value,
        unit: unitName,
        unitName: this.InventoryOutDetailForm.controls.unit.value,
        in_TrxNo: lotNum,
        lotNumber: this.InventoryOutDetailForm.controls.in_TrxNo.value,
        quantity: this.InventoryOutDetailForm.controls.quantity.value,
        remarks: this.InventoryOutDetailForm.controls.remarks.value
      })
    }

    else if (result != undefined) {
      let findID = result.itemID;
      let findUnit = result.in_TrxNo;
      if (findID != item || findUnit != lotNum) {
        this.itemArrayDTL.push({
          transactionNo: this.InventoryOutHeaderForm.controls.transactionNo.value,
          itemID: item,
          itemName: this.InventoryOutDetailForm.controls.itemID.value,
          unit: unitName,
          unitName: this.InventoryOutDetailForm.controls.unit.value,
          in_TrxNo: lotNum,
          lotNumber: this.InventoryOutDetailForm.controls.in_TrxNo.value,
          quantity: this.InventoryOutDetailForm.controls.quantity.value,
          remarks: this.InventoryOutDetailForm.controls.remarks.value
        })
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

  private removeItem(id) {
    if (confirm("Are you sure you want to Remove" + " " + id + "?")) {
      this.itemArrayDTL = this.itemArrayDTL.filter(({ itemID }) => itemID !== id);
    }
  }

  private deleteTransaction() {
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
            this.getDateTimeNow();
            this.toastr.info("Transaction Deleted");
          })
        })
      
      }
    }
  
  }
  //HMTL ONCHANGE CHECK UNIT
  private checkUnit() {
    this.unitArray = [];
    this.lotNumArray = [];
    let itemName = this.InventoryOutDetailForm.controls.itemID.value;
    let itemID = document.getElementById(itemName).innerHTML;
    this.inventoryServices.getJoinUnitAndITMU(itemID).subscribe((unit) => this.unitArray = unit);

  }

  //HTML ONCHANGE OF CHECK LOT NUMBER
  private checkLotNum() {
    this.lotNumArray = [];
    let itemName = this.InventoryOutDetailForm.controls.itemID.value;
    let itemID = document.getElementById(itemName).innerHTML;
    let unit = this.InventoryOutDetailForm.controls.unit.value;
    let unitName = document.getElementById(unit).innerHTML;
    this.inventoryServices.getLotNum(itemID, unitName).subscribe((lotNUm) =>this.lotNumArray = lotNUm);

  }

  private checkRemainingCount() {
    let itemName = this.InventoryOutDetailForm.controls.itemID.value;
    let itemID = document.getElementById(itemName).innerHTML;
    let unit = this.InventoryOutDetailForm.controls.unit.value;
    let unitName = document.getElementById(unit).innerHTML;

    let lotNum = this.InventoryOutDetailForm.controls.in_TrxNo.value;
    this.inventoryServices.getRemainingCount(itemID, unitName, lotNum).subscribe((count) => {
      this.Count = count;
      Array.from(this.Count).forEach((data) => {
        this.remainingCount = data.remainigCount;
      })
    });


  }

 private InputcheckItemIfNUll() {
   let itemID = this.InventoryOutDetailForm.controls.itemID.value;
   if (itemID == '') {
     this.unitArray = [];
     this.InventoryOutDetailForm.controls.unit.setValue('');
     this.lotNumArray = [];
     this.InventoryOutDetailForm.controls.in_TrxNo.setValue('');
   }
  }

  private checkInputQuantity() {
    let quantity = this.InventoryOutDetailForm.controls.quantity.value;
    if (quantity > this.remainingCount) {
      this.InventoryOutDetailForm.controls.quantity.setValue(this.remainingCount);
      this.toastr.error("Quantity Cannot Exceed from the remaining Count")
    }
  }

  private saveQuantityChanges(itemID) {
  
  }

  private minusQuantity(itemID) {
  let quantity = this.itemArrayDTL.find(({ itemID }) => itemID == itemID).quantity;
  var minus =  quantity - this.quan;
    this.quantityForm.controls.quantityInput.setValue(minus);
    this.itemArrayDTL.find(({ itemID }) => itemID == itemID).quantity = minus;
   
  }

  private addQuantity(itemID) {
    let quantity = this.itemArrayDTL.find(({ itemID }) => itemID == itemID).quantity;
    var minus = quantity + this.quan;
    this.quantityForm.controls.quantityInput.setValue(minus);
    this.itemArrayDTL.find(({ itemID }) => itemID == itemID).quantity = minus;
  }

  private resetDetials() {
    this.remainingCount = null;
    this.InventoryOutDetailForm.reset();
    this.InventoryOutDetailForm.controls.in_TrxNo.setValue('');
  }
  private resetPage() {
    this.InventoryOutDetailForm.reset();
    this.InventoryOutHeaderForm.reset();
    this.createHeaderForm();
    this.createDetailForm();
    this.getDateTimeNow();
    this.itemArrayDTL = [];
    this.remainingCount = null;
    this.InventoryOutDetailForm.controls.in_TrxNo.setValue('');
   
  }

}
