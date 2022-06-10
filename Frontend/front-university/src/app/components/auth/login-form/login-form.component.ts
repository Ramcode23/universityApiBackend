import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { StorageService } from 'src/app/services/storage/storage.service';

@Component({
  selector: 'app-login-form',
  templateUrl: './login-form.component.html',
  styleUrls: ['./login-form.component.css']
})
export class LoginFormComponent implements OnInit {
  loginForm: FormGroup = this._formBuilder.group({});

  constructor(
    private _formBuilder: FormBuilder,
    private _router: Router,
    private _authService: AuthService,
    private _storageService: StorageService
  ) { }


  ngOnInit(): void {
    this._storageService.removeStorage('jwtToken');
    this.loginForm = this._formBuilder.group({
      userName: ['admin@gmail.com',
        Validators.required,

      ],
      password: ['Pass1234@', Validators.required,
      ]

    });
  }

  login() {
    console.table(this.loginForm.value);
    let { userName, password } = this.loginForm.value;
    this._authService.authUser(userName, password).subscribe(
      {
        next: (response: any) => {
          if (response.token) {
            console.log('User Token: ', response.token);

            let token = response.token.token;
            this._storageService.setStorage('jwtToken', token);
            this._router.navigate(['main/students']);

          }
        },
        error: (err: any) => {
          console.log(`[Error]: Somenthing wrong happend: ${err.error}`);

          this._storageService.removeStorage('jwtToken');
        },
        complete: () => {
          console.log('Authentication porcess finished');

        }


      });

  }
}
