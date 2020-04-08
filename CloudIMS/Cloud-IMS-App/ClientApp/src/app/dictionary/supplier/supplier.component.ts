import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { SupplierService } from '../../services/supplier.service';
import { FormGroup, FormControl, FormBuilder, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { ISupplier } from '../../classes/data-dictionary/Supplier/ISupplier.interface';
import { ISupplierClass } from '../../classes/data-dictionary/Supplier/ISupplierClass';

@Component({
  selector: 'app-supplier',
  templateUrl: './supplier.component.html',
})


export class SupplierComponent{


  isupplier: ISupplier = new ISupplierClass();
  suppliers: ISupplier[] = new Array();
  addSupplierFormGroup: FormGroup;
  Status: string;
  icon: string;
  iconHeaderTextModal: string;
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
      ID: ['',Validators.required],
      Name: ['',Validators.required]
    })
  }

  private PassData(ID, Name) {

    this.addSupplierFormGroup.controls.ID.setValue(ID);
    this.addSupplierFormGroup.controls.Name.setValue(Name);
    this.Status = "Edit Changes";
    this.icon = "pencil";
    this.isAdd = false;
    this.modalStatus = "Edit Supplier";
    this.iconHeaderTextModal = "fa fa-pencil-square";
  }

  getSupplier() {
    this.supplierService.getSupplier().subscribe((suppliers) => this.suppliers = suppliers)
  }
  private insertSupplier() {
    let errormessage = "Error";

    let supId = this.addSupplierFormGroup.controls.ID.value
    let supName = this.addSupplierFormGroup.controls.Name.value

    if (supId == null || supName == null || supId == '' || supName == ''){

    }else {
      this.isupplier.ID = supId
      this.isupplier.Name = supName

      this.supplierService.insertSupplier(this.isupplier).subscribe(data => {

        this.toastr.success("Data Saved", "Saved");
        this.getSupplier();


      },
        error => {
          errormessage = error.error;
          this.toastr.error(errormessage, "Error");
        });
      this.ResetForm();
    }
  }
  private updateSupplier() {

    this.isupplier.ID = this.addSupplierFormGroup.controls.ID.value
    this.isupplier.Name = this.addSupplierFormGroup.controls.Name.value
  
    let errormessage = "Error";
    this.supplierService.updateSupplier(this.isupplier).subscribe(data => {
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
    this.iconHeaderTextModal ="fa fa-plus-square"
  }

  private ResetForm() {
    this.addSupplierFormGroup.reset();
  }
}
