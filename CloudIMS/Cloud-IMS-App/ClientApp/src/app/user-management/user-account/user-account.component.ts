import { Component } from "@angular/core";
import { UserAccount } from 'src/app/classes/UserAccount';
import { UserAccountService } from 'src/app/services/UserAccount.service';
import { ToastrService } from 'ngx-toastr';

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
}
