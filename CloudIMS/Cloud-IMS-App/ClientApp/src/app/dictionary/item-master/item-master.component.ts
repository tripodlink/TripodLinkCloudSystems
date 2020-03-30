import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { FormBuilder, FormControl, FormGroup, Validators } from "@angular/forms";
import { ItemMasterServices } from '../../services/itemmaster.service';
import { IiTemMaster } from '../../classes/data-dictionary/ItemMaster/IitemMaster.interface';
import { IiTemGroup } from '../../classes/data-dictionary/ItemGroup/IitemGroup.interface';
import { IUnitCode } from '../../classes/data-dictionary/UnitCode/IUnitCode.interface';
import { IitemMasterClass } from '../../classes/data-dictionary/ItemMaster/IitemMasterClass'
import { ViewChild, ElementRef } from '@angular/core';
import { error } from '@angular/compiler/src/util';



@Component({
  selector: 'app-item-master',
  templateUrl: './item-master.component.html',
  styleUrls: ['./item-master.component.css']
})
export class ItemMasterComponent implements OnInit {

  @ViewChild('itemid', { static: true }) private itemid: ElementRef;
  @ViewChild('itemGroupNameDropdown', { static: true }) private itemGroupNameDropdown: ElementRef;
  @ViewChild('itemNameInput', { static: true }) private itemNameInput: ElementRef;
  @ViewChild('itemUnitInput', { static: true }) private itemUnitInput: ElementRef;
  @ViewChild('itemSupplierInput', { static: true }) private itemSupplierInput: ElementRef;
  @ViewChild('itemManufacturerInput', { static: true }) private itemManufacturerInput: ElementRef;

  ItemMasterArray: IiTemMaster[];
  itemGroupArray: IiTemGroup[];
  unitCodeArray: IUnitCode[];

  addItemMasterForm: FormGroup;

  Status: string;
  icon: string;
  isAdd: boolean;
  modalStatus: string;

  idInputName: string;
  itemGroupInputName: string;
  itemNameInputName: string;
  itemUnitInputName: string;
  itemSupplierInputName: string;
  itemManufacturerInputName: string;

  constructor(private itemMasterService: ItemMasterServices, private toastr: ToastrService, private builder: FormBuilder) {
    this.createForm();
  }

  ngOnInit() {
    this.LoadData();
  }

  private createForm() {
    this.addItemMasterForm = this.builder.group({
      id: new FormControl(),
      ItemGroup: new FormControl(),
      ItemName: new FormControl(),
      Unit: new FormControl(),
      Supplier: new FormControl(),
      Manufacturer: new FormControl()
    });
  }

  private LoadData() {
    this.itemMasterService.getItemMasterData().subscribe((itemMaster) => this.ItemMasterArray = itemMaster);
    this.itemMasterService.getItemGroupData().subscribe((itemGroup) => this.itemGroupArray = itemGroup);
    this.itemMasterService.getUnitCodeData().subscribe((unitCode) => this.unitCodeArray = unitCode);
  }

  private clickAddItemMaster() {
    this.resetForm();
    this.Status = "Save Changes";
    this.icon = "floppy-o";
    this.isAdd = true;
    this.modalStatus = "Add Item Master";
  }

  private resetForm() {
    this.addItemMasterForm.reset();
  }

  private insertItemMaster() {
    let errormessage = "Error";

    this.itemMasterService.insertItemMaster(this.addItemMasterForm.value).subscribe(data => {
      this.toastr.success("Data Saved", "Saved");
      this.LoadData();
    },
      error => {
        errormessage = error.error;
        this.toastr.error(errormessage, "Error");
      });
    this.resetForm();
  }

  deleteItemMaster(id) {
    if (confirm("Are you sure to delete" + " " + id)) {
      let errormessage = "Error";
      this.itemMasterService.deleteItemMaster(id).subscribe(data => {
        this.toastr.info("Data" + " " + id + " " + "Successfully Deleted", "Deleted");
        document.getElementById(id).remove();
      },
        error => {
          errormessage = error.error;
          this.toastr.error(errormessage, "Error");
        });
    }
  }
  private PassData(id, itemGroup, itemName, unit, supplier, manufacturer) {
    this.Status = "Edit Changes";
    this.icon = "pencil";
    this.isAdd = false;
    this.modalStatus = "Edit Item Group";

    this.idInputName = id ;
    this.itemGroupInputName = itemGroup;
    this.itemNameInputName= itemName;
    this.itemUnitInputName= unit;
    this.itemSupplierInputName = supplier;
    this.itemManufacturerInputName = manufacturer;

    this.itemid.nativeElement.value = id;
    this.itemGroupNameDropdown.nativeElement.value = itemGroup;
    this.itemNameInput.nativeElement.value = itemName;
    this.itemUnitInput.nativeElement.value = unit;
    this.itemSupplierInput.nativeElement.value = supplier;
    this.itemManufacturerInput.nativeElement.value = manufacturer;

    this.itemGroupNameDropdown.nativeElement.dispatchEvent(new Event('change'));
    this.itemUnitInput.nativeElement.dispatchEvent(new Event('change'));
    this.itemSupplierInput.nativeElement.dispatchEvent(new Event('change'));
    this.itemManufacturerInput.nativeElement.dispatchEvent(new Event('change'));

   

  }
}
