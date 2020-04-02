import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { ItemGroupComponent } from './item-group.component';
import { ItemGroupServices } from '../../services/itemgroup.service';



@NgModule({
  declarations: [ItemGroupComponent],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: 'data-dictionary/item-group', component: ItemGroupComponent },
    ])
  ],
  providers: [ItemGroupServices]
})
export class ItemGroupModule { }
