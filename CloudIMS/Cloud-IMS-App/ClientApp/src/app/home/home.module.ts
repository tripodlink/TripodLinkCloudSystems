import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeComponent } from './home.component';
import { HomeService } from '../services/home.service';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';


@NgModule({
  declarations: [HomeComponent],
  imports: [
    FormsModule,
    ReactiveFormsModule,
    CommonModule
  ],
  providers: [HomeService]
})
export class HomeModule { }
