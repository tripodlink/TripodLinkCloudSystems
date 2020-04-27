import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { FormBuilder, FormControl, FormGroup, Validators } from "@angular/forms";

@Component({
  selector: 'app-report-inventory-out',
  templateUrl: './report-inventory-out.component.html',
  styleUrls: ['./report-inventory-out.component.css']
})
export class ReportInventoryOutComponent implements OnInit {

  invInReports: FormGroup;
  constructor(public builder: FormBuilder) {
    this.createForm()
  }

  ngOnInit() {
  }
  createForm() {
    this.invInReports = this.builder.group({
      TransactionNumber: [''],
      ItemName: [''],
      ItemUnit: [''],
      dateFrom: [''],
      dateTo: ['']
    })
  }
}
