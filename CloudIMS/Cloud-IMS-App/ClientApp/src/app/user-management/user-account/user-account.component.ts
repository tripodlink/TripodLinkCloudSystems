import { Component } from "@angular/core";

import { ToastrService } from 'ngx-toastr';
import { UserAccount } from "../../classes/UserAccount";
import { UserAccountService } from "../../services/UserAccount.service";

@Component({
  selector: "user-account",
  templateUrl:"./user-account.component.html"
})
export class UserAccountComponent {
  users: UserAccount[];
  message: string = "Please wait...";

  constructor(private userAccountService: UserAccountService, private toastr: ToastrService) { }


  ngOnInit(): void {
    this.userAccountService.getAllUsers().subscribe((users) => this.users = users);
  }

  onDeleteUser(): void {
    alert("Delete button was clicked.");
  }

}
