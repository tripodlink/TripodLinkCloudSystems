import { Component } from '@angular/core';
import { UserAuthorizationService } from './services/UserAuthorization.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent {
  constructor(private auth: UserAuthorizationService) {
  }
}
