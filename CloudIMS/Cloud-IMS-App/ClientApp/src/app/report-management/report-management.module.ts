import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { ReportManagementComponent } from './report-management.component';
import { ReportInventoryInComponent } from './report-inventory-in/report-inventory-in.component';
import { ReportInventoryOutComponent } from './report-inventory-out/report-inventory-out.component';
import { ReportInventoryInService } from '../services/ReportInventoryIn.service';


@NgModule({
  declarations: [
    ReportManagementComponent,
    ReportInventoryInComponent,
    ReportInventoryOutComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,

    RouterModule.forRoot([])
  ],
  providers: [ReportInventoryInService]
})
export class ReportManagementModule {  }
