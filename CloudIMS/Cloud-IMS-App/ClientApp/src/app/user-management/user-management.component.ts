import { Component, OnInit } from '@angular/core';
import { IUserManagement } from '../classes/user-management/IUserManagement.interface';
import { UserManagementService } from '../services/UserManagement.service';

@Component({
  selector: 'app-user-management',
  templateUrl: './user-management.component.html'
})
export class UserManagementComponent implements OnInit {
  um_pg_menus: IUserManagement[];
  message: string;

  constructor(private UserManagementService: UserManagementService) {
  }

  ngOnInit(): void {
    this.UserManagementService.getProgramMenu().subscribe((um_pg_menus) => this.um_pg_menus = um_pg_menus)
  }
}
