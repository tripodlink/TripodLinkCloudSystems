import { Component, OnInit } from '@angular/core';
import { IInventoryInTrxHeader } from '../../classes/inventory-management/InventoryIn/IInventoryInTrxHeader.interface';
import { IInventoryInTrxHeaderClass } from '../../classes/inventory-management/InventoryIn/IInventoryInTrxHeaderClass';
import { FormGroup, FormBuilder } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { InventoryInService } from '../../services/InventoryIn.service';
import { DatePipe } from '@angular/common';
import { ISupplier } from '../../classes/data-dictionary/Supplier/ISupplier.interface';
import { IiTemMaster } from '../../classes/data-dictionary/ItemMaster/IitemMaster.interface';
import { IUnitCode } from '../../classes/data-dictionary/UnitCode/IUnitCode.interface';
import { IInventoryInTrxDetail } from '../../classes/inventory-management/InventoryIn/IInventoryInTrxDetail.interface';
import { IInventoryInTrxDetailClass } from '../../classes/inventory-management/InventoryIn/IInventoryInTrxDetailClass';
import { IiTemMasterUnitJoinUnit } from '../../classes/data-dictionary/ItemMasterUnit/IitemMasterUnitJoinUnit.interface';
import { IitemMasterUnitJoinUnitClass } from '../../classes/data-dictionary/ItemMasterUnit/IitemMasterJoinUnitClass';
import { UserAuthorizationService } from '../../services/UserAuthorization.service';

@Component({
  selector: 'app-defected-items',
  templateUrl: './defected-items.component.html',
  //styleUrls: ['./defected-items.component.css']
})
export class DefectedItemsComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

}
