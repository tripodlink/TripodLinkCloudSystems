import { Component, OnInit } from '@angular/core';
import { InventoryService } from '../services/inventory.service';
import { IInventory } from '../classes/inventory-management/IInventory.interface';

@Component({
  selector: 'app-inventory-management',
  templateUrl: './inventory-management.component.html'
})
export class InventoryManagementComponent implements OnInit {

  inv_pg_menus: IInventory[];
  message: string;

  constructor(private inventoryService: InventoryService) {
  }

  ngOnInit(): void {
    this.inventoryService.getProgramMenu().subscribe((inv_pg_menus) => this.inv_pg_menus = inv_pg_menus)
  }
}
