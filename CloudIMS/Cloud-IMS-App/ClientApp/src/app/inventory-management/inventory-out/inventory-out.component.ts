import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { FormBuilder, FormControl, FormGroup, Validators } from "@angular/forms"; 
import { ViewChild, ElementRef } from '@angular/core';
import { InventoryOutHeaderServices } from '../../services/inventoryoutheader.service';
import { IinventoryOutHeaderClass  } from '../../classes/inventory-management/inventory-out/IitemGroupClass';
import { IinventoryOutHeader } from '../../classes/inventory-management/inventory-out/IitemGroup.interface';
import { IDepartment } from '../../classes/data-dictionary/Department/IDepartment.interface';


@Component({
  selector: 'app-inventory-out',
  templateUrl: './inventory-out.component.html'
})
export class InventoryOutComponent implements OnInit {
  
  InventoryOutHeaderForm: FormGroup;
  InventoryOutDetail: FormGroup;

  depListArray: IDepartment[];

  itemArrrayDTL: Array<{ Item: string, Unit: string, LotNum: string, Quantity: number, Remarks: string }> = [];

  InventoryOutHeaderArray: IinventoryOutHeader = new IinventoryOutHeaderClass();

  formatDate: string;
  isNewTrx: boolean;


  constructor(private toastr: ToastrService, private builder: FormBuilder, private inventoryOutServices: InventoryOutHeaderServices) {
    this.createHeaderForm();
    this.createDetialForm();

  }
  ngOnInit() {
    this.loadData();
  }

  loadData() {
    this.inventoryOutServices.getDepartmentList().subscribe((depList) => this.depListArray = depList);
  }

  private createHeaderForm() {
    let today = new Date();
    let getDate = today.toISOString().slice(0, 10);
    let cutTime = today.toTimeString().slice(0, 5);
    let dateTime = getDate + "T" + cutTime;

    this.InventoryOutHeaderForm = this.builder.group({
      transactionNo: [''],
      transactionDate: [this.formatDate],
      department: [''],
      referenceNo: [''],
      remarks: ['']
    })
    this.InventoryOutHeaderForm.get('transactionDate').patchValue(dateTime);
  }

  private createDetialForm() {
    this.InventoryOutDetail = this.builder.group({
      itemID: ['', Validators.required],
      unit: ['', Validators.required],
      in_TrxNo: ['', Validators.required],
      quantity: ['', Validators.required],
      remarks: ['', Validators.required],

    })
  }
  private saveTransaction() {
    let errormessage;
    this.InventoryOutHeaderArray.transactionNo = this.InventoryOutHeaderForm.controls.transactionNo.value;
    this.InventoryOutHeaderArray.transactionDate = this.InventoryOutHeaderForm.controls.transactionDate.value as Date;
    this.InventoryOutHeaderArray.issuedBy = 'SYSAD';
    this.InventoryOutHeaderArray.issuedDate = this.InventoryOutHeaderForm.controls.transactionDate.value as Date;
    this.InventoryOutHeaderArray.receivedBy = 'SYSAD';
    this.InventoryOutHeaderArray.department = this.InventoryOutHeaderForm.controls.department.value;
    this.InventoryOutHeaderArray.referenceNo = this.InventoryOutHeaderForm.controls.referenceNo.value;
    this.InventoryOutHeaderArray.remarks = this.InventoryOutHeaderForm.controls.remarks.value;

    this.inventoryOutServices.insertItemGroup(this.InventoryOutHeaderArray).subscribe(data => {
      this.toastr.success("Transaction Saved", "Saved");

    },
      error => {
        errormessage = error.error;
        this.toastr.error(errormessage, "Error");
      });
    this.InventoryOutHeaderForm.reset();
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

    let itemID = this.InventoryOutDetail.controls.itemID.value;
    let findItem = this.itemArrrayDTL.find(({ Item }) => Item === itemID);

    if (!findItem) {
      this.itemArrrayDTL.push({
        Item: this.InventoryOutDetail.controls.itemID.value, Unit: this.InventoryOutDetail.controls.unit.value
        , LotNum: this.InventoryOutDetail.controls.in_TrxNo.value, Quantity: this.InventoryOutDetail.controls.quantity.value,
        Remarks: this.InventoryOutDetail.controls.remarks.value
      });
     
    }
    else {
      this.toastr.error("Item is Already Entered");
    }
    this.InventoryOutDetail.reset();
  }

  private removeItem(id) {
    if (confirm("Are you sure you want to Remove" + " " + id)) {
      this.itemArrrayDTL = this.itemArrrayDTL.filter(({ Item }) => Item !== id);
    }
  }
}
