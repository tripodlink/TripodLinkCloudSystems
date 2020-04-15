import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { FormBuilder, FormControl, FormGroup, Validators } from "@angular/forms"; 
import { ViewChild, ElementRef } from '@angular/core';
import { InventoryOutHeaderServices } from '../../services/inventoryoutheader.service';
import { IinventoryOutHeaderClass  } from '../../classes/inventory-management/inventory-out/IitemGroupClass';
import { IinventoryOutHeader } from '../../classes/inventory-management/inventory-out/IitemGroup.interface';
import { DatePipe } from '@angular/common'

@Component({
  selector: 'app-inventory-out',
  templateUrl: './inventory-out.component.html'
})
export class InventoryOutComponent implements OnInit {

  InventoryOutHeaderForm: FormGroup;

  InventoryOutHeaderArray: IinventoryOutHeader = new IinventoryOutHeaderClass();

  formatDate: string;
  isNewTrx: boolean;

  constructor(private toastr: ToastrService, private builder: FormBuilder, private inventoryOutServices: InventoryOutHeaderServices,
    private datepipe: DatePipe) {
    this.createHeaderForm();

  }
  ngOnInit() {
  }

  createHeaderForm() {
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
}
