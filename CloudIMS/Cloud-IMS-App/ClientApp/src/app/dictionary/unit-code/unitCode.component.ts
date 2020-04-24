import { Component, ViewChild, ElementRef } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from "@angular/forms";

import { UnitCodeService } from '../../services/UnitCode.service';
import { IUnitCode } from '../../classes/data-dictionary/UnitCode/IUnitCode.interface';
import { IUnitCodeClass } from '../../classes/data-dictionary/UnitCode/IUnitCodeClass';
import { ToastrService } from 'ngx-toastr';
import { error } from 'protractor';
 

@Component({
  selector: 'dic-unitCode',
  templateUrl: './unitCode.component.html',
})
export class UnitCodeComponent {
  unitCodes: IUnitCode[];
  addUnitCodeForm: FormGroup;

  unitCodesForm: IUnitCode = new IUnitCodeClass;

  Status: string;
  icon: string;
  isAdd: boolean;
  modalStatus: string;

  constructor(public unitcodeService: UnitCodeService, public formBuilder: FormBuilder, public toastr: ToastrService) {
    this.CreateForm();
  }


   CreateForm(): void {
    this.addUnitCodeForm = this.formBuilder.group({
      Code: [''],
      Description: ['', {disabled : this.isAdd} ],
      ShortDescription: ['', Validators.required]
    })
  }

  ngOnInit(): void {
    this.loadData();
  }

   loadData() {
    this.unitcodeService.getUnitCodes().subscribe((unitCodes) => this.unitCodes = unitCodes);
  }

   insertUnitCodes() {
    
    this.unitCodesForm.code = this.addUnitCodeForm.get('Code').value;
    this.unitCodesForm.description = this.addUnitCodeForm.controls.Description.value;
    this.unitCodesForm.shortDescription = this.addUnitCodeForm.controls.ShortDescription.value;

    if (this.addUnitCodeForm.get('Code').value.trim() != "" || this.addUnitCodeForm.controls.Description.value.trim() != "") {
      let errormessage = "Error";
      this.unitcodeService.insertUnitCodes(this.unitCodesForm).subscribe(data => {
        this.toastr.success("New Unit Code Inserted", "Saved");
        this.loadData();
        this.resetForm();
      },
        error => {
          errormessage = error.error;
          this.toastr.error(errormessage, "Error");

        }
      );
    }
  }

   onClickAddButton() {
    this.resetForm();
    this.Status = "Save Changes";
    this.icon = "floppy-o";
    this.isAdd = true;
    this.modalStatus = "Add Item Group";
  }

   resetForm() {
    this.addUnitCodeForm.reset();
  }

   deleteUnitCode(id) {
    if (confirm("Are you sure to delete" + " " + id)) {
      let errormessage = "Error";
      this.unitcodeService.deleteUnitCodes(id).subscribe(data => {
        this.toastr.info("Unit Code" + " " + id + " " + "Successfully Deleted", "Deleted");
        document.getElementById(id).remove();
      },
        error => {
          errormessage = error.error;
          this.toastr.error(errormessage, "Unit Code Error");
        }
      )
    }
  }

   onPassData(code, desc, shortdesc) {

    this.addUnitCodeForm.controls.Code.setValue(code);
    this.addUnitCodeForm.controls.Description.setValue(desc);
    this.addUnitCodeForm.controls.ShortDescription.setValue(shortdesc);

    this.Status = "Edit Changes";
    this.icon = "pencil";
    this.isAdd = false;
    this.modalStatus = "Edit Item Group";
  }


   updateUnitCodes() {

    this.unitCodesForm.code = this.addUnitCodeForm.controls.Code.value;
    this.unitCodesForm.description = this.addUnitCodeForm.controls.Description.value;
    this.unitCodesForm.shortDescription = this.addUnitCodeForm.controls.ShortDescription.value;

    let errormessage = "Error";
    this.unitcodeService.updateUnitCodes(this.unitCodesForm).subscribe(data => {
      this.toastr.info("Item Group Data Edited", "Edited");
      this.loadData();
    },
      error => {
        errormessage = error.error;
        this.toastr.error(errormessage, "Error");
      }
    );
  }
}
