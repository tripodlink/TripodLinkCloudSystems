import { Component, OnInit} from '@angular/core';
import { UserAuthorizationService } from '../services/UserAuthorization.service';
import { UserAccount } from '../classes/UserAccount';
import { Router } from '@angular/router';

/////////////////
import { HomeService } from '../services/home.service';
import { IItemListPendingToStockOut } from '../classes/home/IItemListPendingToStockOut.interface';
import { ICountNotif } from '../classes/app-sidebar-menu/ICountNotif.interface';

@Component({
  selector: 'app-app-sidebar-menu',
  templateUrl: './app-sidebar-menu.component.html'
})
export class AppSidebarMenuComponent implements OnInit {
  user: UserAccount;
  message: string;
  button_status: string;
  button_isActive: boolean;
  isZeroNotif: boolean;
  timeout: number;

  IItemListPendingToStockOut: IItemListPendingToStockOut[];
  ICountNotif: ICountNotif[];


  constructor(private homeService: HomeService, public auth: UserAuthorizationService, public router: Router) {  }

  ngOnInit(): void {
    this.auth.getCurrentUser()
      .then(userdata => {
        this.user = userdata;
      })
      .catch((error) => {
        this.user = null;
      })
    this.homeService.getItemListPending_To_StockOut().subscribe((itemlistpendingstockout) => {
      this.IItemListPendingToStockOut = itemlistpendingstockout;
    })

    this.loadNotif();
  }

  ngOnDestroy() {
    clearTimeout(this.timeout);
  }

  navigate(url: string): void {
    this.router.navigateByUrl(url)
  }

  private IsButtonActive() {
    this.button_status = "active";
    this.button_isActive = true;
  }

  private getTrxNo_GotoInvOutScreen(trxno: string) {
    this.router.navigateByUrl("/inventory-management/inventory-out/" + trxno)
  }



  public loadNotif() {
/*
    this.timeout = setTimeout(() => {
      this.notificationsService.getAll()
        .subscribe(
          (data: any) => {
            console.log(data);
            this.notifications = data.notifications;
            // Call recursively
            this.getAllNotifs();
          },
          error => {
            console.log(error);
          });
    }, 5000);*/

    this.homeService.getCountNotif().subscribe((countnotif) => {
      this.ICountNotif = countnotif;
      ///////////
      /*if (this.ICountNotif.length < 1) {
        this.isZeroNotif = true;
      } else {
        this.isZeroNotif = false;
      }*/
      ////////////////
    })
  }

}
