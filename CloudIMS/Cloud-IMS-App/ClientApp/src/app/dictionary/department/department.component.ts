import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { FormBuilder, FormControl, FormGroup, Validators } from "@angular/forms";
import { DepartmentServices } from '../../services/department.service';
import { IDepartment } from '../../classes/data-dictionary/Department/IDepartment.interface';
import { DepartmentClass } from '../../classes/data-dictionary/Department/DepartmentClass';
import { ViewChild, ElementRef } from '@angular/core';

@Component({
  selector: 'app-department',
  templateUrl: './department.component.html',
  styleUrls: ['./department.component.css']
})
export class DepartmentComponent implements OnInit {

  depForm: FormGroup;
  depListArray: IDepartment[];

  depListForm: IDepartment = new DepartmentClass();
  errormessage: string = "Error";
  Status: string;
  icon: string;
  isAdd: boolean;
  modalStatus: string;

  constructor(private departmentService: DepartmentServices, private toastr: ToastrService, private builder: FormBuilder) {
    this.createDepartmentForm();
  }

  ngOnInit() {
    this.loadDepartmentList();
  }

  private loadDepartmentList() {
    this.departmentService.getDepartmentList().subscribe((department) => this.depListArray = department);
  }

  private createDepartmentForm() {
    this.depForm = this.builder.group({
      id: [''],
      depName: ['']
    })
  }

  private insertDepartment() {
    this.depListForm.id = this.depForm.controls.id.value;
    this.depListForm.departmentName = this.depForm.controls.depName.value;
    if (this.depForm.controls.id.value.trim() != "" || this.depForm.controls.depName.value.trim() !="") {
      this.departmentService.insertDepartment(this.depListForm).subscribe(data => {
        this.toastr.success("Department Saved Succesfully", "Saved");
        this.loadDepartmentList();
      },
        error => {
          this.errormessage = error.error;
          this.toastr.error(this.errormessage, "Error");
        })
      this.resetForm();
    }
  }

  private PassData(id, departmentName) {

    this.depForm.controls.id.setValue(id);
    this.depForm.controls.depName.setValue(departmentName);
    this.Status = "Edit Changes";
    this.icon = "pencil";
    this.isAdd = false;
    this.modalStatus = "Edit Item Group";

  }

  private updateDepartment() {
    this.depListForm.id = this.depForm.controls.id.value;
    this.depListForm.departmentName = this.depForm.controls.depName.value;

    this.departmentService.updateDepartment(this.depListForm).subscribe(data => {
      this.toastr.info("Department Edited", "Edited");
      this.loadDepartmentList();
    },
      error => {
        this.errormessage = error.error;
        this.toastr.error(this.errormessage, "Error");
      })
  }

  private ClickAdd() {
    this.resetForm();
    this.Status = "Save Changes";
    this.icon = "floppy-o";
    this.isAdd = true;
    this.modalStatus = "Add Item Group";
  }

  private resetForm() {
    this.depForm.reset();
  }

  private deleteDepartment(id: string) {
    if (confirm("Are you sure to delete " + " " + id)) {
      this.departmentService.deleteItemGroup(id).subscribe(data => {
        this.toastr.info("Data" + " " + id + " " + "Successfully Deleted", "Deleted");
        document.getElementById(id).remove();
      },
        error => {
          this.errormessage = error.error;
          this.toastr.error(this.errormessage, "Error");
        })
    }
  }
}
