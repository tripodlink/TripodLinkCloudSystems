import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SupplierModule } from './supplier/supplier.module';
import { ManufacturerModule } from './manufacturer/manufacturer.module';
import { RouterModule } from '@angular/router';
import { DictionaryComponent } from './dictionary.component';
import { DictionaryService } from '../services/dictionary.service';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ItemGroupModule } from './item-group/item-group.module';
import { ItemMasterModule } from './item-master/item-master.module';
import { UnitCodeModule } from './unit-code/unitCode.module';



@NgModule({
  declarations: [DictionaryComponent],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,

    ItemMasterModule,
    ItemGroupModule,
    UnitCodeModule,
    SupplierModule,
    ManufacturerModule,

    RouterModule.forRoot([
      { path: 'data-dictionary', component: DictionaryComponent },
    ])
  ],
  providers: [DictionaryService]
})
export class DictionaryModule {  }
