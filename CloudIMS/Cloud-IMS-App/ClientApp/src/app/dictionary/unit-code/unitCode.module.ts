import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { UnitCodeComponent } from './unitCode.component';
import { UnitCodeService } from '../../services/UnitCode.service';



@NgModule({
  declarations: [UnitCodeComponent],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: 'data-dictionary/unit-code', component: UnitCodeComponent },
    ])
  ],
  providers: [UnitCodeService]
})
export class UnitCodeModule { }
