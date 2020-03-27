var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
import { Component } from '@angular/core';
import { FormControl, FormGroup } from "@angular/forms";
import { UnitCodeService } from '../../services/UnitCode.service';
import { ToastrService } from 'ngx-toastr';
let UnitCodeComponent = class UnitCodeComponent {
    constructor(unitcodeService, toastr) {
        this.unitcodeService = unitcodeService;
        this.toastr = toastr;
        this.CreateForm();
    }
    CreateForm() {
        this.addForm = new FormGroup({
            Code: new FormControl(),
            Description: new FormControl(),
            ShortDescription: new FormControl()
        });
    }
    ngOnInit() {
        this.reloadData();
    }
    reloadData() {
        this.unitcodeService.getUnitCodes().subscribe((unitCodes) => this.unitCodes = unitCodes);
    }
    insertUnitCodes() {
        let errormessage = "Error";
        this.unitcodeService.insertUnitCodes(this.addForm.value).subscribe(data => {
            this.toastr.success("Data Saved", "Saved");
            this.reloadData();
            this.addForm.reset();
        }, error => {
            errormessage = error.error;
            this.toastr.error(errormessage, "Error");
            this.addForm.reset();
        });
    }
};
UnitCodeComponent = __decorate([
    Component({
        selector: 'dic-unitCode',
        templateUrl: './unitCode.component.html',
    }),
    __metadata("design:paramtypes", [UnitCodeService, ToastrService])
], UnitCodeComponent);
export { UnitCodeComponent };
//# sourceMappingURL=unitCode.component.js.map