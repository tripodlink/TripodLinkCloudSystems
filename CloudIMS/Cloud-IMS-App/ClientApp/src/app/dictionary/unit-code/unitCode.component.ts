import { Component, ViewChild, ElementRef } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from "@angular/forms";

import { UnitCodeService } from '../../services/UnitCode.service';
import { IUnitCode } from '../../classes/IUnitCode.interface';
import { Interface } from 'readline';
import { error } from 'protractor';
import { ToastrService } from 'ngx-toastr';


@Component({
  selector: 'dic-unitCode',
  templateUrl: './unitCode.component.html',
})
export class UnitCodeComponent {
  unitCodes: IUnitCode[];
  message: string;
  addForm: FormGroup;

  constructor(private unitcodeService: UnitCodeService, private formBuilder: FormBuilder, private toastr: ToastrService) {
    this.CreateForm();
  }


  private CreateForm() {
    this.addForm = new FormGroup({
      Code: new FormControl(),
      Description: new FormControl(),
      ShortDescription: new FormControl()
    })
  }

  ngOnInit(): void {
    this.reloadData();
  }

  reloadData() {
    this.unitcodeService.getUnitCodes().subscribe((unitCodes) => this.unitCodes = unitCodes);
  }

  insertUnitCodes() {
    let errormessage = "Error";
    this.unitcodeService.insertUnitCodes(this.addForm.value).subscribe(data => {

      this.toastr.success("Data Saved", "Saved");
      this.reloadData();
    
      this.addForm.reset();

    },
      error => {
        errormessage = error.error;
        this.toastr.error(errormessage, "Error");
        this.addForm.reset();
      }
    );

  }


}
