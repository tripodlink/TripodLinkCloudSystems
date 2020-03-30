import { Component } from "@angular/core";
import { UserAccount } from './../../classes/UserAccount';
import { UserAccountService } from './../../services/UserAccount.service';
import { ToastrService } from 'ngx-toastr';


@Component({
  selector: "user-account",
  templateUrl:"./user-account.component.html"
})
export class UserAccountComponent {
  users: UserAccount[];
  message: string = "Please wait...";

  constructor(private _userAccountService: UserAccountService, private toastr: ToastrService) { }


  ngOnInit(): void {
    this._userAccountService.getAllUsers().subscribe((users) => this.users = users);
  }

  onDeleteUser(event): void {
    let userId = event.currentTarget.name;

    let isDeleted: boolean = true;

    this._userAccountService.deleteUser(userId).subscribe(
      anyData => {
        isDeleted = true;
      },
      error => {
        if (error != null && error.status != 200) {
          this.toastr.error(error.error, "Delete");
          isDeleted = false;
        }
      }
    );

    if (isDeleted) {
      document.getElementById(userId).remove();
      this.toastr.info("Deleted susccessfully.", "Delete");
    }
  }

}
