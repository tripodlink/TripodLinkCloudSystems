import { Component, OnInit } from '@angular/core';
import { DictionaryService } from '../services/dictionary.service';
import { IDictionary } from '../classes/data-dictionary/IDictionary.interface';

@Component({
  selector: 'app-dictionary',
  templateUrl: './dictionary.component.html'
})
export class DictionaryComponent implements OnInit {
  dic_pg_menus: IDictionary[];
  message: string;

  constructor(private dictionaryService: DictionaryService) {
  }

  ngOnInit(): void {
    this.dictionaryService.getProgramMenu().subscribe((dic_pg_menus) => this.dic_pg_menus = dic_pg_menus)
  }
}
