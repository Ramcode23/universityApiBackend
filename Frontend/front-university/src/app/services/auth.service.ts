import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Register } from '../Intefaces/resgiste';
import { RegisterStudent, UserLogins } from './api/models';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  apiUrl = environment.apiUrl+'Account';
  constructor(private http: HttpClient) { }

  authUser(userName: string, password: string) {
    return this.http.post<UserLogins>(this.apiUrl + '/login', {
      userName: userName,
      password: password
    });
  }
  authRegisterUser(registerStudent: RegisterStudent) {
    return this.http.post<UserLogins>(this.apiUrl + '/register',
      registerStudent
    );
  }

  createmanager(manager: Register) {
    return this.http.post<UserLogins>(this.apiUrl + '/createmanager', {
      manager
    });
  }
}
