"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
exports.__esModule = true;
var core_1 = require("@angular/core");
require("rxjs/add/operator/map");
require("rxjs/add/operator/catch");
require("rxjs/add/operator/toPromise");
var SupplierService = /** @class */ (function () {
  function SupplierService(_http) {
        this._http = _http;
    }
  SupplierService.prototype.get = function () {
        return this._http.get("")
            .map(function (response) { return response.json(); });
    };
  SupplierService = __decorate([
        core_1.Injectable()
  ], SupplierService);
  return SupplierService;
}());
exports.SupplierService = SupplierService;
