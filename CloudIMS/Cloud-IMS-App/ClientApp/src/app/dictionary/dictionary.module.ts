import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DictionaryComponent } from './dictionary.component';
import { DictionaryService } from '../services/dictionary.service';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { ItemGroupComponent } from './item-group/item-group.component';
import { ItemMasterComponent } from './item-master/item-master.component';
import { ManufacturerComponent } from './manufacturer/manufacturer.component';
import { SupplierComponent } from './supplier/supplier.component';
import { UnitCodeComponent } from './unit-code/unitCode.component';
import { ItemGroupServices } from '../services/itemgroup.service';
import { ItemMasterServices } from '../services/itemmaster.service';
import { SupplierService } from '../services/supplier.service';
import { ManufacturerService } from '../services/Manufacturer.service';
import { UnitCodeService } from '../services/UnitCode.service';
import { DepartmentComponent } from './department/department.component';
import { DepartmentServices } from '../services/department.service';
import { ItemMasterUnitComponent } from './item-master-unit/item-master-unit.component';
import { ItemMasterUnitServices } from '../services/itemmasterUnit.service';


@NgModule({
  declarations: [
    DictionaryComponent,
    ItemGroupComponent,
    ItemMasterComponent,
    ManufacturerComponent,
    SupplierComponent,
    UnitCodeComponent,
    DepartmentComponent,
    ItemMasterUnitComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,

    RouterModule.forRoot([])
  ],
  providers: [
    DictionaryService,
    ItemGroupServices,
    ItemMasterServices,
    ManufacturerService,
    SupplierService,
    UnitCodeService,
    DepartmentServices,
    ItemMasterUnitServices
  ]
})
export class DictionaryModule {  }
