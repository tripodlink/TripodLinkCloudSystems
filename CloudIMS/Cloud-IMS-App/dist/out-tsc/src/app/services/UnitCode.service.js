var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { HttpHeaders } from '@angular/common/http';
let UnitCodeService = class UnitCodeService {
    constructor(_http) {
        this._http = _http;
        this.url = 'api/unitcode';
    }
    getUnitCodes() {
        return this._http.get(this.url);
    }
    insertUnitCodes(units) {
        const headers = new HttpHeaders({
            "Content-Type": "application/json",
            "Accept": "application/json"
        });
        return this._http.post("api/unitcode/Add", JSON.stringify(units), { headers: headers });
    }
};
UnitCodeService = __decorate([
    Injectable(),
    __metadata("design:paramtypes", [HttpClient])
], UnitCodeService);
export { UnitCodeService };
//# sourceMappingURL=UnitCode.service.js.map