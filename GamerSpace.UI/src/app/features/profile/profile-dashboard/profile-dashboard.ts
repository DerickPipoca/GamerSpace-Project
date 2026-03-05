import { AuthService } from './../../../core/services/auth-service';
import { User } from './../../../shared/models/user.model';
import { Component } from '@angular/core';

@Component({
  selector: 'app-profile-dashboard',
  imports: [],
  templateUrl: './profile-dashboard.html',
  styleUrl: './profile-dashboard.scss',
})
export class ProfileDashboard {
  public currentUser: User | null;
  constructor(authService: AuthService) {
    this.currentUser = authService.currentUserValue;
  }
}
