import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { FormBuilder, FormControl, FormGroup, Validators } from "@angular/forms";
import { ItemMasterServices } from '../../services/itemmaster.service';
import { IiTemMaster } from '../../classes/data-dictionary/ItemMaster/IitemMaster.interface';
import { IiTemMasterUnit } from '../../classes/data-dictionary/ItemMasterUnit/IitemMasterUnit.interface';
import { IiTemGroup } from '../../classes/data-dictionary/ItemGroup/IitemGroup.interface';
import { IUnitCode } from '../../classes/data-dictionary/UnitCode/IUnitCode.interface';
import { IitemMasterClass } from '../../classes/data-dictionary/ItemMaster/IitemMasterClass';
import { IitemMasterUnitClass } from '../../classes/data-dictionary/ItemMasterUnit/IitemMasterUnitClass';
import { ViewChild, ElementRef } from '@angular/core';
import { ItemMasterUnitServices } from '../../services/itemmasterUnit.service';

import { IiTemMasterUnitJoinUnit} from '../../classes/data-dictionary/ItemMasterUnit/IitemMasterUnitJoinUnit.interface';



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
  @ViewChild('itemMasterUnitUnit', { static: true }) private itemMasterUnitUnit: ElementRef;
  @ViewChild('itemMasterUnitConversionID', { static: true }) private itemMasterUnitConversionID: ElementRef;



  ItemMasterArray: IiTemMaster[];
  itemGroupArray: IiTemGroup[];
  unitCodeArray: IUnitCode[];
  ItemMasterUnitArray: IiTemMasterUnitJoinUnit[];

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
  isITMUAdd: boolean;
  deleteUnit: string;

  constructor(private itemMasterService: ItemMasterServices, private toastr: ToastrService, private builder: FormBuilder) {
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
      Manufacturer: new FormControl()
    });
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

    let itemMasterData = document.getElementsByClassName('itemGroupByClass');

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
      this.toastr.success("New Item Master Saved", "Saved");
      this.LoadData();
    },
      error => {
        errormessage = error.error;
        this.toastr.error(errormessage, "Item Master Error");
      });
    this.insertItemMasterUnits();
    this.resetForm();
  }

  private deleteItemMaster(id) {
    if (confirm("Are you sure to delete" + " " + id)) {
      let errormessage = "Error";
      this.itemMasterService.deleteItemMaster(id).subscribe(data => {
        this.toastr.info("Item Master" + " " + id + " " + "Successfully Deleted", "Deleted");
        this.itemMasterService.deleteAllItemMasterUnit(id).subscribe();
        document.getElementById(id).remove();
      },
        error => {
          errormessage = error.error;
          this.toastr.error(errormessage, "Item Master Error");
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
      this.toastr.success("New Item Master Unit", "Saved");
    },
      error => {
        errormessage = error.error;
        this.toastr.error(errormessage, "Item Master Unit Error Insert");
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
      this.toastr.info("Item Master Data Updated", "Edited");
      this.itemMasterService.deleteAllItemMasterUnit(itemMaster.id).subscribe();
      this.insertItemMasterUnits();
      this.LoadData();
    },

      error => {
        errormessage = error.error;
        this.toastr.error(errormessage, "Error");
      });
  
  }

  private passDataItemMaster(name, itemId, itemUnit) {
    this.ItemMasterUnitName = name;

    this.itemMasterUnitID = itemId;

    this.loadItemMasterUnit();

    this.isITMUAdd = true;

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
      this.toastr.info("New Item Master Saved", "Saved");
      this.itemMasterService.getItemMasterUnitByID(this.itemMasterUnitID).subscribe((data => this.ItemMasterUnitArray = data));
    },
      error => {
        errormessage = error.error;
        this.toastr.error(errormessage, "Item Master Error");
      });

    this.addItemMasterUnitForm.reset();

  }

  deleteItemMasterUnit(unit) {
    if (confirm("Are you sure to delete" + " " + this.itemMasterUnitID)) {
      let errormessage = "Error";
      this.itemMasterService.deleteItemMasterUnit(this.itemMasterUnitID, unit).subscribe(data => {
        this.toastr.info("Item Master Unit" + " " + this.itemMasterUnitID + " " + "Successfully Deleted", "Deleted");
        document.getElementById(unit).remove();
      },
        error => {
          errormessage = error.error;
          this.toastr.error(errormessage, "Item Master Unit Error");
        });
    }
  }
  passDataItemMasterUnit(unit, conversion) {

    this.isITMUAdd = false;

    this.itemMasterUnitUnit.nativeElement.value = unit;

    this.itemMasterUnitConversionID.nativeElement.value = conversion;

    this.itemMasterUnitUnit.nativeElement.dispatchEvent(new Event('change'));
  }

  updateItemMasterUnit() {
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

    this.itemMasterService.updateItemMasterUnit(itemMasterUnit).subscribe(data => {
      this.toastr.info("Item Master Unit Successfully", "Updated");
      this.itemMasterService.getItemMasterUnitByID(this.itemMasterUnitID).subscribe((data => this.ItemMasterUnitArray = data));
      this.addItemMasterUnitForm.reset();
      this.isITMUAdd = true;
    },
      error => {
        errormessage = error.error;
        this.toastr.error(errormessage, "Item Master Unit Error");
      });

   
  }
  clearItemMasterUnit() {
    this.isITMUAdd = true;
    this.addItemMasterUnitForm.reset();
  }
}
