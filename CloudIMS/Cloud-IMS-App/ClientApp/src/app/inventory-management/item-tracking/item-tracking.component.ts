import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { CookieService } from 'ngx-cookie-service';
import { ToastrService } from 'ngx-toastr';
import { itemLotNo,ItemTracking } from '../../classes/inventory-management/itemTracking/itemTracking.interface';
import { itemLotNoClass, itemTracking} from '../../classes/inventory-management/itemTracking/itemTrackingClass';
import { ItemTrackingServices } from '../../services/itemTracking.service';
import { DatePipe } from '@angular/common';
import { IDepartment } from '../../classes/data-dictionary/Department/IDepartment.interface';
@Component({
  selector: 'app-item-tracking',
  templateUrl: './item-tracking.component.html',
  styleUrls: ['./item-tracking.component.css']
})
export class ItemTrackingComponent implements OnInit {

  itemTrackingHdrForm: FormGroup
  itemTrackingDtlForm: FormGroup
  isValidItemId: boolean = false
  isValidHdr: boolean = false
  errormessage: string;
  lotNumArray: itemLotNo[];
  depListArray: IDepartment[];
  trxNumFunction: string;
  getTrx: string;
  dtlFormArray: ItemTracking = new itemTracking()
  dtlTableArray: ItemTracking[];
  currentLocation: string;
  remainingCount: number
  itemUnit: string
  MinimumCount: number;

  constructor(public toastr: ToastrService, public builder: FormBuilder,
    public cookieService: CookieService,
    public itemTrackingServices: ItemTrackingServices,
    public datepipe: DatePipe
  ) {
    this.createAllForm()
  }

  ngOnInit() {
    this.itemTrackingServices.getDepartmentList().subscribe((depList) => this.depListArray = depList);
  }

  public createAllForm() {
    this.itemTrackingHdrForm = this.builder.group({
      scanItemBarcodeNo: ['', Validators.required],
      serialLotNo: ['', Validators.required],
      
    })
    this.itemTrackingDtlForm = this.builder.group({
      location: ['', Validators.required],
      updateDate: ['', Validators.required],
    })
  }

  public getDateTimeNow(): string {
    let today = new Date();
    let getDate = today.toISOString().slice(0, 10);
    let cutTime = today.toTimeString().slice(0, 5);
    let dateTime = getDate + "T" + cutTime;
    return dateTime;
  }

  public checkItemLotNo(): void {
    let itemBarcode: string = this.itemTrackingHdrForm.controls.scanItemBarcodeNo.value
    if (itemBarcode != "") {
      this.itemTrackingServices.getLotNum(itemBarcode).subscribe((array) => {
        this.lotNumArray = array
        this.isValidItemId = true
      })
    }
  }

  //Generate Trx Number when form is touched
  public getTrxNumFunction() {
    this.itemTrackingServices.getTrxNumFunction().subscribe((data) => {
      this.getTrx = data;
      

      let trxNum = this.getTrx.split('|');
      let type = trxNum[0].toString();
      let year = trxNum[1];
      let convertYear = this.datepipe.transform(new Date(), year.toLowerCase());
      let num = trxNum[2];
      this.trxNumFunction = type + convertYear + num
      this.updateLocation(this.trxNumFunction)
    })
  }


  public updateLocation(transactionNo: string): void {
    this.dtlFormArray.transactionNo = transactionNo
    this.dtlFormArray.itemID = this.itemTrackingHdrForm.controls.scanItemBarcodeNo.value;
    this.dtlFormArray.lotNo = this.itemTrackingHdrForm.controls.serialLotNo.value;
    this.dtlFormArray.dateUpdated = this.itemTrackingDtlForm.controls.updateDate.value as Date
    this.dtlFormArray.location = this.itemTrackingDtlForm.controls.location.value

    this.itemTrackingServices.updateLocation(this.dtlFormArray).subscribe((data) => {
      this.toastr.info("Updated Location");
      this.searchItem()
    },
      error => {
        this.errormessage = error.error;
        this.toastr.error(this.errormessage, "Error");
      })
  }

  public generateDate(): void {
    this.itemTrackingDtlForm.get('updateDate').patchValue(this.getDateTimeNow());
  }

  async searchItem() {
    this.isValidHdr = true
    let ItemID = this.itemTrackingHdrForm.controls.scanItemBarcodeNo.value
    let lotNum = this.itemTrackingHdrForm.controls.serialLotNo.value

    await this.getTrackingData(ItemID,lotNum)
    await this.getCurrentLocation(ItemID, lotNum)
    await this.getRemainingCount(ItemID,lotNum)
    await this.getItemUnit(ItemID, lotNum)
    await this.getItemMinimumStockLevel(ItemID)

  }
  async getTrackingData(itemID: string, lotNum: string) {
    let TrackingData = this.itemTrackingServices.getItemTrackingData(itemID, lotNum)
   await TrackingData.subscribe((data) => {
      this.dtlTableArray = data
    })
  }

  async getCurrentLocation(itemID: string, lotNum: string) {
    let currentLocation = this.itemTrackingServices.getCurrentLocation(itemID, lotNum)
    await currentLocation.toPromise().then((data) => {
      this.currentLocation = data['departmentDescription']
    
    }
    )
  }

  async getRemainingCount(itemID: string, lotNum: string) {
    let remainingCount = this.itemTrackingServices.getRemainingCount(itemID, lotNum)

    await remainingCount.toPromise().then((data) => this.remainingCount = data)
  }

  async getItemUnit(itemID: string, lotNum: string) {
    let itemUnit = this.itemTrackingServices.getItemUnit(itemID, lotNum)

    await itemUnit.toPromise().then((data) => this.itemUnit = data["itemUnit"])
  }

  async getItemMinimumStockLevel(itemID: string) {
    let itemMinimum = this.itemTrackingServices.getItemMinimumLimit(itemID)

    await itemMinimum.toPromise().then((data) => this.MinimumCount = data)
  }

  public deleteItemTrack(itemTrx: string): void {
    if (confirm("Are you sure you want to delete this?")) {
      this.itemTrackingServices.deleteLocationHistory(itemTrx).toPromise().then((result) => {
        this.toastr.info("History Deleted");
        this.searchItem()
      },
        error => {
          this.errormessage = error.error;
          this.toastr.error(this.errormessage, "Error");
        })
    }
  }

  public clearItem(): void {
    this.itemTrackingHdrForm.reset()
    this.isValidHdr =false
  }
}
