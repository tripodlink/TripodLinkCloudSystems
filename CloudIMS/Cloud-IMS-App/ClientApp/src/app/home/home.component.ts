import { Component } from '@angular/core';
import { HomeService } from '../services/home.service';
import { ProgramMenu } from '../classes/ProgramMenu';
import { UserAuthorizationService } from '../services/UserAuthorization.service';
import { ICountUser } from '../classes/home/ICountUser.interface';
import { ICountStockIn } from '../classes/home/ICountStockIn.interface';
import { ICountStockOut } from '../classes/home/ICountStockOut.interface';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {

  ICountUser: ICountUser[];
  ICountStockIn: ICountStockIn[];
  ICountStockOut: ICountStockOut[];
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

  }
 
}
