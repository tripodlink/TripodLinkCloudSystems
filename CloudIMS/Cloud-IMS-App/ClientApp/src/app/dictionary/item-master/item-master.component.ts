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
import { IAllDataDictionaryJoin} from '../../classes/data-dictionary/alldatadictionary/alldatadictionaryjoin.interface';
import { ISupplier} from '../../classes/data-dictionary/Supplier/ISupplier.interface';
import { IManufacturer} from '../../classes/data-dictionary/Manufacturer/IManufacturer.interface';

@Component({
  selector: 'app-item-master',
  templateUrl: './item-master.component.html',
  styleUrls: ['./item-master.component.css']
})
export class ItemMasterComponent implements OnInit {
  ItemMasterArray: IiTemMaster[];
  ItemMasterForm: IiTemMaster = new IitemMasterClass();

  itemGroupArray: IiTemGroup[];
  unitCodeArray: IUnitCode[];
  UnitCodeSelectArray: IUnitCode[];

  ItemMasterUnitArray: IiTemMasterUnitJoinUnit[];
  ItemMasterUnitForm: IiTemMasterUnit = new IitemMasterUnitClass();

  supplierDataArray: ISupplier[];
  manufactDataArray: IManufacturer[];

  showAllDataDic: IAllDataDictionaryJoin[];

  addItemMasterForm: FormGroup;
  addItemMasterUnitForm: FormGroup;

  Status: string;
  icon: string;
  isAdd: boolean;
  modalStatus: string;

  itemGroupInputName: string;
  itemNameInputName: string;
  itemUnitInputName: string;
  itemSupplierInputName: string;
  itemManufacturerInputName: string;

  ItemMasterUnitName: string;
  itemMasterUnitID: string;
  isITMUAdd: boolean;
  deleteUnit: string;

  isButtonDisabled: boolean;
  isEditItemUnit: boolean;

  constructor(private itemMasterService: ItemMasterServices, private toastr: ToastrService, private builder: FormBuilder) {
    this.createForm();
    this.createFormItemMasterUnit();
  }

  ngOnInit() {
    this.LoadData();
  }
  private createForm() {
    this.addItemMasterForm = this.builder.group({
      id: [Validators.required],
      ItemGroup: [Validators.required],
      ItemName: [Validators.required],
      Unit: [Validators.required],
      Supplier: [Validators.required],
      Manufacturer: [Validators.required]
    });
  }

  private createFormItemMasterUnit() {
    this.addItemMasterUnitForm = this.builder.group({
      id: new FormControl(),
      itemMasterUnitUnit: [{disabled: this.isEditItemUnit }, Validators.required],
      itemMasterUnitConversion: [Validators.required]
    });
  }

  private LoadData() {
    //this.itemMasterService.getItemMasterData().subscribe((itemMaster) => this.ItemMasterArray = itemMaster);
    this.itemMasterService.getAllDataDic().subscribe((dataDic) => this.showAllDataDic = dataDic);
    this.itemMasterService.getItemGroupData().subscribe((itemGroup) => this.itemGroupArray = itemGroup);
    this.itemMasterService.getUnitCodeData().subscribe((unitCode) => this.unitCodeArray = unitCode);
    this.itemMasterService.getSupplierData().subscribe((suppData) => this.supplierDataArray = suppData);
    this.itemMasterService.getManuData().subscribe((manuData) => this.manufactDataArray = manuData);

  }

