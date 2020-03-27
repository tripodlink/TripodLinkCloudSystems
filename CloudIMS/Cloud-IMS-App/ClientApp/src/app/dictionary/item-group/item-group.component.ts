import { Component, OnInit, AfterViewInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { FormBuilder, FormControl, FormGroup, Validators } from "@angular/forms";
import {  ItemGroupServices } from '../../services/itemgroup.service';
import { IiTemGroup } from '../../classes/IitemGroup.interface';
import { ViewChild, ElementRef } from '@angular/core';

@Component({
  selector: 'app-item-group',
  templateUrl: './item-group.component.html'
})


export class ItemGroupComponent{
  itemGroup: IiTemGroup[];
  addItemGroupForm: FormGroup;
  @ViewChild('closeModal', { static: true }) closeModal;
  Status: string;
  icon: string;
  isAdd: boolean;
  idInput: string;
  itemGroupInput: string;
  
  constructor(private itemgroupService: ItemGroupServices, private toastr: ToastrService) {
    this.CreateForm();
  }

  ngOnInit(): void {
    this.LoadData();
  }

  private CreateForm() {
    this.addItemGroupForm = new FormGroup({
      id: new FormControl(),
      itemGroupName: new FormControl()
    })
  }

  private PassData(id, itemGroupName) {
    this.idInput = id;
    this.itemGroupInput = itemGroupName;
    this.CreateForm();
    this.Status = "Edit Changes";
    this.icon = "pencil";
    this.isAdd = false;
  }

  LoadData() {
    this.itemgroupService.getItemGroupData().subscribe((itemGroup) => this.itemGroup = itemGroup);
  }
  private verifyProcess() {

    if (this.isAdd == true) {
      this.insertItemGroup();
    } else {
      this.updateItemGroup();
    }

  }
  private insertItemGroup() {
    let errormessage = "Error";
    this.itemgroupService.insertItemGroup(this.addItemGroupForm.value).subscribe(data => {
      this.toastr.success("Data Saved", "Saved");
      this.LoadData();
      
    },
      error => {
        errormessage = error.error;
        this.toastr.error(errormessage, "Error");
        });
    this.ResetForm();
   this.closeModal.nativeElement.click();
  }

  private updateItemGroup() {
   
    let errormessage = "Error";
    this.itemgroupService.updateItemGroup(this.addItemGroupForm.value).subscribe(data => {
      this.toastr.info("Data Edited", "Edited");
      this.LoadData();
    },

      error => {
        errormessage = error.error;
        this.toastr.error(errormessage, "Error");
      });
  }

  private deleteItemGroup(id) {
    if (confirm("Are you sure to delete" + " " + id)) {
      let errormessage = "Error";
      this.itemgroupService.deleteItemGroup(id).subscribe(data => {
        this.toastr.info("Data" + " " + id + " " + "Successfully Deleted", "Deleted");
        document.getElementById(id).remove();
      },
        error => {
          errormessage = error.error;
          this.toastr.error(errormessage, "Error");
        });
    }
    
  }


  private ClickAdd() {
    this.addItemGroupForm.reset();
    this.Status = "Save Changes";
    this.icon = "floppy-o";
    this.isAdd = true;
    this.idInput = "";
    this.itemGroupInput = "";
  }

  private ResetForm() {
    this.addItemGroupForm.reset();
  }
}
