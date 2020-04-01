import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { FormBuilder, FormControl, FormGroup, Validators } from "@angular/forms";
import { ViewChild, ElementRef } from '@angular/core';
import { IiTemMasterUnit } from '../../classes/data-dictionary/ItemMasterUnit/IitemMaster.interface';
import { IitemMasterUnitClass } from '../../classes/data-dictionary/ItemMasterUnit/IitemMasterClass';
import { ItemMasterUnitServices } from '../../services/itemmasterUnit.service';
import { error } from '@angular/compiler/src/util';


@Component({
  selector: 'app-item-master-unit',
  templateUrl: './item-master-unit.component.html',
  styleUrls: ['./item-master-unit.component.css']
})
export class ItemMasterUnitComponent implements OnInit {


  ItemMasterUnitArray: IiTemMasterUnit[];
  ItemMasterUnitSelectArray: string[];

  constructor(private itemMasterUnitService: ItemMasterUnitServices, private toastr: ToastrService, private builder: FormBuilder,
    public el: ElementRef) { }

  ngOnInit() {
    this.LoadData();
    this.loadSelectData();
  }
  LoadData() {
    this.itemMasterUnitService.getItemMasterData().subscribe((itemMasterUnit) => this.ItemMasterUnitArray = itemMasterUnit);
  }
  loadSelectData() {
    this.itemMasterUnitService.getDistinctIndex().subscribe((itemMasterUnit) => this.ItemMasterUnitSelectArray = itemMasterUnit);
  }
  itemChanges() {
    let errormessage = "Error";
    let value = (<HTMLSelectElement>document.getElementById('itemSelect')).value;
    this.itemMasterUnitService.getItemMasterUnitByID(value).subscribe(data => {
      this.ItemMasterUnitArray = data
    },
      error => {
        this.toastr.error(errormessage, "No data Found");
      });
  }
}
