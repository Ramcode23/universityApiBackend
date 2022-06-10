import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable, tap } from 'rxjs';
import { StorageService } from '../services/storage/storage.service';

@Injectable()
export class ApiInterceptor implements HttpInterceptor {

  constructor( private _storageService:StorageService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
   // Apply the headers
   request = request.clone({
     setHeaders: {
       'Authorization': 'Bearer '+ this._storageService.getStorage('jwtToken')
      }

    });
    console.log(request, 'request');

  // Also handle errors globally
  return next.handle(request).pipe(
    tap(x => x, err => {
      // Handle this err
      console.error(`Error performing request, status code = ${err.status}`);
    })
  );
}
}
