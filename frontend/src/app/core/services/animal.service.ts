import { Injectable, inject, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { Animal, CreateAnimal, UpdateAnimal } from '../models/animal.model';

@Injectable({ providedIn: 'root' })
export class AnimalService {
  private readonly http = inject(HttpClient);
  private readonly apiUrl = `${environment.apiUrl}/animais`;

  readonly loading = signal(false);
  readonly error = signal<string | null>(null);

  getAll(): Observable<Animal[]> {
    return this.http.get<Animal[]>(this.apiUrl);
  }

  getById(id: number): Observable<Animal> {
    return this.http.get<Animal>(`${this.apiUrl}/${id}`);
  }

  create(dto: CreateAnimal): Observable<Animal> {
    return this.http.post<Animal>(this.apiUrl, dto);
  }

  update(id: number, dto: UpdateAnimal): Observable<Animal> {
    return this.http.put<Animal>(`${this.apiUrl}/${id}`, dto);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
