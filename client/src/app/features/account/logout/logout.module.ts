import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';

import { LogoutRoutingModule } from './logout-routing.module';
import { LogoutComponent } from './logout.component';
import { MaterialModule } from 'src/app/material.module';
import { SharedModule } from 'src/app/shared/shared.module';

@NgModule({
    imports: [
        CommonModule,
        LogoutRoutingModule,
        ReactiveFormsModule,
        MaterialModule,
        SharedModule
    ],
    declarations: [LogoutComponent]
})
export class LogoutModule { }
