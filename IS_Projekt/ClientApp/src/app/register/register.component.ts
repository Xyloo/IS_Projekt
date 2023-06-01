import { Component } from '@angular/core';
import { AuthService} from '../auth.service'
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {

  username = '';
  password = '';

  constructor(private authService: AuthService, private router: Router) { }

  register() {
    // Creating a user object
    let user = {
      username: this.username,
      password: this.password
    };

    this.authService.register(user).subscribe(
      (response: any) => { // Success callback
        // Store the token on successful registration
        localStorage.setItem('access_token', response.token);
        // Redirect to the login page
        this.router.navigate(['/login']);
      },
      (error: any) => { // Error callback
        // Handle the error
        console.error(error);
      }
    );
  }
}
