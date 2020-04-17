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

  inventoryInArray: IInventoryInTrxDetail[];
  unitArray: IiTemMasterUnitJoinUnit[];
  lotNumArray: IInventoryInTrxDetail[];

  inventHeadOutArray: IinventoryOutHeader = new IinventoryOutHeaderClass();
  inventDetailOutArray: IinventoryOutDetail = new IinventoryOutDetailClass();

  itemArrayDTL: Array<{ transactionNo: string, itemID: string, unit: string, in_TrxNo: string, quantity: number, remarks: string }> = [];

  InventoryOutHeaderArray: IinventoryOutHeader = new IinventoryOutHeaderClass();
  checkTrxNumArray: IinventoryOutHeader[];

  formatDate: string;
  isDisabledDeleteButton: boolean = true;

  errormessage: string;

  constructor(private toastr: ToastrService, private builder: FormBuilder, private inventoryServices: InventorysServices,
    private cookieService: CookieService) {
    this.createHeaderForm();
    this.createDetailForm();
    this.createQuantityForm();

  }
   ngOnInit() {
    this.loadData();
     this.getAllTrxNumFromCookies();
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
  }

  private createHeaderForm() {
    this.InventoryOutHeaderForm = this.builder.group({
      transactionNo: [''],
      transactionDate: [this.formatDate],
      department: [''],
      referenceNo: [''],
      remarks: ['']
    })
    this.getDateTimeNow();
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
      remarks: ['', Validators.required],

    })
  }
  private issueTransaction() {

 
  }

  private getAllTrxNumFromCookies() {
    let cookiearray = this.cookieService.getAll();

  }

  private saveTransaction() {
    let trxNum = this.InventoryOutHeaderForm.controls.transactionNo.value;

    this.inventoryServices.findOutHeaderTrxNum(trxNum).subscribe((data) => this.checkTrxNumArray = data)
    if (this.InventoryOutHeaderForm.valid) {
      if (this.checkTrxNumArray.length == 0) {
        this.saveOutHeader();
        this.saveOutDetails();
      }
      else {
        this.saveOutDetails();
      }

    }
    
  //let trxNum =  this.InventoryOutHeaderForm.controls.transactionNo.value;
  //  this.cookieService.set("D" + trxNum, JSON.stringify(this.itemArrayDTL),365);
  //  this.cookieService.set("H" + trxNum, JSON.stringify(this.InventoryOutHeaderForm.value),365);
  //  this.toastr.info("Transaction Save Temporarily", "Saved");

  //  if (trxNum != "") {
  //    this.isDisabledDeleteButton = false;

  //  }

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
    this.InventoryOutHeaderArray.status = "P";

   
      this.inventoryServices.insertOutHeader(this.InventoryOutHeaderArray).subscribe(data => {
        this.toastr.success("Transaction Header Saved", "Saved");

      },
        error => {
          this.errormessage = error.error;
          this.toastr.error(this.errormessage, "Error");
        });
      this.InventoryOutHeaderForm.reset();
    
  }


  private saveOutDetails() {
    Array.from(this.itemArrayDTL).forEach((data) => {
      this.inventDetailOutArray.transactionNo = data.transactionNo;
      this.inventDetailOutArray.itemID = data.itemID;
      this.inventDetailOutArray.unit = data.unit;
      this.inventDetailOutArray.in_TrxNo = data.in_TrxNo;
      this.inventDetailOutArray.quantity = data.quantity;
      this.inventDetailOutArray.remarks = data.remarks;
      this.inventDetailOutArray.minCount = "100";
    })

    this.inventoryServices.insertOutDetail(this.inventDetailOutArray).subscribe(data => {
      this.toastr.success("Transaction Detail Saved", "Saved");

    },
      error => {
        this.errormessage = error.error;
        this.toastr.error(this.errormessage, "Error");
      });
    this.InventoryOutHeaderForm.reset();
  }

  private getTrxNum() {
    
    let trxNum = this.InventoryOutHeaderForm.controls.transactionNo.value;
    this.itemArrayDTL = JSON.parse(this.cookieService.get("D" + trxNum));
    this.inventHeadOutArray = JSON.parse(this.cookieService.get("H" + trxNum));

    this.InventoryOutHeaderForm.get('transactionDate').patchValue(this.inventHeadOutArray.transactionDate); 
    this.InventoryOutHeaderForm.controls.department.setValue(this.inventHeadOutArray.department);
    this.InventoryOutHeaderForm.controls.referenceNo.setValue(this.inventHeadOutArray.referenceNo);
    this.InventoryOutHeaderForm.controls.remarks.setValue(this.inventHeadOutArray.remarks);

    if (trxNum != "") {
      this.isDisabledDeleteButton = false;

    }
  }

  private checkTrxNum() {
    if (this.InventoryOutHeaderForm.valueChanges) {
      if (this.InventoryOutHeaderForm.controls.transactionNo.value == "") {
        let random = "T" + new Date().getFullYear() + new Date().getDate() + new Date().getTime();
        this.InventoryOutHeaderForm.controls.transactionNo.setValue(random);
      }
    }
  }

  private addItem() {

    let itemID = this.InventoryOutDetailForm.controls.itemID.value;
    let findItem = this.itemArrayDTL.find(({ itemID }) => itemID === itemID);

    if (!findItem) {
      this.itemArrayDTL.push({
        transactionNo: this.InventoryOutHeaderForm.controls.transactionNo.value, itemID: this.InventoryOutDetailForm.controls.itemID.value, unit: this.InventoryOutDetailForm.controls.unit.value
        , in_TrxNo: this.InventoryOutDetailForm.controls.in_TrxNo.value, quantity: this.InventoryOutDetailForm.controls.quantity.value,
        remarks: this.InventoryOutDetailForm.controls.remarks.value
      });
     
    }
    else {
      this.toastr.error("Item is Already Entered");
    }
    this.InventoryOutDetailForm.reset();


  }

  private removeItem(id) {
    if (confirm("Are you sure you want to Remove" + " " + id + "?")) {
      this.itemArrayDTL = this.itemArrayDTL.filter(({ itemID }) => itemID !== id);
    }
  }

  private saveQuantityChanges(itemID) {
   let quantity = this.quantityForm.controls.quantityInput.value;
    this.itemArrayDTL.find(({ itemID }) => itemID == itemID).quantity = quantity;
  }

  private deleteTransaction() {
    if (this.InventoryOutHeaderForm.controls.transactionNo.value != "") {
      if (confirm("Are you sure you want to delete this transaction?")) {
        let trxNum = this.InventoryOutHeaderForm.controls.transactionNo.value;
        this.cookieService.delete("H" + trxNum);
        this.cookieService.delete("D" + trxNum);

        this.InventoryOutHeaderForm.reset();
        this.itemArrayDTL = [];

        this.createHeaderForm();
        this.createDetailForm();
        this.getDateTimeNow();
      }
    }
  
  }
  //HMTL ONCHANGE ITEM ID
  private checkUnit() {
    let itemID =  this.InventoryOutDetailForm.controls.itemID.value;
    this.inventoryServices.getJoinUnitAndITMU(itemID).subscribe((unit) => this.unitArray = unit);
  }

  //HTML ONCHANGE OF UNIT
  private checkLotNum() {
    let itemID = this.InventoryOutDetailForm.controls.itemID.value;
    let unit = this.InventoryOutDetailForm.controls.unit.value;
    this.inventoryServices.getLotNum(itemID, unit).subscribe((lotNUm) => {
      this.lotNumArray = lotNUm;

      Array.from(this.lotNumArray).forEach((data) => {
        console.log(data.expirationDate);
      })
    });


  }

  private minusQuantity() {
    //let quantity = this.itemArrayDTL.find(({ Item }) => Item == item).Quantity;
    //let quantity = this.quantityForm.controls.quantityInput.value;
    //quantity --;
    //this.quantityForm.controls.quantityInput.setValue(quantity);

   
  }

  private addQuantity() {
    //let quantity = this.itemArrayDTL.find(({ Item }) => Item == item).Quantity;
    //quantity ++;
    //this.quantityForm.controls.quantityInput.setValue(quantity);
  }
}
