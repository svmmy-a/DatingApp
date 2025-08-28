//
// <summary>
// Angular component for user registration form.
// Receives member list from parent, collects user input, and will send to backend.
// </summary>
import { Component, input } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RegisterCreds, User } from '../../../types/user';

@Component({
  selector: 'app-register',
  imports: [FormsModule],
  templateUrl: './register.html',
  styleUrl: './register.css'
})
export class Register {
  // Input signal: gets member list from parent (Home)
  membersFromHome = input.required<User[]>()
  // Holds registration form data
  protected creds = {} as RegisterCreds;

  // Called when form is submitted
  register() {
    // TODO: Send creds to backend via AccountService
    console.log(this.creds);
  }

  // Called when user cancels registration
  cancel() {
    console.log('cancelled!');
  }
}
