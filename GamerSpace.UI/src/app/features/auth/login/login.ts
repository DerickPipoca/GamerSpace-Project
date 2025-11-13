import { AuthService } from './../../../core/services/auth-service';
import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './login.html',
  styleUrl: './login.scss',
})
export class Login implements OnInit {
  loginForm!: FormGroup;
  public error: string | null = null;

  constructor(
    private authService: AuthService,
    private router: Router,
  ) {}

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
      },
      error: (err) => {
        this.error = err.error.detail || 'Falha no login.';
        console.error(err);
      },
    });
  }
}
