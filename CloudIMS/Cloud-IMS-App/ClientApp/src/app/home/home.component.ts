import { Component } from '@angular/core';
import { HomeService } from '../services/home.service';
import { ProgramMenu } from '../classes/ProgramMenu';
import { UserAuthorizationService } from '../services/UserAuthorization.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {

  constructor(private homeService: HomeService, private auth: UserAuthorizationService) {
  }

}
