//
// <summary>
// Angular component for user registration form.
// Receives member list from parent, collects user input, and will send to backend.
// </summary>
import { Component, inject, input, output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RegisterCreds, User } from '../../../types/user';
import { AccountService } from '../../../core/services/account-service';

@Component({
  selector: 'app-register',
  imports: [FormsModule],
  templateUrl: './register.html',
  styleUrl: './register.css'
})
export class Register {
private accountService = inject(AccountService);

  cancelRegister = output<boolean>();
  // Holds registration form data
  protected creds = {} as RegisterCreds; 

  // Called when form is submitted
  register() {
    // TODO: Send creds to backend via AccountService
    this.accountService.register(this.creds).subscribe({
      next: response => {
        console.log(response);
        this.cancel();
      },
      error: error => console.log(error)
    })
  }

  // Called when user cancels registration
  cancel() {
    this.cancelRegister.emit(false);
  }
}
