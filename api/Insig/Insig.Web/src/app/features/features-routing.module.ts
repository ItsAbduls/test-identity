import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from '@app/core/guards/auth.guard';
import { LoginSuccessComponent } from './account/login-success/login-success.component';
import { TfaSetupComponent } from './account/tfa-setup/tfa-setup.component';

import { SampleComponent } from './sample/sample.component';

const routes: Routes = [
    {
        path: 'sample',
        component: SampleComponent,
        canActivate: [AuthGuard]
    },
    {
        path: '2fa',
        component: TfaSetupComponent
    },
    {
        path: 'congratulation',
        component: LoginSuccessComponent
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class FeaturesRoutingModule { }
