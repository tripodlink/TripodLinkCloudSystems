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

  @ViewChild('itemid', { static: true }) private itemid: ElementRef;
  @ViewChild('itemgroupnametext', { static: true }) private itemgroupnametext: ElementRef;



  itemGroup: IiTemGroup[];
  addItemGroupForm: FormGroup;
  Status: string;
  icon: string;
  isAdd: boolean;
  modalStatus: string;

  idInput: string;
  itemGroupInput: string;
  
  constructor(private itemgroupService: ItemGroupServices, private toastr: ToastrService, private builder: FormBuilder,
  public el: ElementRef) {
    this.CreateForm();
  }

  ngOnInit(): void {
    this.LoadData();
  }

  private CreateForm() {
    this.addItemGroupForm = this.builder.group({
      id: new FormControl(),
      itemGroupName: new FormControl()
    })

  
  }

  private PassData(id, itemGroupName) {

    this.idInput = id
    this.itemGroupInput = itemGroupName;

    this.itemid.nativeElement.value = id;
    this.itemgroupnametext.nativeElement.value = itemGroupName;
      this.Status = "Edit Changes";
      this.icon = "pencil";
    this.isAdd = false;
  this.modalStatus = "Edit Item Group";
    
  }

  LoadData() {
    this.itemgroupService.getItemGroupData().subscribe((itemGroup) => this.itemGroup = itemGroup);
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
  }

  private updateItemGroup() {

    let dataToUpdate = document.getElementsByClassName('itemgrouptextfield');

    let itemgroup: IiTemGroup = new IitemGroupClass();

    Array.from(dataToUpdate).forEach((element: HTMLInputElement) => {

      if (element.id == "itemid") {
        itemgroup.id = element.value;
      }
      if (element.id == "itemgroupnametext") {
        itemgroup.itemgroupname = element.value;
      }
      
     
    });
    let errormessage = "Error";
    this.itemgroupService.updateItemGroup(itemgroup).subscribe(data => {
      this.toastr.info("Data Edited", "Edited");
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
    this.idInput = "";
    this.itemGroupInput = "";
  }

  private ResetForm() {
    this.addItemGroupForm.reset();
  }
}
