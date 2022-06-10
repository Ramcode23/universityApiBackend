import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Register } from 'src/app/Intefaces/resgiste';
import { RegisterStudent } from 'src/app/services/api/models';
import { AuthService } from 'src/app/services/auth.service';
import { StorageService } from 'src/app/services/storage/storage.service';

@Component({
  selector: 'app-register-form',
  templateUrl: './register-form.component.html',
  styleUrls: ['./register-form.component.css']
})
export class RegisterFormComponent implements OnInit {
  registerForm: FormGroup = this._formBuilder.group({});
  constructor(
    private _formBuilder: FormBuilder,
    private _router: Router,
    private _authService: AuthService,
    private _storageService: StorageService
  ) { }

  ngOnInit(): void {
    this.registerForm = this._formBuilder.group({
      email: ['',
        [ Validators.required,Validators.email],
      ],
      password: ['', [ Validators.required, Validators.minLength(6) ]],

      firstName: ['', Validators.required,
      ],
      lastName: ['', Validators.required,
      ],
    });
  }

  register() {
    let userRegister = this.registerForm.value as RegisterStudent;
    this._authService.authRegisterUser(userRegister).subscribe(
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
          console.log(`[Error]: Somenthing wrong happend: ${err}`);
          this.registerForm.reset();
          this._storageService.removeStorage('jwtToken');
        },
        complete: () => {
          console.log('Authentication porcess finished');

        }


      });
    }

}
