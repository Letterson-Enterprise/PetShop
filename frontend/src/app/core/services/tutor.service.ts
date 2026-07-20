import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { CreateTutor, Tutor, UpdateTutor } from '../models/tutor.model';

@Injectable({ providedIn: 'root' })
export class TutorService {
  private readonly http = inject(HttpClient);
  private readonly apiUrl = `${environment.apiUrl}/tutores`;

  getAll(): Observable<Tutor[]> {
    return this.http.get<Tutor[]>(this.apiUrl);
  }

  getById(id: number): Observable<Tutor> {
    return this.http.get<Tutor>(`${this.apiUrl}/${id}`);
  }

  create(dto: CreateTutor): Observable<Tutor> {
    return this.http.post<Tutor>(this.apiUrl, dto);
  }

  update(id: number, dto: UpdateTutor): Observable<Tutor> {
    return this.http.put<Tutor>(`${this.apiUrl}/${id}`, dto);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
