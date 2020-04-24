import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-report-inventory-in',
  templateUrl: './report-inventory-in.component.html'
})
export class ReportInventoryInComponent implements OnInit {

  rptInvInFormGroup: FormGroup;
  constructor(public builder: FormBuilder) { }

  ngOnInit() {
  }

  createForm() {
    this.rptInvInFormGroup = this.builder.group({
      supplier_hdr: [''],
      itemMaster_dtl: [''],
      itemUnit_dtl: [''],
      from_DT: [''],
      To_DT: [''],
    })
  }
}
