import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { Http } from '@angular/http';
import { CommonModule } from '@angular/common';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';

//--------IMPORTS MODULES-----------------------------------------------------------------------
import { HomeModule } from './home/home.module';
import { DictionaryModule } from './dictionary/dictionary.module';
import { InventoryManagementModule } from './inventory-management/inventory-management.module';
import { UserManagementModule } from './user-management/user-management.module';
//--------IMPORTS COMPONENTS-----------------------------------------------------------------------
import { AppComponent } from './app.component';
import { AppSidebarMenuComponent } from './app-sidebar-menu/app-sidebar-menu.component';
import { AppNavbarMenuComponent } from './app-navbar-menu/app-navbar-menu.component';
import { SideBarService } from './services/SideBar.service';


@NgModule({

    declarations: [
    AppComponent,
    AppNavbarMenuComponent,
    AppSidebarMenuComponent,

  ],

  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    CommonModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot({
      timeOut: 3000,
      positionClass: 'toast-top-right',
      preventDuplicates: true,
    }),

    HomeModule,
    DictionaryModule,
    InventoryManagementModule,
    UserManagementModule,

    RouterModule.forRoot([ ])
  ],
    providers: [ SideBarService, Http],
    bootstrap: [AppComponent]
})
export class AppModule { }
