import { Component } from '@angular/core';
import { HomeService } from '../services/home.service';
import { ProgramMenu } from '../classes/ProgramMenu';
import { UserAuthorizationService } from '../services/UserAuthorization.service';
import { ICountUser } from '../classes/home/ICountUser.interface';
import { ICountStockIn } from '../classes/home/ICountStockIn.interface';
import { ICountStockOut } from '../classes/home/ICountStockOut.interface';
import { IItemListStockIn } from '../classes/home/IItemListStockIn.interface';
import { IItemListStockOut } from '../classes/home/IItemListStockOut.interface';
import { Router } from '@angular/router';
import { IItemListPendingToStockOut } from '../classes/home/IItemListPendingToStockOut.interface';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {

  ICountUser: ICountUser[];
  ICountStockIn: ICountStockIn[];
  ICountStockOut: ICountStockOut[];
  IItemListStockIn: IItemListStockIn[];
  IItemListStockOut: IItemListStockOut[];
  IItemListPendingToStockOut: IItemListPendingToStockOut[];

  constructor(private homeService: HomeService, private auth: UserAuthorizationService,private router: Router) {
  }
  ngOnInit() {
    this.homeService.getCountUser().subscribe((countuser) => {
      this.ICountUser = countuser;
    })
    this.homeService.getCountStockIn().subscribe((countstockin) => {
      this.ICountStockIn = countstockin;
    })
     this.homeService.getCountStockOut().subscribe((countstockout) => {
       this.ICountStockOut = countstockout;
    })
      this.homeService.getItemListStockIn().subscribe((itemliststockin) => {
        this.IItemListStockIn = itemliststockin;
    })
     this.homeService.getItemListStockOut().subscribe((itemliststockout) => {
       this.IItemListStockOut = itemliststockout;
    })
    this.homeService.getItemListPending_To_StockOut().subscribe((itemlistpendingstockout) => {
      this.IItemListPendingToStockOut = itemlistpendingstockout;
    })

  }
  private getTrxNo_GotoInvOutScreen(trxno: string) {
    this.router.navigateByUrl("/inventory-management/inventory-out/"+trxno)
  }
 
}
