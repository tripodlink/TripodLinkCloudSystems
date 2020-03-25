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
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
let UserAccountService = class UserAccountService {
    constructor(_http) {
        this._http = _http;
        this.url = 'api/useraccount';
    }
    getAllUsers() {
        return this._http.get(this.url);
    }
    findUserByIdOrName(search_key) {
        let params = new HttpParams().set('search_key', search_key);
        return this._http.get(this.url + "/findUserIdOrName", { params: params });
    }
    addUser(user) {
        const headers = new HttpHeaders({
            "Content-Type": "application/json",
            "Accept": "application/json"
        });
        return this._http.post(this.url + "/Add", JSON.stringify(user), { headers: headers });
    }
    updateUser(user) {
        const headers = new HttpHeaders({
            "Content-Type": "application/json",
            "Accept": "application/json"
        });
        return this._http.post(this.url + "/update", JSON.stringify(user), { headers: headers });
    }
    deleteUser(id) {
        const headers = new HttpHeaders({
            "Content-Type": "application/json",
            "Accept": "application/json"
        });
        let params = new HttpParams().set('id', id);
        return this._http.post(this.url + "/delete", { params: params }, { headers: headers });
    }
};
UserAccountService = __decorate([
    Injectable(),
    __metadata("design:paramtypes", [HttpClient])
], UserAccountService);
export { UserAccountService };
//# sourceMappingURL=user-account.service.js.map