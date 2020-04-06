import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from './_helpers/auth.guard';


// home components
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './LoginPage/login.component';


// dictionary components
import { DictionaryComponent } from './dictionary/dictionary.component';


// inventory managment components
import { InventoryManagementComponent } from './inventory-management/inventory-management.component';



//##############################################################################################
// user management folder componenet
import { UserManagementComponent } from './user-management/user-management.component';

// user account components
import { UserAccountComponent } from './user-management/user-account/user-account.component';
import { AddEditUserAccountComponent } from './user-management/user-account/addEdit-user-account.component';

// user group components
import { UserGroupComponent } from './user-management/user-group/user-group.component';
//##############################################################################################

const routes: Routes = [
  { path: '', component: HomeComponent, canActivate: [AuthGuard] },
  { path: 'login', component: LoginComponent },
  { path: 'dashboard', component: HomeComponent, canActivate: [AuthGuard] },


  // data dictionary routes
  { path: 'data-dictionary', component: DictionaryComponent, canActivate: [AuthGuard] },


  // inventory management routes
  { path: 'inventory-management', component: InventoryManagementComponent, canActivate: [AuthGuard] },


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
