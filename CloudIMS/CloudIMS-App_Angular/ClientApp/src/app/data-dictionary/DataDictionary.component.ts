import { Component } from '@angular/core';
@Component({
    selector: 'app-data-dictionary',
    templateUrl: './DataDictionary.component.html',
})
export class DataDictionaryComponent {
   

  public createButton() {
      for (let i = 0; i < 10; i++) {
        console.log(i);
    };

  }
}
