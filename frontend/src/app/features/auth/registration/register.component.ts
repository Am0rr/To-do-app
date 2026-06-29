import { Component, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../../core/services/auth.service';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './register.component.html',
})
export class RegisterComponent {
  username = '';
  email = '';
  password = '';
  error = signal<string>('');

  constructor(
    private authService: AuthService,
    private router: Router,
  ) {}

  onSubmit() {
    this.error.set('');
    this.authService
      .register({ username: this.username, email: this.email, password: this.password })
      .subscribe({
        next: () => this.router.navigate(['/login']),
        error: (err) => {
          this.error.set(err.error?.errors?.[0] ?? err.error?.message ?? 'Registration failed');
        },
      });
  }
}
