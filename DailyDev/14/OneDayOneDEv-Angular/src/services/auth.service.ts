import { HttpClient } from '@angular/common/http'
import { Observable } from 'rxjs';
import { User } from '../models/User';
import { Injectable } from '@angular/core';
import { authresponse } from '../models/auth-reponse';


@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private api_url: string = "https://localhost:7180/api/auth/login";

  constructor(private http: HttpClient) {

  }

  isLoggedIn(): boolean {
    const token = localStorage.getItem('token');
    const expiration = localStorage.getItem('tokenExpiration');

    if (!token || !expiration) {
      return false;
    }

    const expirationDate = new Date(expiration);

    if (new Date() > expirationDate) {
      this.logout();
      return false;
    }

    return true;
  }

  login(user: User) {

    const formData = new FormData();

    formData.append('Username', user.Username);
    formData.append('Password', user.Password);

    return this.http.post<authresponse>(
      this.api_url,
      formData
    );
  }

  isTokenExpired(): boolean {

    const expiration = localStorage.getItem('tokenExpiration');

    if (!expiration) return true;

    const expirationDate = new Date(expiration);

    return new Date() > expirationDate;
  }

  logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('tokenExpiration');

    window.location.href = '/';
  }
}
