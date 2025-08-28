//
// <summary>
// Navigation bar component. Handles login/logout and displays user info.
// Uses AccountService for authentication and signals for reactivity.
// </summary>
import { Component, inject, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../../core/services/account-service';

@Component({
  selector: 'app-nav',
  imports: [FormsModule],
  templateUrl: './nav.html',
  styleUrl: './nav.css'
})
export class Nav {
  // Inject AccountService for login/logout and user state
  protected accountService = inject(AccountService)
  // Holds login form credentials
  protected creds: any = {}

    // Called when login form is submitted
    login() {
      this.accountService.login(this.creds).subscribe({
        next: result => {
          // On success, clear form
          console.log(result);
          this.creds = {};
        },
        error: error => alert(error.message)
      })
    }

    // Logs out user
    logout() {
      this.accountService.logout();
    }
  }
