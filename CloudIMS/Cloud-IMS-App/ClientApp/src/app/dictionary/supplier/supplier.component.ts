import { Component, OnInit, ViewChild } from '@angular/core';
import { ISupplier } from '../../classes/ISupplier.interface';
import { SupplierService } from '../../services/supplier.service';
import { FormGroup, FormControl } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-supplier',
  templateUrl: './supplier.component.html',
})
export class SupplierComponent implements OnInit {
  suppliers: ISupplier[];
  addSupplierForm: FormGroup;
  @ViewChild('closeModal', { static: true }) closeModal;
  Status: string;
  icon: string;
  isAdd: boolean;

  idInput: string;
  SupplierInput: string;

  constructor(private supplierService: SupplierService, private toastr: ToastrService) {
    this.CreateForm()
  }

  private CreateForm() {
    this.addSupplierForm = new FormGroup({
      id: new FormControl(),
      Name: new FormControl()
    })
  }
  private PassData(id, SupplierName) {
    this.idInput = id;
    this.SupplierInput = SupplierName;
    this.CreateForm();
    this.Status = "Edit Changes";
    this.icon = "pencil";
    this.isAdd = false;
  }

  getSupplier() {
    this.supplierService.getSupplier().subscribe((suppliers) => this.suppliers = suppliers)
  }

  private verifyProcess() {

    if (this.isAdd == true) {
      this.insertSupplier();
    } else {
      this.updateSupplier();
    }

  }

  ngOnInit(): void {
    this.getSupplier();
  }
  private insertSupplier() {
    let errormessage = "Error";
    this.supplierService.insertSupplier(this.addSupplierForm.value).subscribe(data => {

      this.toastr.success("Data Saved", "Saved");
      this.getSupplier();
     

    },
      error => {
        errormessage = error.error;
        this.toastr.error(errormessage, "Error");
      });
    this.ResetForm();
    this.closeModal.nativeElement.click();
  }
  private updateSupplier() {

    let errormessage = "Error";
    this.supplierService.updateSupplier(this.addSupplierForm.value).subscribe(data => {
      this.toastr.info("Data Edited", "Edited");
      this.getSupplier();
    },

      error => {
        errormessage = error.error;
        this.toastr.error(errormessage, "Error");
      });
  }

  private deleteSupplier(id) {
    var ans = confirm("Do you want to delete supplier with Id: " + id);
    if (ans) {
      this.supplierService.deleteSupplier(id).subscribe((data) => {
        this.getSupplier();
        this.toastr.info("Successfully Deleted");
        document.getElementById(id).remove();
      }, error => console.error(error))
    }
  }
  private ClickAdd() {
    this.addSupplierForm.reset();
    this.Status = "Save Changes";
    this.icon = "floppy-o";
    this.isAdd = true;
    this.idInput = "";
    this.SupplierInput = "";
  }

  private ResetForm() {
    this.addSupplierForm.reset();
  }
}
