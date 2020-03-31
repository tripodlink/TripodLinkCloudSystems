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

  @ViewChild('manufactID', { static: true }) private manufactID: ElementRef;
  @ViewChild('manufactNametext', { static: true }) private manufactNametext: ElementRef;

  manufacturer: IManufacturer[];
  addManufacturerFormGroup: FormGroup;
  Status: string;
  icon: string;
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
      ID: new FormControl(),
      ManufactName: new FormControl()
    })
  }

  private PassData(ID, ManufactName) {

    this.manufactID.nativeElement.value = ID;
    this.manufactNametext.nativeElement.value = ManufactName;
    this.Status = "Edit Changes";
    this.icon = "pencil";
    this.isAdd = false;
    this.modalStatus = "Edit Manufacturer";
  }

  private insertManufacturer() {
    let errormessage = "Error";
    this.manufacturerService.insertManufacturer(this.addManufacturerFormGroup.value).subscribe(data => {

      this.toastr.success("Data Saved", "Saved");
      this.getManufacturer();
    },
      error => {
        errormessage = error.error;
        this.toastr.error(errormessage, "Error");
      });
    this.ResetForm();
  }

  private updateManufacturer() {

    let dataToUpdate = document.getElementsByClassName('manufacturertextfield');
    let manufact: IManufacturer = new IManufacturerClass();

    Array.from(dataToUpdate).forEach((element: HTMLInputElement) => {

      if (element.id == "manufactID") {
        manufact.ID = element.value;
      }
      if (element.id == "manufactNametext") {
        manufact.ManufactName = element.value;
      }
    });


    let errormessage = "Error";
    this.manufacturerService.updateManufacturer(manufact).subscribe(data => {
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
  }
  private ResetForm() {
    this.addManufacturerFormGroup.reset();
  }
}
