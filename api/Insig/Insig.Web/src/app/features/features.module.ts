import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MaterialModule } from '@app/material.module';

import { LoginModule } from './account/login/login.module';
import { LogoutModule } from './account/logout/logout.module';
import { RegisterModule } from './account/register/register.module';
import { FeaturesRoutingModule } from './features-routing.module';
import { SampleComponent } from './sample/sample.component';
import { TfaSetupComponent } from './account/tfa-setup/tfa-setup.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { LoginSuccessComponent } from './account/login-success/login-success.component';
import { GroupComponent } from './SignalRComponents/group/group.component';
import { MessageComponent } from './SignalRComponents/message/message.component';
import { VoteComponent } from './SignalRComponents/vote/vote.component';
import { TimeHubComponent } from './SignalRComponents/time-hub/time-hub.component';

@NgModule({
    imports: [
        CommonModule,
        FeaturesRoutingModule,
        MaterialModule,
        LoginModule,
        RegisterModule,
        LogoutModule,
        FormsModule,
        ReactiveFormsModule
    ],
    declarations: [
        SampleComponent,
        TfaSetupComponent,
        LoginSuccessComponent,
        // signalR components
        GroupComponent,
        MessageComponent,
        VoteComponent,
        TimeHubComponent

    ],
    exports: [
        SampleComponent,
        // signalR components
        GroupComponent,
        MessageComponent,
        VoteComponent,
        TimeHubComponent
    ]
})
export class FeaturesModule { }
