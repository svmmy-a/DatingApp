//
// <summary>
// Home page component. Shows welcome UI and registration form.
// Receives member list from App, toggles register form with a signal.
// </summary>
import { Component, Input, signal } from '@angular/core';
import { Register } from "../account/register/register";
import { User } from '../../types/user';

@Component({
  selector: 'app-home',
  imports: [Register],
  templateUrl: './home.html',
  styleUrl: './home.css'
})
export class Home {
  // Input: gets member list from App component
  @Input({required: true}) membersFromApp: User[] = [];
  // Signal: controls whether register form is shown
  protected registerMode = signal(false);

  // Shows the register form
  showRegister() {
    this.registerMode.set(true);
  }
}
