import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { AuthService } from '../../../core/services/auth-service';
import { Router, RouterLink, RouterOutlet } from '@angular/router';
import { AsyncPipe } from '@angular/common';

@Component({
  selector: 'app-profile-layout',
  imports: [RouterOutlet, RouterLink, AsyncPipe],
  templateUrl: './profile-layout.html',
  styleUrl: './profile-layout.scss',
})
export class ProfileLayout {
  isAdmin$: Observable<boolean>;

  constructor(
    private authService: AuthService,
    private router: Router,
  ) {
    this.isAdmin$ = this.authService.isAdmin$;
  }

  public logout() {
    this.authService.logout();
    this.router.navigate(['/home']);
  }
}
