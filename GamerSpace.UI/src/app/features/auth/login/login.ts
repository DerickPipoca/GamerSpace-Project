import { AuthService } from './../../../core/services/auth-service';
import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { FaIconLibrary, FaIconComponent } from '@fortawesome/angular-fontawesome';
import { faApple, faFacebook, faGoogle } from '@fortawesome/free-brands-svg-icons';
import { faLock, faUser } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, FaIconComponent, RouterLink],
  templateUrl: './login.html',
  styleUrl: './login.scss',
})
export class Login implements OnInit {
  loginForm!: FormGroup;
  public error: string | null = null;

  constructor(
    private authService: AuthService,
    private router: Router,
    lib: FaIconLibrary,
  ) {
    lib.addIcons(faUser, faLock, faFacebook, faApple, faGoogle);
  }

  ngOnInit(): void {
    this.loginForm = new FormGroup({
      email: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', [Validators.required]),
    });
  }

  onSubmit(): void {
    if (this.loginForm.invalid) {
      this.error = 'Por favor, preencha os campos corretamente.';
      return;
    }

    this.error = null;

    const loginDto = this.loginForm.value;

    this.authService.login(loginDto).subscribe({
      next: (response) => {
        console.log('Login bem-sucedido!', response.token);
        alert('Login bem-sucedido!');
        this.router.navigate(['home']);
      },
      error: (err) => {
        this.error = err.error.detail || 'Falha no login.';
        console.error(err);
      },
    });
  }
}
