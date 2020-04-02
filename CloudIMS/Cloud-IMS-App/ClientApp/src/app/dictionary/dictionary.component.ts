import { Component, OnInit } from '@angular/core';
import { DictionaryService } from '../services/dictionary.service';
import { IDictionary } from '../classes/data-dictionary/IDictionary.interface';
import { ProgramMenu } from '../classes/ProgramMenu';
import { UserAuthorizationService } from '../services/UserAuthorization.service';
import { UserAccount } from '../classes/UserAccount';


@Component({
  selector: 'app-dictionary',
  templateUrl: './dictionary.component.html'
})
export class DictionaryComponent implements OnInit {
  dic_pg_menus: ProgramMenu[] = new Array();
  message: string;

  constructor(private dictionaryService: DictionaryService, private _userAuthorizationService: UserAuthorizationService) {
  }

  ngOnInit(): void {
    let user: UserAccount = this._userAuthorizationService.getCurrentUser("Dictionary");

    if (user != null) {
      this.dic_pg_menus = user.programMenus.filter(pm => pm.programFolderID == "DIC");
    }
  }
}
