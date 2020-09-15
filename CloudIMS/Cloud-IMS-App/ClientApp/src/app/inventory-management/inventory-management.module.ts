import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { InventoryManagementComponent } from './inventory-management.component';
import { InventoryService } from '../services/inventory.service';
import { InventorysServices } from '../services/inventorys.service';
import { InventoryInComponent } from './inventory-in/inventory-in.component';
import { InventoryOutComponent } from './inventory-out/inventory-out.component';
import { InventoryInService } from '../services/InventoryIn.service';
import { DatePipe } from '@angular/common';
import { ItemTrackingComponent } from './item-tracking/item-tracking.component';
import { ItemTrackingServices } from '../../app/services/itemTracking.service';



@NgModule({
  declarations: [
    InventoryManagementComponent,
    InventoryInComponent,
    InventoryOutComponent,
    ItemTrackingComponent,

    
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,

    RouterModule.forRoot([])
  ],

  providers: [InventoryService, InventoryInService, InventorysServices, DatePipe, ItemTrackingServices]

})
export class InventoryManagementModule { }
