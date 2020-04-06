import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { InventoryManagementComponent } from './inventory-management.component';
import { InventoryService } from '../services/inventory.service';
import { InventoryInComponent } from './inventory-in/inventory-in.component';
import { InventoryOutComponent } from './inventory-out/inventory-out.component';



@NgModule({
  declarations: [
    InventoryManagementComponent,
    InventoryInComponent,
    InventoryOutComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,

    RouterModule.forRoot([])
  ],
  providers: [InventoryService]
})
export class InventoryManagementModule { }
