import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '@app/auth';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-tfa-setup',
  templateUrl: './tfa-setup.component.html',
  styleUrls: ['./tfa-setup.component.scss']
})
export class TfaSetupComponent implements OnInit {

  tfa: any = {};
  authcode: string = "";
  errorMessage: string = "";
  isVerified: boolean = false;
  isGenerate: boolean = false;
  email: string;

  tfaVerificationForm = this._fb.group({
    passCode: ['', [Validators.required, Validators.minLength(6)]],
  });

  constructor(private _fb: FormBuilder, private _authService: AuthService, private _toastr: ToastrService, private _router: Router) {
    console.log("any2fa values = ", this._router?.getCurrentNavigation()?.extras?.state); // should log out 'bar'
    this.tfa = this._router?.getCurrentNavigation()?.extras?.state;
  }

  ngOnInit(): void {
  }

  verify2fa() {
    if (this.tfaVerificationForm.valid) {
      this._authService.verfiyTfa(this.tfaVerificationForm.value.passCode, this.tfa?.Email).subscribe({
        next: _ => {
          if (_) {
            this._toastr.success('Tfa has been successfully verfied.', 'Thank you for registering.');
            this._router.navigate(['login']);
          } else {
            this._toastr.error('Please enter valid 2fa code.', "Please try again.");
            this._toastr.info('Please install google authenticator and and setup google tfa by clicking generate tfa', "Let me help, If you don't have tfa setup.");
          }

        },
        error: error => {
          this._toastr.error(error.error[0].description);
        }
      });
    }
  }

  generate2fa() {
    if (this.tfa == undefined || this.tfa?.Email != null) {
      this._authService.generateTfa(this.email).subscribe({
        next: _ => {
          if (_) {
            this._toastr.success('2fa has been successfully created. Please verify your 2fa.', '2fa created');
            this.tfa = _;
            this.isGenerate = true;
          } else {
            this._toastr.error('Email not registered.Please register first.', "Please entered valid email");
            this.isGenerate = false;
          }

        },
        error: error => {
          this._toastr.error(error.error[0].description);
          this.isGenerate = false;
        }
      });
    }
  }

}
