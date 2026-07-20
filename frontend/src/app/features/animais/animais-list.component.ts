import { Component, OnInit, inject, signal } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { DatePipe, DecimalPipe } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatDialog } from '@angular/material/dialog';
import { AnimalService } from '../../core/services/animal.service';
import { TutorService } from '../../core/services/tutor.service';
import { Animal } from '../../core/models/animal.model';
import { Tutor } from '../../core/models/tutor.model';
import { ErrorResponse } from '../../core/models/error.model';
import { ConfirmDialogComponent } from '../../shared/confirm-dialog.component';

@Component({
  selector: 'app-animais-list',
  imports: [
    RouterLink,
    DatePipe,
    DecimalPipe,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatProgressSpinnerModule,
  ],
  templateUrl: './animais-list.component.html',
  styleUrl: './animais-list.component.scss',
})
export class AnimaisListComponent implements OnInit {
  private readonly animalService = inject(AnimalService);
  private readonly tutorService = inject(TutorService);
  private readonly router = inject(Router);
  private readonly snackBar = inject(MatSnackBar);
  private readonly dialog = inject(MatDialog);

  readonly animais = signal<Animal[]>([]);
  readonly tutores = signal<Map<number, Tutor>>(new Map());
  readonly loading = signal(true);
  readonly error = signal<string | null>(null);

  readonly displayedColumns = ['nome', 'especie', 'peso', 'dataNascimento', 'tutor', 'acoes'];

  ngOnInit(): void {
    this.load();
  }

  load(): void {
    this.loading.set(true);
    this.error.set(null);
    this.tutorService.getAll().subscribe({
      next: (tutores) =>
        this.tutores.set(new Map(tutores.map((t) => [t.id, t]))),
      error: () => this.tutores.set(new Map()),
    });
    this.animalService.getAll().subscribe({
      next: (animais) => this.animais.set(animais),
      error: (err: ErrorResponse) => this.error.set(err.mensagem ?? 'Erro ao carregar animais.'),
      complete: () => this.loading.set(false),
    });
  }

  tutorNome(tutorId: number): string {
    return this.tutores().get(tutorId)?.nome ?? '—';
  }

  excluir(animal: Animal): void {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      data: { titulo: 'Excluir animal', mensagem: `Deseja excluir "${animal.nome}"?` },
    });

    dialogRef.afterClosed().subscribe((confirmado) => {
      if (!confirmado) return;
      this.animalService.delete(animal.id).subscribe({
        next: () => {
          this.snackBar.open('Animal excluído.', 'OK', { duration: 3000 });
          this.load();
        },
        error: (err: ErrorResponse) =>
          this.snackBar.open(err.mensagem ?? 'Erro ao excluir.', 'OK', { duration: 4000 }),
      });
    });
  }

  novo(): void {
    this.router.navigate(['/animais/novo']);
  }
}
