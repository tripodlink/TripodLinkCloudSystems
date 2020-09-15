import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from './_helpers/auth.guard';


// home components
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './LoginPage/login.component';


// dictionary components
import { DictionaryComponent } from './dictionary/dictionary.component';
import { ItemGroupComponent } from './dictionary/item-group/item-group.component';
import { ItemMasterComponent } from './dictionary/item-master/item-master.component';
import { ManufacturerComponent } from './dictionary/manufacturer/manufacturer.component';
import { SupplierComponent } from './dictionary/supplier/supplier.component';
import { UnitCodeComponent } from './dictionary/unit-code/unitCode.component';
import { DepartmentComponent } from './dictionary/department/department.component';

// inventory managment components
import { InventoryManagementComponent } from './inventory-management/inventory-management.component';
import { InventoryInComponent } from './inventory-management/inventory-in/inventory-in.component';
import { InventoryOutComponent } from './inventory-management/inventory-out/inventory-out.component';

//report management
import { ReportManagementComponent } from './report-management/report-management.component';

//##############################################################################################
// user management folder componenet
import { UserManagementComponent } from './user-management/user-management.component';

// user account components
import { UserAccountComponent } from './user-management/user-account/user-account.component';
import { AddEditUserAccountComponent } from './user-management/user-account/addEdit-user-account.component';

// user group components
import { UserGroupComponent } from './user-management/user-group/user-group.component';
import { AppSidebarMenuComponent } from './app-sidebar-menu/app-sidebar-menu.component';
import { ReportInventoryInComponent } from './report-management/report-inventory-in/report-inventory-in.component';
import { ReportInventoryOutComponent } from './report-management/report-inventory-out/report-inventory-out.component';
import { ItemTrackingComponent } from './inventory-management/item-tracking/item-tracking.component';
//import { DefectedItemsComponent } from './inventory-management/defected-items/defected-items.component';

//##############################################################################################

const routes: Routes = [
  { path: '', component: HomeComponent, canActivate: [AuthGuard] },
  { path: 'login', component: LoginComponent },
  { path: 'dashboard', component: HomeComponent, canActivate: [AuthGuard] },

  //NAVIGATION BAR AND SIDEBAR ROUTES
  { path: 'app-sidebar/sidebar-menu', component: AppSidebarMenuComponent },

  // data dictionary routes
  { path: 'data-dictionary', component: DictionaryComponent, canActivate: [AuthGuard] },
  { path: 'data-dictionary/item-group', component: ItemGroupComponent, canActivate: [AuthGuard] },
  { path: 'data-dictionary/item-master', component: ItemMasterComponent, canActivate: [AuthGuard] },
  { path: 'data-dictionary/manufacturer', component: ManufacturerComponent, canActivate: [AuthGuard] },
  { path: 'data-dictionary/supplier', component: SupplierComponent, canActivate: [AuthGuard] },
  { path: 'data-dictionary/unit-code', component: UnitCodeComponent, canActivate: [AuthGuard] },
  { path: 'data-dictionary/department', component: DepartmentComponent, canActivate: [AuthGuard] },

  // inventory management routes
  { path: 'inventory-management', component: InventoryManagementComponent, canActivate: [AuthGuard] },
  { path: 'inventory-management/inventory-in', component: InventoryInComponent, canActivate: [AuthGuard] },
  { path: 'inventory-management/inventory-out', component: InventoryOutComponent, canActivate: [AuthGuard] },
  { path: 'inventory-management/inventory-out/:trxno', component: InventoryOutComponent, canActivate: [AuthGuard] },
  { path: 'inventory-management/item-tracking', component: ItemTrackingComponent, canActivate: [AuthGuard] },
  //{ path: 'inventory-management/defected-items', component: DefectedItemsComponent, canActivate: [AuthGuard] },

  //report management
  { path: 'report-management', component: ReportManagementComponent, canActivate: [AuthGuard] },
  { path: 'report-management/report-inventory-in', component: ReportInventoryInComponent, canActivate: [AuthGuard] },
  { path: 'report-management/report-inventory-out', component: ReportInventoryOutComponent, canActivate: [AuthGuard] },


  //user management folder
  { path: 'user-management', component: UserManagementComponent, canActivate: [AuthGuard] },


  // user account routes
  { path: 'user-management/user-account', component: UserAccountComponent, canActivate: [AuthGuard] },
  { path: 'user-management/user-account/add', component: AddEditUserAccountComponent, canActivate: [AuthGuard] },
  { path: 'user-management/user-account/edit/:id', component: AddEditUserAccountComponent, canActivate: [AuthGuard] },


  // user group routes
  { path: 'user-management/user-group', component: UserGroupComponent, canActivate: [AuthGuard] },


  // otherwise redirect to home
  { path: '**', redirectTo: '' }
];

export const AppRoutingModule = RouterModule.forRoot(routes);
