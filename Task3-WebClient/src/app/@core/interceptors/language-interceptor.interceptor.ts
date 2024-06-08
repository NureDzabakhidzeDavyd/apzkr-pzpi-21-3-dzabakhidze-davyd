import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { TranslateService } from '@ngx-translate/core';

@Injectable()
export class LanguageInterceptor implements HttpInterceptor {

  constructor(private translate: TranslateService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    const currentLang = this.translate.currentLang || this.translate.defaultLang;
    const language = currentLang === 'ua' ? 'uk-UA' : 'en-US';
    const modifiedRequest = request.clone({
      setHeaders: { 'Accept-Language': language }
    });
    return next.handle(modifiedRequest);
  }
}
