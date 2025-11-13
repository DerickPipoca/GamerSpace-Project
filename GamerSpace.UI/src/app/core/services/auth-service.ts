import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { jwtDecode } from 'jwt-decode';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { User } from '../../shared/models/user.model';
import { LoginDto } from '../../shared/models/login.model';
import { LoginResponse } from '../../shared/models/login-response.model';
import { RegisterUserDto } from '../../shared/models/register-user.model';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private readonly apiUrl = environment.apiUrl + '/api/auth';
  private readonly JWT_TOKEN_KEY = 'gamer-space-token';

  private currentUserSubjetct = new BehaviorSubject<User | null>(null);

  private currentUser$ = this.currentUserSubjetct.asObservable();

  constructor(private http: HttpClient) {
    this.loadUserFromToken();
  }

  public login(dto: LoginDto): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(`${this.apiUrl}/login`, dto).pipe(
      tap((response) => {
        this.saveTokenAndNotify(response.token);
      }),
    );
  }

  public register(dto: RegisterUserDto): Observable<User> {
    return this.http.post<User>(`${this.apiUrl}/register`, dto);
  }

  public logout(): void {
    localStorage.removeItem(this.JWT_TOKEN_KEY);
    this.currentUserSubjetct.next(null);
  }

  private saveTokenAndNotify(token: string): void {
    localStorage.setItem(this.JWT_TOKEN_KEY, token);

    const user = this.decodeToken(token);

    this.currentUserSubjetct.next(user);
  }

  private decodeToken(token: string) {
    const decodedToken: any = jwtDecode(token);

    return {
      id: decodedToken.nameid,
      email: decodedToken.email,
      role: decodedToken.role,
      fullName: '',
    };
  }

  private loadUserFromToken() {
    const token = localStorage.getItem(this.JWT_TOKEN_KEY);

    if (token) {
      this.saveTokenAndNotify(token);
    }
  }

  public get currentUserValue(): User | null {
    return this.currentUserSubjetct.value;
  }

  public isLoggedIn(): boolean {
    return !!this.currentUserSubjetct.value;
  }
}
