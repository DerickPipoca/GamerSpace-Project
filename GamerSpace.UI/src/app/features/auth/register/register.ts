import { LoginDto } from './../../../shared/models/login.model';
import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService } from '../../../core/services/auth-service';
import { Router } from '@angular/router';
import { FaIconComponent, FaIconLibrary } from '@fortawesome/angular-fontawesome';
import { faEnvelope, faLock, faUser } from '@fortawesome/free-solid-svg-icons';
import { faApple, faFacebook, faGoogle } from '@fortawesome/free-brands-svg-icons';

@Component({
  selector: 'app-register',
  imports: [CommonModule, FaIconComponent, ReactiveFormsModule],
  templateUrl: './register.html',
  styleUrl: './register.scss',
})
export class Register implements OnInit {
  registerForm!: FormGroup;
  public error: string | null = null;

  constructor(
    private authService: AuthService,
    private router: Router,
    lib: FaIconLibrary,
  ) {
    lib.addIcons(faUser, faLock, faEnvelope, faFacebook, faApple, faGoogle);
  }

  ngOnInit(): void {
    this.registerForm = new FormGroup({
      fullName: new FormControl('', [Validators.required]),
      email: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', [Validators.required]),
      reEnterPassword: new FormControl('', [Validators.required]),
      serviceTerms: new FormControl(false, [Validators.requiredTrue]),
    });
  }

  onSubmit() {
    if (this.registerForm.invalid) {
      this.registerForm.markAllAsTouched();
      this.error = 'Por favor, preencha os campos corretamente.';
      return;
    }

    this.error = null;

    const registerDto = this.registerForm.value;

    if (registerDto.password !== registerDto.reEnterPassword) {
      this.error = 'As senhas não são idênticas.';
      return;
    }

    if (!registerDto.serviceTerms) {
      this.error = 'Você deve aceitar os termos de serviço.';
      return;
    }

    const payload = {
      fullName: registerDto.fullName,
      email: registerDto.email,
      password: registerDto.password,
    };

    this.authService.register(payload).subscribe({
      next: (response) => {
        console.log('Conta criada com sucesso!', response.fullName);
        alert('Conta criada com sucesso! ' + response.fullName);

        const loginDto = { email: response.email, password: registerDto.password };
        this.authService.login(loginDto).subscribe({
          next: () => {
            this.router.navigate(['home']);
          },
          error: () => {
            this.error = 'Conta criada, mas falha no login automático. Tente entrar manualmente.';
            this.router.navigate(['login']);
          },
        });
      },
      error: (err) => {
        if (err.error.errors) {
          this.error = err.error.errors.Password[0];
        } else if (err.error.detail) {
          this.error = err.error.detail;
        } else {
          this.error = 'Falha no login.';
        }
        console.error(err);
      },
    });
  }
}
