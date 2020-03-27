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
import { ToastrService } from 'ngx-toastr';
import { ItemGroupServices } from '../../services/itemgroup.service';
let ItemGroupComponent = class ItemGroupComponent {
    constructor(itemgroupService, toastr) {
        this.itemgroupService = itemgroupService;
        this.toastr = toastr;
    }
    ngOnInit() {
        this.LoadData();
    }
    LoadData() {
        this.itemgroupService.GetItemGroupData().subscribe((itemGroup) => this.itemGroup = itemGroup);
    }
    showModalHTML() {
    }
};
ItemGroupComponent = __decorate([
    Component({
        selector: 'app-item-group',
        templateUrl: './item-group.component.html'
    }),
    __metadata("design:paramtypes", [ItemGroupServices, ToastrService])
], ItemGroupComponent);
export { ItemGroupComponent };
//# sourceMappingURL=item-group.component.js.map