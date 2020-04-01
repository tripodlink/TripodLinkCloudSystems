import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ManufacturerComponent } from './manufacturer.component';
import { RouterModule } from '@angular/router';
import { ManufacturerService } from '../../services/Manufacturer.service';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';



@NgModule({
  declarations: [
    ManufacturerComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: 'data-dictionary/manufacturer', component: ManufacturerComponent }
    ])
  ],
  providers: [ManufacturerService]
})
export class ManufacturerModule { }
