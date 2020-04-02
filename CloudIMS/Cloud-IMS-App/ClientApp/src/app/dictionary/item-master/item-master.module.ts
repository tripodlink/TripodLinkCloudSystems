import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { ItemMasterComponent } from './item-master.component';
import { ItemMasterServices } from '../../services/itemmaster.service';



@NgModule({
  declarations: [ItemMasterComponent],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: 'data-dictionary/item-master', component: ItemMasterComponent },
    ])
  ],
  providers: [ItemMasterServices]
})
export class ItemMasterModule { }
