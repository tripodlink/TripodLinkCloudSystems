import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { AppNavbarMenuComponent } from './app-navbar-menu.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AppComponent } from '../app.component';



@NgModule({
  declarations: [AppNavbarMenuComponent],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule
  ],
})
export class AppNavbarMenuModule { }
