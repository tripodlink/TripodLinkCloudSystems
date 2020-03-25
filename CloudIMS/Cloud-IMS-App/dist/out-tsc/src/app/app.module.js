var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { Http } from '@angular/http';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { UnitCodeComponent } from './dictionary/unit-code/unitCode.component';
import { UnitCodeService } from './services/UnitCode.service';
import { UserAccountComponent } from './user-management/user-account/user-account.component';
import { UserAccountService } from './services/UserAccount.service';
import { CommonModule } from '@angular/common';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
let AppModule = class AppModule {
};
AppModule = __decorate([
    NgModule({
        declarations: [
            AppComponent,
            NavMenuComponent,
            HomeComponent,
            CounterComponent,
            FetchDataComponent,
            UnitCodeComponent,
            UserAccountComponent
        ],
        imports: [
            BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
            HttpClientModule,
            FormsModule,
            ReactiveFormsModule,
            CommonModule,
            BrowserAnimationsModule,
            ToastrModule.forRoot({
                timeOut: 3000,
                positionClass: 'toast-top-right',
                preventDuplicates: true,
            }),
            RouterModule.forRoot([
                { path: '', component: HomeComponent, pathMatch: 'full' },
                { path: 'counter', component: CounterComponent },
                { path: 'fetch-data', component: FetchDataComponent },
                { path: 'dictionary/unit-code', component: UnitCodeComponent },
                { path: 'user-management/user-account', component: UserAccountComponent },
            ])
        ],
        providers: [UnitCodeService, UserAccountService, Http],
        bootstrap: [AppComponent]
    })
], AppModule);
export { AppModule };
//# sourceMappingURL=app.module.js.map