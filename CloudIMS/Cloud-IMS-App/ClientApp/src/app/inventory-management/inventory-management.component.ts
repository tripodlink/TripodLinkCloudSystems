import { Component, OnInit } from '@angular/core';
import { InventoryService } from '../services/inventory.service';
import { IInventory } from '../classes/inventory-management/IInventory.interface';
import { UserAuthorizationService } from '../services/UserAuthorization.service';
import { ProgramMenu } from '../classes/ProgramMenu';

@Component({
  selector: 'app-inventory-management',
  templateUrl: './inventory-management.component.html'
})
export class InventoryManagementComponent implements OnInit {

  inv_pg_menus: ProgramMenu[];
  message: string;

  constructor(private inventoryService: InventoryService, private _userAuthorizationService: UserAuthorizationService) {
  }

  ngOnInit(): void {
    this.inv_pg_menus = this._userAuthorizationService.getCurrentUser().programMenus.filter(pm => pm.programFolderID == "IVM");
  }
}
