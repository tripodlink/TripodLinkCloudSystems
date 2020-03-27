import { Component, OnInit } from '@angular/core';
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
  message: string;
  addForm: FormGroup;

  constructor(private supplierService: SupplierService, private toastr: ToastrService) {
    this.CreateForm()
  }

  private CreateForm() {
    this.addForm = new FormGroup({
      id: new FormControl(),
      Name: new FormControl()
    })
  }

  getSupplier() {
    this.supplierService.getSupplier().subscribe((suppliers) => this.suppliers = suppliers)
  }

  ngOnInit(): void {
    this.getSupplier();
  }
  insertSupplier() {
    let errormessage = "Error";
    this.supplierService.insertSupplier(this.addForm.value).subscribe(data => {

      this.toastr.success("Data Saved", "Saved");
      this.getSupplier();
      this.addForm.reset();

    },
      error => {
        errormessage = error.error;
        this.toastr.error(errormessage, "Error");
        this.addForm.reset();
      }
    );

  }

  onDelete(id) {
    var ans = confirm("Do you want to delete supplier with Id: " + id);
    if (ans) {
      this.supplierService.deleteSupplier(id).subscribe((data) => {
        this.getSupplier();
        this.toastr.info("Successfully Deleted");
        document.getElementById(id).remove();
      }, error => console.error(error))
    }
  }
}
