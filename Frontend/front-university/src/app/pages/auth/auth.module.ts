import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AuthRoutingModule } from './auth-routing.module';
import { AuthComponent } from './auth.component';
 import { LoginPageComponent } from './login-page/login-page.component';
import { RegisterPageComponent } from './register-page/register-page.component' ;
import { ComponentsModule } from 'src/app/components/components.module';



@NgModule({
  declarations: [
    AuthComponent,
     LoginPageComponent,
    RegisterPageComponent,

  ],
  imports: [
    CommonModule,
    AuthRoutingModule,
   ComponentsModule,
  ]
})
export class AuthModule { }
