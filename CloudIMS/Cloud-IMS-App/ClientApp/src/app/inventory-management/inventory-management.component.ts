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
    this._userAuthorizationService.getCurrentUser()
      .then(user => {
        this.inv_pg_menus = user.programMenus.filter(pm => pm.programFolderID == "UM");
      })
      .catch((error) => {
        // error while retrieving user data
      })
  }
}
