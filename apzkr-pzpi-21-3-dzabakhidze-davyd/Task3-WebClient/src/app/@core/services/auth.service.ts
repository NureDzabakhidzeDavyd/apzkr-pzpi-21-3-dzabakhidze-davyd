import {HttpClient, HttpHeaders} from "@angular/common/http";
import {Observable, of} from "rxjs";
import {Contact} from "../../models";
import {Injectable} from "@angular/core";
import {AuthenticatedResponse} from "../../models/authenticated-response";

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  constructor(private readonly _http: HttpClient) {}

  getProfile(): Observable<Contact> {
   const apiUrl = 'http://localhost:5000/api/v1/auth/profile';
    return this._http.get<any>(apiUrl);
  }

  logout(): Observable<void> {
    return new Observable<void>((observer) => {
      localStorage.removeItem('jwt');
      localStorage.removeItem('refreshToken');
      observer.next();
      observer.complete();
    });
  }

  register(registerUser: any): Observable<any> {
    const apiUrl = 'http://localhost:5000/api/v1/auth';
    return this._http.post(`${apiUrl}/register`, registerUser);
  }

  async tryRefreshingTokens(token: string, refreshToken: string): Promise<boolean> {
    if (!token || !refreshToken) {
      return false;
    }

    const credentials = JSON.stringify({ accessToken: token, refreshToken: refreshToken });

    try {
      const refreshRes = await this._http.post<AuthenticatedResponse>(
        'http://localhost:5000/api/v1/auth/refresh',
        credentials,
        {
          headers: new HttpHeaders({
            'Content-Type': 'application/json'
          })
        }
      ).subscribe(authResponse => {
        localStorage.setItem('jwt', authResponse!.token);
        localStorage.setItem('refreshToken', authResponse!.refreshToken);
      });

      return true;
    } catch (error) {
      return false;
    }
  }
}
