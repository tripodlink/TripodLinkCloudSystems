import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { FormBuilder, FormControl, FormGroup, Validators } from "@angular/forms";
import { ItemMasterServices } from '../../services/itemmaster.service';
import { IiTemMaster } from '../../classes/data-dictionary/ItemMaster/IitemMaster.interface';
import { IiTemMasterUnit } from '../../classes/data-dictionary/ItemMasterUnit/IitemMaster.interface';
import { IiTemGroup } from '../../classes/data-dictionary/ItemGroup/IitemGroup.interface';
import { IUnitCode } from '../../classes/data-dictionary/UnitCode/IUnitCode.interface';
import { IitemMasterClass } from '../../classes/data-dictionary/ItemMaster/IitemMasterClass';
import { IitemMasterUnitClass } from '../../classes/data-dictionary/ItemMasterUnit/IitemMasterClass';
import { ViewChild, ElementRef } from '@angular/core';
import { ItemMasterUnitServices } from '../../services/itemmasterUnit.service';
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
  ItemMasterUnitArray: IiTemMasterUnit[];

  UnitCodeSelectArray: IUnitCode[];

  addItemMasterForm: FormGroup;
  addItemMasterUnitForm: FormGroup;

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

  ItemMasterUnitName: string;
  itemMasterUnitID: string;

  constructor(private itemMasterService: ItemMasterServices,private toastr: ToastrService, private builder: FormBuilder) {
    this.createForm();
    this.createFormItemMasterUnit();
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
      Manufacturer: new FormControl()});
  }

  private createFormItemMasterUnit() {
    this.addItemMasterUnitForm = this.builder.group({
      id: new FormControl(),
      itemMasterUnitUnit: new FormControl(),
      itemMasterUnitConversion: new FormControl()
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

    let itemMasterData= document.getElementsByClassName('itemGroupByClass');

    let itemMaster: IiTemMaster = new IitemMasterClass();

    Array.from(itemMasterData).forEach((element: HTMLInputElement) => {

      if (element.id == "itemid") {
        itemMaster.id = element.value;
      }
      if (element.id == "itemGroupNameDropdown") {
        itemMaster.ItemGroup = element.value;
      }
      if (element.id == "itemNameInput") {
        itemMaster.ItemName = element.value;
      }
      if (element.id == "itemUnitInput") {
        itemMaster.Unit = element.value;
      }
      if (element.id == "itemSupplierInput") {
        itemMaster.Supplier = element.value;
      }
      if (element.id == "itemManufacturerInput") {
        itemMaster.Manufacturer = element.value;
      }
    });

    this.itemMasterService.insertItemMaster(itemMaster).subscribe(data => {
      this.toastr.success("Data Saved", "Saved");
      this.LoadData();
    },
      error => {
        errormessage = error.error;
        this.toastr.error(errormessage, "Error");
      });
    this.insertItemMasterUnits();
    this.resetForm();
  }

  private deleteItemMaster(id) {
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

  private insertItemMasterUnits() {

    let itemMasterUnitData = document.getElementsByClassName('itemGroupByClass');

    let itemMasterUnit: IiTemMasterUnit = new IitemMasterUnitClass();

    Array.from(itemMasterUnitData).forEach((element: HTMLInputElement) => {

      if (element.id == "itemid") {
        itemMasterUnit.id = element.value;
      }
      if (element.id == "itemUnitInput") {
        itemMasterUnit.itemMasterUnitUnit = element.value;
      }

    });
  
    itemMasterUnit.itemMasterUnitConversion = "1";
    let errormessage = "Error";
    this.itemMasterService.insertItemMasterUnit(itemMasterUnit).subscribe(data => {
      this.toastr.success("Data Saved", "Saved");
    },
      error => {
      errormessage = error.error;
    this.toastr.error(errormessage, "Error");
  });
  }

  private PassData(id, itemGroup, itemName, unit, supplier, manufacturer) {
    this.Status = "Edit Changes";
    this.icon = "pencil";
    this.isAdd = false;
    this.modalStatus = "Edit Item Group";

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
  private updateItemMaster() {
    let formDatatoUpdate = document.getElementsByClassName('itemGroupByClass');

    let itemMaster: IiTemMaster = new IitemMasterClass();

    Array.from(formDatatoUpdate).forEach((element: HTMLInputElement) => {

      if (element.id == "itemid") {
        itemMaster.id = element.value;
      }
      if (element.id == "itemGroupNameDropdown") {
        itemMaster.ItemGroup = element.value;
      }
      if (element.id == "itemNameInput") {
        itemMaster.ItemName = element.value;
      }
      if (element.id == "itemUnitInput") {
        itemMaster.Unit = element.value;
      }
      if (element.id == "itemSupplierInput") {
        itemMaster.Supplier = element.value;
      }
      if (element.id == "itemManufacturerInput") {
        itemMaster.Manufacturer = element.value;
      }
    });

    let errormessage = "Error";

    this.itemMasterService.updateItemMaster(itemMaster).subscribe(data => {
      this.toastr.info("Data Edited", "Edited");
      this.LoadData();
    },

      error => {
        errormessage = error.error;
        this.toastr.error(errormessage, "Error");
      });
  }
  private passDataItemMaster(name,itemId) {
    this.ItemMasterUnitName = name;

    this.itemMasterUnitID = itemId;

    this.loadItemMasterUnit();

    this.itemMasterService.getItemMasterUnitByID(itemId).subscribe((data => this.ItemMasterUnitArray = data));
   
  }

  private loadItemMasterUnit() {
    this.itemMasterService.getUnitCodeData().subscribe((unitCode) => this.UnitCodeSelectArray = unitCode);
  }

  private saveItemMasterUnit() {
    let itemMasterUnitData = document.getElementsByClassName('itemMasterUnitClass');

    let itemMasterUnit: IiTemMasterUnit = new IitemMasterUnitClass();

    Array.from(itemMasterUnitData).forEach((element: HTMLInputElement) => {

      if (element.id == "itemMasterUnitUnit") {
        itemMasterUnit.itemMasterUnitUnit = element.value;
      }
      if (element.id == "itemMasterUnitConversionID") {
        itemMasterUnit.itemMasterUnitConversion = element.value;
      }
    })

    let errormessage = "Error";

    itemMasterUnit.id = this.itemMasterUnitID;
    this.itemMasterService.insertItemMasterUnit(itemMasterUnit).subscribe(data => {
      this.toastr.info("Data Saved", "Saved");
      this.itemMasterService.getItemMasterUnitByID(this.itemMasterUnitID).subscribe((data => this.ItemMasterUnitArray = data));
    },
      error => {
        errormessage = error.error;
        this.toastr.error(errormessage, "Error");
      });
 
    this.addItemMasterUnitForm.reset();
   
  }

  deleteItemMasterUnit() {
    if (confirm("Are you sure to delete" + " " + this.itemMasterUnitID)) {
      let errormessage = "Error";
      this.itemMasterService.deleteItemMasterUnit(this.itemMasterUnitID).subscribe(data => {
        this.toastr.info("Data" + " " + this.itemMasterUnitID + " " + "Successfully Deleted", "Deleted");
        document.getElementById(this.itemMasterUnitID).remove();
      },
        error => {
          errormessage = error.error;
          this.toastr.error(errormessage, "Error");
        });
    }
  }

}
