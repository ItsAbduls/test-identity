import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/auth';

@Component({
  selector: 'app-login-success',
  templateUrl: './login-success.component.html',
  styleUrls: ['./login-success.component.scss']
})
export class LoginSuccessComponent implements OnInit {

  constructor(private _authService: AuthService) { }

  ngOnInit(): void {
  }

  login(): void {
    this._authService.login();
  }

}
