import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { SupplierComponent } from './supplier.component';
import { SupplierService } from '../../services/supplier.service';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';


@NgModule({
  declarations: [
    SupplierComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: 'data-dictionary/supplier', component: SupplierComponent}
    ])
  ],
  providers: [SupplierService]
})
export class SupplierModule { }
