import { Component, OnInit } from '@angular/core';
import { InventoryService } from '../services/inventory.service';
import { IInventory } from '../classes/inventory-management/IInventory.interface';
import { UserAuthorizationService } from '../services/UserAuthorization.service';
import { ProgramMenu } from '../classes/ProgramMenu';
import { UserAccount } from '../classes/UserAccount';

@Component({
  selector: 'app-inventory-management',
  templateUrl: './inventory-management.component.html'
})
export class InventoryManagementComponent implements OnInit {

  inv_pg_menus: ProgramMenu[] = new Array();
  message: string;

  constructor(private inventoryService: InventoryService, private _userAuthorizationService: UserAuthorizationService) {
  }

  ngOnInit(): void {
    let user: UserAccount = this._userAuthorizationService.getCurrentUser("Inventory Management");

    if (user != null) {
      this.inv_pg_menus = user.programMenus.filter(pm => pm.programFolderID == "IVM");
    }
  }
}
