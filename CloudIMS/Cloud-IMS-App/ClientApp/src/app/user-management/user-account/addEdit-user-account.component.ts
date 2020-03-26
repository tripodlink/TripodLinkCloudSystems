import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { UserAccount } from 'src/app/classes/UserAccount';
import { UserAccountService } from 'src/app/services/UserAccount.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: "add-edit-user",
  templateUrl: "addEdit-user-acount.component.html"
})
export class AddEditUserAccountComponent {
  isNewUser: boolean = true;
  user: UserAccount;

  constructor(private _activatedRoute: ActivatedRoute, private _serAccountService: UserAccountService, private _toastr: ToastrService) {

  }

  ngOnInit() {
    let id = this._activatedRoute.snapshot.params["id"];

    this._serAccountService.findUserById(id)
      .subscribe(userData => {
        this.user = userData
        }
        ,error => {
          this._toastr.error(error.error, "Error");
          this._toastr.clear();
        }
    );
  }
}
