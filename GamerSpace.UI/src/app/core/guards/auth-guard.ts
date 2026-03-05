import { AuthService } from './../services/auth-service';
import { CanActivateFn, Router, UrlTree } from '@angular/router';
import { inject } from '@angular/core';

export const authGuard: CanActivateFn = (route, state): boolean | UrlTree => {
  const authService = inject(AuthService);
  const router = inject(Router);

  const isLoggedIn = authService.isAuthenticated();

  if (isLoggedIn) {
    return true;
  }

  return router.createUrlTree(['/login'], {
    queryParams: { returnUrl: state.url },
  });
};
