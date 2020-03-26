import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { FormBuilder, FormControl, FormGroup, Validators } from "@angular/forms";
import {  ItemGroupServices } from '../../services/itemgroup.service';
import { IiTemGroup } from '../../classes/IitemGroup.interface';

@Component({
  selector: 'app-item-group',
  templateUrl: './item-group.component.html'
})
export class ItemGroupComponent{
  itemGroup: IiTemGroup[];
  constructor(private itemgroupService: ItemGroupServices, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.LoadData();
  }
  LoadData() {
    this.itemgroupService.GetItemGroupData().subscribe((itemGroup) => this.itemGroup = itemGroup);
  }
  showModalHTML() {
  }
}
