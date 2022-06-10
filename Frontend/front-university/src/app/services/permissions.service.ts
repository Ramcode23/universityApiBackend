import { Injectable } from '@angular/core';
import jwtDecode, { JwtPayload } from 'jwt-decode';
import { StorageService } from './storage/storage.service';

@Injectable({
  providedIn: 'root'
})
export class PermissionsService {

  constructor(private _storageService: StorageService) { }


  public getPermise() {
    const token = this._storageService.getStorage('jwtToken') || '';
    const decoded = jwtDecode<JwtPayload>(token);
    // get object from token
    const index = Object.keys(decoded).findIndex(key => key.includes('role'));
    const roleValue = Object.values(decoded)[index];

    if (roleValue === 'Administrator') {
      return true;
    }
    return false;



  }
}
