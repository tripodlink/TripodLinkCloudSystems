import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { ManufacturerService } from '../../services/Manufacturer.service';
import { ToastrService } from 'ngx-toastr';
import { FormBuilder, FormGroup, FormControl } from '@angular/forms';
import { IManufacturer } from '../../classes/data-dictionary/Manufacturer/IManufacturer.interface';
import { IManufacturerClass } from '../../classes/data-dictionary/Manufacturer/IManufacturerClass';

@Component({
  selector: 'app-manufacturer',
  templateUrl: './manufacturer.component.html'
})
export class ManufacturerComponent implements OnInit {

  imanufact: IManufacturerClass = new IManufacturerClass();
  manufacturer: IManufacturer[] = new Array();
  addManufacturerFormGroup: FormGroup;
  Status: string;
  icon: string;
  iconHeaderTextModal: string;
  isAdd: boolean;
  modalStatus: string;

  idInput: string;
  manufacturerInput: string;

  constructor(private manufacturerService: ManufacturerService, private toastr: ToastrService, private builder: FormBuilder, public el: ElementRef) {
    this.CreateForm();
  }
  ngOnInit(): void {
    this.getManufacturer();
  }

  getManufacturer() {
    this.manufacturerService.getManufacturer().subscribe((manufacturer) => this.manufacturer = manufacturer)
  }
  private CreateForm() {
    this.addManufacturerFormGroup = this.builder.group({
      ID: [''],
      ManufactName: ['']
    })
  }

  private PassData(ID, ManufactName) {

    this.addManufacturerFormGroup.controls.ID.setValue(ID)
    this.addManufacturerFormGroup.controls.ManufactName.setValue(ManufactName)
    this.Status = "Edit Changes";
    this.icon = "pencil";
    this.isAdd = false;
    this.modalStatus = "Edit Manufacturer";
    this.iconHeaderTextModal = "fa fa-pencil-square";
  }

  private insertManufacturer() {
    let errormessage = "Error";

    let manufactID = this.addManufacturerFormGroup.controls.ID.value
    let manufactName = this.addManufacturerFormGroup.controls.ManufactName.value

    if (manufactID == null || manufactName == null || manufactID == '' || manufactName == '') {

    }
    else {
      this.imanufact.ID = manufactID;
      this.imanufact.ManufactName = manufactName;

      this.manufacturerService.insertManufacturer(this.imanufact).subscribe(data => {

        this.toastr.success("Data Saved", "Saved");
        this.getManufacturer();
      },
        error => {
          errormessage = error.error;
          this.toastr.error(errormessage, "Error");
        });
      this.ResetForm();
    }
  }

  private updateManufacturer() {

    let errormessage = "Error";

    let manufactID = this.addManufacturerFormGroup.controls.ID.value
    let manufactName = this.addManufacturerFormGroup.controls.ManufactName.value

    this.imanufact.ID = manufactID;
    this.imanufact.ManufactName = manufactName;

    this.manufacturerService.updateManufacturer(this.imanufact).subscribe(data => {
      this.toastr.info("Data Edited", "Edited");
      this.getManufacturer();
    },

      error => {
        errormessage = error.error;
        this.toastr.error(errormessage, "Error");
      });
  }
  private deleteManufacturer(id) {
    if (confirm("Are you sure do you want to delete this Manufacturer" + " " + id)) {
      let errormessage = "Error";
      this.manufacturerService.deleteManufacturer(id).subscribe(data => {
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
    this.modalStatus = "Add Manufacturer"
    this.idInput = "";
    this.manufacturerInput = "";
    this.iconHeaderTextModal = "fa fa-plus-square";
  }
  private ResetForm() {
    this.addManufacturerFormGroup.reset();
  }
}