  itemChanges() {
    let dropDownValue = (<HTMLSelectElement>document.getElementById('itemMasterUnitUnit')).value;
   
    if (dropDownValue == "") {
      this.isButtonDisabled = true;
    }
    else {
      this.isButtonDisabled = false;
    }

    if (this.isEditItemUnit == true) {
      this.addItemMasterUnitForm.controls.itemMasterUnitUnit.disabled;
    } else {
      this.addItemMasterUnitForm.controls.itemMasterUnitUnit.enabled;
    }
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

    this.ItemMasterForm.id = this.addItemMasterForm.controls.id.value;
    this.ItemMasterForm.ItemGroup = this.addItemMasterForm.controls.ItemGroup.value;
    this.ItemMasterForm.ItemName = this.addItemMasterForm.controls.ItemName.value;
    this.ItemMasterForm.Unit = this.addItemMasterForm.controls.Unit.value;
    this.ItemMasterForm.Supplier = this.addItemMasterForm.controls.Supplier.value;
    this.ItemMasterForm.Manufacturer = this.addItemMasterForm.controls.Manufacturer.value;

    if (this.addItemMasterForm.controls.id.value.trim() != "" || this.addItemMasterForm.controls.ItemName.value.trim() != "") {
      this.itemMasterService.insertItemMaster(this.ItemMasterForm).subscribe(data => {
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
  private PassData(id, itemGroup, itemName, unit, supplier, manufacturer) {
    this.Status = "Edit Changes";
    this.icon = "pencil";
    this.isAdd = false;
    this.modalStatus = "Edit Item Group";

    this.addItemMasterForm.controls.id.setValue(id);
    this.addItemMasterForm.controls.ItemGroup.setValue(itemGroup);
    this.addItemMasterForm.controls.ItemName.setValue(itemName);
    this.addItemMasterForm.controls.Unit.setValue(unit);
    this.addItemMasterForm.controls.Supplier.setValue(supplier);
    this.addItemMasterForm.controls.Manufacturer.setValue(manufacturer);
  }

  private updateItemMaster() {

    this.ItemMasterForm.id = this.addItemMasterForm.controls.id.value;
    this.ItemMasterForm.ItemGroup = this.addItemMasterForm.controls.ItemGroup.value;
    this.ItemMasterForm.ItemName = this.addItemMasterForm.controls.ItemName.value;
    this.ItemMasterForm.Unit = this.addItemMasterForm.controls.Unit.value;
    this.ItemMasterForm.Supplier = this.addItemMasterForm.controls.Supplier.value;
    this.ItemMasterForm.Manufacturer = this.addItemMasterForm.controls.Manufacturer.value;

    let errormessage = "Error";

    this.itemMasterService.updateItemMaster(this.ItemMasterForm).subscribe(data => {
      this.toastr.info("Item Master Data Updated", "Edited");
      this.itemMasterService.deleteAllItemMasterUnit(this.ItemMasterForm.id).subscribe();
      this.insertItemMasterUnits();
      this.LoadData();
    },

      error => {
        errormessage = error.error;
        this.toastr.error(errormessage, "Error");
      });
  
  }

  private passDataItemMaster(name, itemId) {
    this.ItemMasterUnitName = name;
    this.itemMasterUnitID = itemId;
    this.loadItemMasterUnit();
    this.isITMUAdd = true;
    this.addItemMasterUnitForm.reset();
    this.itemMasterService.getItemMasterUnitByID(itemId).subscribe((data => this.ItemMasterUnitArray = data));

  }

  private loadItemMasterUnit() {
    this.itemMasterService.getUnitCodeData().subscribe((unitCode) => this.UnitCodeSelectArray = unitCode);
  }

  private insertItemMasterUnits() {

    this.ItemMasterUnitForm.id = this.addItemMasterForm.controls.id.value;
    this.ItemMasterUnitForm.itemMasterUnitUnit = this.addItemMasterForm.controls.Unit.value;
    this.ItemMasterUnitForm.itemMasterUnitConversion = "1";

    let errormessage = "Error";

    this.itemMasterService.insertItemMasterUnit(this.ItemMasterUnitForm).subscribe(data => {
      this.toastr.success("New Item Master Unit", "Saved");
    },
      error => {
        errormessage = error.error;
        this.toastr.error(errormessage, "Item Master Unit Error Insert");
      });
  }

  private saveItemMasterUnit() {
  
    var conversion = this.addItemMasterUnitForm.controls.itemMasterUnitConversion.value;
    let convertString = String(conversion);

    this.ItemMasterUnitForm.id = this.itemMasterUnitID;
    this.ItemMasterUnitForm.itemMasterUnitUnit = this.addItemMasterUnitForm.controls.itemMasterUnitUnit.value;
    this.ItemMasterUnitForm.itemMasterUnitConversion = convertString;

    let errormessage = "Error";

    this.itemMasterService.insertItemMasterUnit(this.ItemMasterUnitForm).subscribe(data => {
      this.toastr.info("New Item Master Unit Saved", "Saved");
      this.itemMasterService.getItemMasterUnitByID(this.itemMasterUnitID).subscribe((data => this.ItemMasterUnitArray = data));
    },
      error => {
        errormessage = error.error;
        this.toastr.error(errormessage, "Item Master Unit Error");
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
    this.addItemMasterUnitForm.controls.itemMasterUnitUnit.setValue(unit);
    this.addItemMasterUnitForm.controls.itemMasterUnitConversion.setValue(conversion);

    this.isEditItemUnit = true;
    //this.createFormItemMasterUnit();
  }

  updateItemMasterUnit() {
    var conversion = this.addItemMasterUnitForm.controls.itemMasterUnitConversion.value;
    let convertString = String(conversion);

    this.ItemMasterUnitForm.id = this.itemMasterUnitID;
    this.ItemMasterUnitForm.itemMasterUnitUnit = this.addItemMasterUnitForm.controls.itemMasterUnitUnit.value;
    this.ItemMasterUnitForm.itemMasterUnitConversion = convertString;

    let errormessage = "Error";

   
    this.itemMasterService.updateItemMasterUnit(this.ItemMasterUnitForm).subscribe(data => {
      this.toastr.info("Item Master Unit Successfully", "Updated");
      this.itemMasterService.getItemMasterUnitByID(this.itemMasterUnitID).subscribe((data => this.ItemMasterUnitArray = data));
      this.addItemMasterUnitForm.reset();
      this.isITMUAdd = true;
      this.isEditItemUnit = false;
    },
      error => {
        errormessage = error.error;
        this.toastr.error(errormessage, "Item Master Unit Error");
      });

   
  }
  clearItemMasterUnit() {
    this.isITMUAdd = true;
    this.isButtonDisabled = false;
    this.addItemMasterUnitForm.reset();
    this.isEditItemUnit = false;
  }
}
