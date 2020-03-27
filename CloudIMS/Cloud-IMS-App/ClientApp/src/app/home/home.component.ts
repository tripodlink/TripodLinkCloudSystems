import { Component } from '@angular/core';
import { HomeService } from '../services/home.service';
import { IHome } from '../classes/IHome.interface';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  //pm_menus: IHome[];
  //message: string;

  //constructor(private homeService: HomeService) {
  //}

  //ngOnInit(): void {
  //  this.homeService.getProgramMenu().subscribe((pm_menus) => this.pm_menus = pm_menus)
  //}
}
