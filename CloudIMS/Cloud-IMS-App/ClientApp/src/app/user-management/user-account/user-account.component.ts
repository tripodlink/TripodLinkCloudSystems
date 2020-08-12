import { Component } from "@angular/core";
import { ToastrService } from 'ngx-toastr';
import { UserAccountService } from "../../services/UserAccount.service";
import { UserAccount } from "../../classes/UserAccount";


@Component({
  selector: "user-account",
  templateUrl: "./user-account.component.html"
})
export class UserAccountComponent {
  users: UserAccount[];
  message: string = "Please wait...";

  constructor(private _userAccountService: UserAccountService, private toastr: ToastrService) { }


  ngOnInit(): void {
    this._userAccountService.getAllUsers().subscribe((users) => this.users = users);
  }

  onDeleteUser(userID, userName): void {
    if (confirm("Are you sure to delete" + " " + userName)) {

    this._userAccountService.deleteUser(userID).subscribe(
      anyData => {

        document.getElementById(userID).remove();
        this.toastr.info("Deleted susccessfully.", "Delete");
      },
      error => {
        if (error != null && error.status != 200) {
          this.toastr.error(error.error, "Delete");
        }
      })
    }
  }
}
