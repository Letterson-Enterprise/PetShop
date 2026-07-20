import { Component, OnInit, inject, signal } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MAT_DATE_LOCALE, MatNativeDateModule } from '@angular/material/core';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AnimalService } from '../../core/services/animal.service';
import { TutorService } from '../../core/services/tutor.service';
import { Tutor } from '../../core/models/tutor.model';
import { ErrorResponse } from '../../core/models/error.model';

@Component({
  selector: 'app-animal-form',
  imports: [
    ReactiveFormsModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatButtonModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatProgressSpinnerModule,
  ],
  providers: [{ provide: MAT_DATE_LOCALE, useValue: 'pt-BR' }],
  templateUrl: './animal-form.component.html',
  styleUrl: './animal-form.component.scss',
})
export class AnimalFormComponent implements OnInit {
  private readonly fb = inject(FormBuilder);
  private readonly route = inject(ActivatedRoute);
  private readonly router = inject(Router);
  private readonly animalService = inject(AnimalService);
  private readonly tutorService = inject(TutorService);
  private readonly snackBar = inject(MatSnackBar);

  readonly tutores = signal<Tutor[]>([]);
  readonly loading = signal(false);
  readonly saving = signal(false);
  readonly error = signal<string | null>(null);

  private readonly idSignal = signal<number | null>(null);
  readonly id = this.idSignal;

  readonly form = this.fb.nonNullable.group({
    nome: ['', Validators.required],
    especie: ['', Validators.required],
    peso: [0, [Validators.required, Validators.min(0.01)]],
    dataNascimento: [new Date(), Validators.required],
    tutorId: [0, [Validators.required, Validators.min(1)]],
  });

  ngOnInit(): void {
    this.tutorService.getAll().subscribe({
      next: (tutores) => this.tutores.set(tutores),
      error: (err: ErrorResponse) => this.error.set(err.mensagem ?? 'Erro ao carregar tutores.'),
    });

    const param = this.route.snapshot.paramMap.get('id');
    if (param) {
      this.idSignal.set(Number(param));
      this.loading.set(true);
      this.animalService.getById(this.id()!).subscribe({
        next: (animal) => {
          this.form.patchValue({
            nome: animal.nome,
            especie: animal.especie,
            peso: animal.peso,
            dataNascimento: new Date(animal.dataNascimento),
            tutorId: animal.tutorId,
          });
        },
        error: (err: ErrorResponse) => this.error.set(err.mensagem ?? 'Animal não encontrado.'),
        complete: () => this.loading.set(false),
      });
    }
  }

  submit(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.saving.set(true);
    this.error.set(null);

    const value = this.form.getRawValue();
    const dto = {
      nome: value.nome,
      especie: value.especie,
      peso: value.peso,
      dataNascimento: this.toIso(value.dataNascimento),
      tutorId: value.tutorId,
    };

    const request$ = this.idSignal()
      ? this.animalService.update(this.idSignal()!, dto)
      : this.animalService.create(dto);

    request$.subscribe({
      next: () => {
        this.snackBar.open('Animal salvo.', 'OK', { duration: 3000 });
        this.router.navigate(['/animais']);
      },
      error: (err: ErrorResponse) => this.error.set(err.mensagem ?? 'Erro ao salvar animal.'),
      complete: () => this.saving.set(false),
    });
  }

  cancelar(): void {
    this.router.navigate(['/animais']);
  }

  private toIso(date: Date): string {
    return new Date(date.getTime() - date.getTimezoneOffset() * 60000).toISOString();
  }
}
