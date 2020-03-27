import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { Http } from '@angular/http';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';

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

import { CommonModule } from '@angular/common';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { ToastrModule } from 'ngx-toastr';
import { ItemGroupComponent } from './dictionary/item-group/item-group.component';


@NgModule({
    declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    UnitCodeComponent,
    SupplierComponent,
    AppSidebarMenuComponent,
    AppNavbarMenuComponent,
    UserAccountComponent,
    AddEditUserAccountComponent,
    ItemGroupComponent
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
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'dictionary/unit-code', component: UnitCodeComponent },
      { path: 'user-management/user-account', component: UserAccountComponent },
      { path: 'user-management/user-account/add', component: AddEditUserAccountComponent },
      { path: 'user-management/user-account/edit/:id', component: AddEditUserAccountComponent },
      { path: 'dictionary/item-group', component: ItemGroupComponent },
   ])
  ],
    providers: [UnitCodeService, UserAccountService, UserAuthorizationService, UserGroupService, ItemGroupServices, Http],
  bootstrap: [AppComponent]
})
export class AppModule { }
