import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AppSidebarMenuComponent } from './app-sidebar-menu.component';
import { RouterModule } from '@angular/router';
import { SideBarService } from '../services/SideBar.service';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AppComponent } from '../app.component';


@NgModule({
  declarations: [AppSidebarMenuComponent],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: 'app-sidebar/sidebar-menu', component: AppSidebarMenuComponent },
    ])
  ],
  providers: [SideBarService]
})
export class AppSidebarMenuModule { }
