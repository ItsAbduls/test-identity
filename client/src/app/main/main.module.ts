import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { HomeComponent } from './home/home.component';
import { MainRoutingModule } from './main-routing.module';
import { NavbarComponent } from './navbar/navbar.component';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { MaterialModule } from '../material.module';
import { FeaturesModule } from '../features/features.module';

@NgModule({
    imports: [
        CommonModule,
        MainRoutingModule,
        MaterialModule,
        FeaturesModule
    ],
    declarations: [
        NavbarComponent,
        HomeComponent,
        PageNotFoundComponent
    ],
    exports: [
        NavbarComponent,
        HomeComponent,
        PageNotFoundComponent
    ]
})
export class MainModule { }
