import { inject } from '@angular/core';
import { AuthService } from './../services/auth-service';
import { CanActivateFn, Router } from '@angular/router';

export const adminGuard: CanActivateFn = (route, state) => {
  const authService = inject(AuthService);
  const router = inject(Router);

  const user = authService.currentUserValue;

  if (user && authService.isLoggedIn() && user.role === 'Admin') {
    return true;
  }

  router.navigate(['/']);
  return false;
};
