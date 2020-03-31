import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { SupplierService } from '../../services/supplier.service';
import { FormGroup, FormControl, FormBuilder } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { ISupplier } from '../../classes/data-dictionary/Supplier/ISupplier.interface';
import { ISupplierClass } from '../../classes/data-dictionary/Supplier/ISupplierClass';


@Component({
  selector: 'app-supplier',
  templateUrl: './supplier.component.html',
})
export class SupplierComponent{

  @ViewChild('supplierID', { static: true }) private supplierID: ElementRef;
  @ViewChild('supplierNametext', { static: true }) private supplierNametext: ElementRef;

  suppliers: ISupplier[];
  addSupplierFormGroup: FormGroup;
  Status: string;
  icon: string;
  isAdd: boolean;
  modalStatus: string;

  idInput: string;
  supplierInput: string;

  constructor(private supplierService: SupplierService, private toastr: ToastrService, private builder: FormBuilder, public el: ElementRef) {
    this.CreateForm();
  }
  ngOnInit(): void {
    this.getSupplier();
  }
  private CreateForm() {
    this.addSupplierFormGroup = this.builder.group({
      ID: new FormControl(),
      Name: new FormControl()
    })
  }

  private PassData(ID, Name) {

    this.supplierID.nativeElement.value = ID;
    this.supplierNametext.nativeElement.value = Name;
    this.Status = "Edit Changes";
    this.icon = "pencil";
    this.isAdd = false;
    this.modalStatus = "Edit Supplier";
  }

  getSupplier() {
    this.supplierService.getSupplier().subscribe((suppliers) => this.suppliers = suppliers)
  }
  private insertSupplier() {
    let errormessage = "Error";
    this.supplierService.insertSupplier(this.addSupplierFormGroup.value).subscribe(data => {

      this.toastr.success("Data Saved", "Saved");
      this.getSupplier();
     

    },
      error => {
        errormessage = error.error;
        this.toastr.error(errormessage, "Error");
      });
    this.ResetForm();
  }
  private updateSupplier() {

    let dataToUpdate = document.getElementsByClassName('suppliertextfield');
    let supplier: ISupplier = new ISupplierClass();

    Array.from(dataToUpdate).forEach((element: HTMLInputElement) => {

      if (element.id == "supplierID") {
        supplier.ID = element.value;
      }
      if (element.id == "supplierNametext") {
        supplier.Name = element.value;
      }
    });


    let errormessage = "Error";
    this.supplierService.updateSupplier(supplier).subscribe(data => {
      this.toastr.info("Data Edited", "Edited");
      this.getSupplier();
    },

      error => {
        errormessage = error.error;
        this.toastr.error(errormessage, "Error");
      });
  }

  private deleteSupplier(id) {
    if (confirm("Are you sure do you want to delete this Supplier" + " " + id)) {
      let errormessage = "Error";
      this.supplierService.deleteSupplier(id).subscribe(data => {
        this.toastr.info("Data" + " " + id + " " + "Successfully Deleted", "Deleted");
        document.getElementById(id).remove();
      },
        error => {
          errormessage = error.error;
          this.toastr.error(errormessage, "Error");
        });
    }
  }
  private ClickAdd() {
    this.ResetForm();
    this.Status = "Save Changes";
    this.icon = "floppy-o";
    this.isAdd = true;
    this.modalStatus = "Add Supplier"
    this.idInput = "";
    this.supplierInput = "";
  }

  private ResetForm() {
    this.addSupplierFormGroup.reset();
  }
}
