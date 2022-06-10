import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MaterialModule } from './modules/material/material.module';

import { LayoutModule } from '@angular/cdk/layout';



import { RegisterFormComponent } from './components/auth/register-form/register-form.component';
import { ReactiveFormsModule } from '@angular/forms';

import { HttpClientModule } from '@angular/common/http';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { ApiInterceptor } from './interceptors/api.interceptor';
import { API_INTERCEPTOR_PROVIDER } from './interceptors/apiInterceptoProvider';





@NgModule({
  declarations: [
    AppComponent],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MaterialModule,
    ReactiveFormsModule,
    HttpClientModule

  ],
  providers: [
    ApiInterceptor,
    API_INTERCEPTOR_PROVIDER

  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
