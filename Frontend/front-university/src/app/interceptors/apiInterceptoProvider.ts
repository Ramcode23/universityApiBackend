import { HTTP_INTERCEPTORS } from "@angular/common/http";
import { forwardRef, Provider } from "@angular/core";
import { ApiInterceptor } from "./api.interceptor";

export const API_INTERCEPTOR_PROVIDER: Provider = {
  provide: HTTP_INTERCEPTORS,
  useExisting: forwardRef(() => ApiInterceptor),
  multi: true
};
