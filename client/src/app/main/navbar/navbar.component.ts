import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { AuthService } from 'src/app/auth';

@Component({
    selector: 'app-navbar',
    templateUrl: './navbar.component.html',
    styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {
    title = 'Inner core';
    authStatus$!: Observable<boolean>;

    constructor(private _authService: AuthService) { }

    signout(): void {
        this._authService.signout();
    }

    manageAccount(): void {
        this._authService.manageAccount();
    }

    ngOnInit(): void {
        this.authStatus$ = this._authService.authStatus$;
    }

    getUserName(): Nullable<string> {
        return this._authService.name;
    }
}
