import { Component, OnInit } from '@angular/core';
import { DictionaryService } from '../services/dictionary.service';
import { IDictionary } from '../classes/data-dictionary/IDictionary.interface';
import { ProgramMenu } from '../classes/ProgramMenu';
import { UserAuthorizationService } from '../services/UserAuthorization.service';

@Component({
  selector: 'app-dictionary',
  templateUrl: './dictionary.component.html'
})
export class DictionaryComponent implements OnInit {
  dic_pg_menus: ProgramMenu[];
  message: string;

  constructor(private dictionaryService: DictionaryService, private _userAuthorizationService: UserAuthorizationService) {
  }

  ngOnInit(): void {
    this.dic_pg_menus = this._userAuthorizationService.getCurrentUser().programMenus.filter(pm => pm.programFolderID == "DIC");
  }
}
