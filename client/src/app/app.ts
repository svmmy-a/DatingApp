//
// <summary>
// Root Angular component. Bootstraps the app, fetches members from backend, and manages current user state.
// Connects to backend API via HttpClient and uses Angular signals for reactivity.
// </summary>
import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit, signal } from '@angular/core';
import { lastValueFrom } from 'rxjs';
import { Nav } from "../layout/nav/nav";
import { AccountService } from '../core/services/account-service';
import { Home } from "../features/home/home";
import { User } from '../types/user';

@Component({
  selector: 'app-root',
  imports: [Nav, Home],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App implements OnInit{
  // Inject AccountService for auth/user state
  private accountService = inject(AccountService);
  // Inject HttpClient for API calls
  private http = inject(HttpClient);
  // App title for display
  protected readonly title = 'Dating app';
  // Signal holds the list of members (reactive)
  protected members = signal<User[]>([]);

  // On component init, fetch members and set current user from localStorage
  async ngOnInit() {
    this.members.set(await this.getMembers())
    this.setCurrentUser();
  }

  // Loads user from localStorage and updates AccountService signal
  setCurrentUser() {
    const userString = localStorage.getItem('user');
    if (!userString) return;
    const user = JSON.parse(userString);
    this.accountService.currentUser.set(user);
    }

  // Fetches members from backend API (returns a Promise)
  async getMembers() {
    try {
    return lastValueFrom(this.http.get<User[]>('https://localhost:5001/api/members'));
  } catch (error) {
    console.log(error);
    throw error;
  }
}
}
