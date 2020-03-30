import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { Http } from '@angular/http';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { UnitCodeComponent } from './dictionary/unit-code/unitCode.component';
import { UnitCodeService } from './services/UnitCode.service';
import { SupplierComponent } from './dictionary/supplier/supplier.component';
import { SupplierService } from './services/supplier.service';
import { AppSidebarMenuComponent } from './app-sidebar-menu/app-sidebar-menu.component';
import { AppNavbarMenuComponent } from './app-navbar-menu/app-navbar-menu.component';

import { UserAccountComponent } from './user-management/user-account/user-account.component';
import { AddEditUserAccountComponent } from './user-management/user-account/addEdit-user-account.component';
import { UserAccountService } from './services/UserAccount.service';
import { UserGroupService } from './services/UserGroup.service';
import { UserAuthorizationService } from './services/UserAuthorization.service';
import { ItemGroupServices } from './services/itemgroup.service';
import { ItemMasterServices } from './services/itemmaster.service';



import { CommonModule } from '@angular/common';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { ToastrModule } from 'ngx-toastr';
import { SideBarService } from './services/SideBar.service';
import { HomeService } from './services/home.service';
import { DictionaryComponent } from './dictionary/dictionary.component';
import { DictionaryService } from './services/dictionary.service';
import { InventoryManagementComponent } from './inventory-management/inventory-management.component';
import { InventoryOutComponent } from './inventory-management/inventory-out/inventory-out.component';
import { InventoryInComponent } from './inventory-management/inventory-in/inventory-in.component';
import { InventoryService } from './services/inventory.service';
import { ItemGroupComponent } from './dictionary/item-group/item-group.component';
import { ItemMasterComponent } from './dictionary/item-master/item-master.component';

@NgModule({
    declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
        UnitCodeComponent,
        SupplierComponent,
        AppSidebarMenuComponent,
        AppNavbarMenuComponent,
        UserAccountComponent,
        DictionaryComponent,
        InventoryManagementComponent,
        InventoryOutComponent,
        InventoryInComponent,  
        ItemGroupComponent,
        ItemMasterComponent,
       AddEditUserAccountComponent,

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
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
        { path: 'app-sidebar/sidebar-menu', component: AppSidebarMenuComponent },
        { path: 'data-dictionary', component: DictionaryComponent },
        { path: 'inventory-management', component: InventoryManagementComponent },
        { path: 'data-dictionary/unit-code', component: UnitCodeComponent },
        { path: 'data-dictionary/supplier', component: SupplierComponent },
        { path: 'user-management/user-account', component: UserAccountComponent },
        { path: 'data-dictionary/item-group', component: ItemGroupComponent },
        { path: 'data-dictionary/item-master', component: ItemMasterComponent },
        { path: 'user-management/user-account/add', component: AddEditUserAccountComponent },
        { path: 'user-management/user-account/edit/:id', component: AddEditUserAccountComponent }
   ]) 
  ],
  providers: [SideBarService, HomeService, DictionaryService, InventoryService, 
              ItemGroupServices,ItemMasterServices,UnitCodeService, SupplierService, 
              UserAccountService,UserAccountService,UserGroupService,UserAuthorizationService Http],
  bootstrap: [AppComponent]
})
export class AppModule { }
