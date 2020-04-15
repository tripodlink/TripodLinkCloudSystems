import { Component, OnInit, AfterViewInit, Input } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { FormBuilder, FormControl, FormGroup, Validators } from "@angular/forms";
import { ItemGroupServices } from '../../services/itemgroup.service';
import { IiTemGroup } from '../../classes/data-dictionary/ItemGroup/IitemGroup.interface';
import { IitemGroupClass } from '../../classes/data-dictionary/ItemGroup/IitemGroupClass';
import { ViewChild, ElementRef } from '@angular/core';


@Component({
  selector: 'app-item-group',
  templateUrl: './item-group.component.html'
})


export class ItemGroupComponent{

  itemGroup: IiTemGroup[];
  itemGroupForm: IiTemGroup = new IitemGroupClass();
  addItemGroupForm: FormGroup;
  Status: string;
  icon: string;
  isAdd: boolean;
  modalStatus: string;
  
  constructor(private itemgroupService: ItemGroupServices, private toastr: ToastrService, private builder: FormBuilder,
  public el: ElementRef) {
    this.CreateForm();
  }

  ngOnInit(): void {
    this.LoadData();
  }

  private CreateForm() {
    this.addItemGroupForm = this.builder.group({
      id: [''],
      itemGroupName: ['']
    })

  
  }

  private PassData(id, itemGroupName) {

    this.addItemGroupForm.controls.id.setValue(id);
    this.addItemGroupForm.controls.itemGroupName.setValue(itemGroupName);
      this.Status = "Edit Changes";
      this.icon = "pencil";
    this.isAdd = false;
  this.modalStatus = "Edit Item Group";
    
  }

  LoadData() {
    this.itemgroupService.getItemGroupData().subscribe((itemGroup) => this.itemGroup = itemGroup);
  }

  private insertItemGroup() {

    this.itemGroupForm.id = this.addItemGroupForm.controls.id.value;
    this.itemGroupForm.itemgroupname = this.addItemGroupForm.controls.itemGroupName.value;

    if (this.addItemGroupForm.controls.id.value.trim() != "" || this.addItemGroupForm.controls.itemGroupName.value.trim() != "") {
      let errormessage = "Error";
      this.itemgroupService.insertItemGroup(this.itemGroupForm).subscribe(data => {
        this.toastr.success("Data Saved", "Saved");
        this.LoadData();

      },
        error => {
          errormessage = error.error;
          this.toastr.error(errormessage, "Error");
        });
      this.ResetForm();
    }
  }

  private updateItemGroup() {
    this.itemGroupForm.id = this.addItemGroupForm.controls.id.value;
    this.itemGroupForm.itemgroupname = this.addItemGroupForm.controls.itemGroupName.value;

    let errormessage = "Error";
    this.itemgroupService.updateItemGroup(this.itemGroupForm).subscribe(data => {
      this.toastr.info("Item Group Data Edited", "Edited");
      this.LoadData();
    },

      error => {
        errormessage = error.error;
        this.toastr.error(errormessage, "Error");
      });
   
  }

  private deleteItemGroup(id: string) {
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
    this.ResetForm();
    this.Status = "Save Changes";
    this.icon = "floppy-o";
    this.isAdd = true;
    this.modalStatus = "Add Item Group";
  }

  private ResetForm() {
    this.addItemGroupForm.reset();
  }
}
