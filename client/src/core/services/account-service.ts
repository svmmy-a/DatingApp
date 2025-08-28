//
// <summary>
// Angular service for authentication and user state.
// Talks to backend API for login, manages JWT and current user using signals and localStorage.
// </summary>
import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { LoginCreds, RegisterCreds, User } from '../../types/user';
import { tap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  // Inject HttpClient for API calls
  private http = inject(HttpClient);
  // Signal for current user (null if not logged in)
  currentUser = signal<User | null>(null);

  // Base URL for backend API
  baseUrl = 'https://localhost:5001/api/';

  register(creds: RegisterCreds) {
    return this.http.post<User>(this.baseUrl + 'account/register', creds).pipe(
      tap(user => {
        if (user) {
          this.setCurrentUser(user)
        }
      })
    )   
  } 

  // Sends login request to backend, updates user state and localStorage on success
  login(creds: LoginCreds) {
    return this.http.post<User>(this.baseUrl + 'account/login', creds).pipe(
      tap(user => {
        if (user) {
          this.setCurrentUser(user)
        }
      })
    )
  }

  setCurrentUser(user: User) {
    localStorage.setItem('user', JSON.stringify(user))
    this.currentUser.set(user)
  }
  // Logs out user (clears localStorage and signal)
  logout() {
    localStorage.removeItem('user');
    this.currentUser.set(null);
  }
}
