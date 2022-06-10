import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class StorageService {

  constructor() { }

  getStorage(key: string) {
    return sessionStorage.getItem(key);
  }

  setStorage(key: string,value: string) {
    sessionStorage.setItem(key, value);
  }
  removeStorage(key: string) {
    sessionStorage.removeItem(key);
  }

}
