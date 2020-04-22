import { Component } from '@angular/core';
import { HomeService } from '../services/home.service';
import { ProgramMenu } from '../classes/ProgramMenu';
import { UserAuthorizationService } from '../services/UserAuthorization.service';
import { ICountUser } from '../classes/home/ICountUser.interface';
import { ICountStockIn } from '../classes/home/ICountStockIn.interface';
import { ICountStockOut } from '../classes/home/ICountStockOut.interface';
import { IItemListStockIn } from '../classes/home/IItemListStockIn.interface';
import { IItemListStockOut } from '../classes/home/IItemListStockOut.interface';

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

  constructor(private homeService: HomeService, private auth: UserAuthorizationService) {
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

  }
 
}
