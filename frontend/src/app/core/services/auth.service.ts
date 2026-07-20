import { Injectable, computed, inject, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, tap } from 'rxjs';
import { environment } from '../../../environments/environment';
import { LoginRequest, LoginResponse } from '../models/auth.model';

const TOKEN_KEY = 'petshop.token';
const EXPIRACAO_KEY = 'petshop.dataExpiracao';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private readonly http = inject(HttpClient);
  private readonly apiUrl = `${environment.apiUrl}/auth`;

  private readonly token = signal<string | null>(localStorage.getItem(TOKEN_KEY));

  readonly isAuthenticated = computed(() => !!this.token() && !this.isExpired());

  login(credentials: LoginRequest): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(`${this.apiUrl}/login`, credentials).pipe(
      tap((res) => this.setSession(res)),
    );
  }

  logout(): void {
    localStorage.removeItem(TOKEN_KEY);
    localStorage.removeItem(EXPIRACAO_KEY);
    this.token.set(null);
  }

  getToken(): string | null {
    return this.token();
  }

  private setSession(res: LoginResponse): void {
    localStorage.setItem(TOKEN_KEY, res.token);
    localStorage.setItem(EXPIRACAO_KEY, res.dataExpiracao);
    this.token.set(res.token);
  }

  private isExpired(): boolean {
    const expiracao = localStorage.getItem(EXPIRACAO_KEY);
    if (!expiracao) return true;
    return new Date(expiracao).getTime() <= Date.now();
  }
}
