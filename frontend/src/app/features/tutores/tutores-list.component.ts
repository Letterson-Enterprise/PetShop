import { Component, OnInit, inject, signal } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatDialog } from '@angular/material/dialog';
import { TutorService } from '../../core/services/tutor.service';
import { Tutor } from '../../core/models/tutor.model';
import { ErrorResponse } from '../../core/models/error.model';
import { ConfirmDialogComponent } from '../../shared/confirm-dialog.component';

@Component({
  selector: 'app-tutores-list',
  imports: [
    RouterLink,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatProgressSpinnerModule,
  ],
  templateUrl: './tutores-list.component.html',
  styleUrl: './tutores-list.component.scss',
})
export class TutoresListComponent implements OnInit {
  private readonly tutorService = inject(TutorService);
  private readonly router = inject(Router);
  private readonly snackBar = inject(MatSnackBar);
  private readonly dialog = inject(MatDialog);

  readonly tutores = signal<Tutor[]>([]);
  readonly loading = signal(true);
  readonly error = signal<string | null>(null);

  readonly displayedColumns = ['nome', 'cidade', 'uf', 'animais', 'acoes'];

  ngOnInit(): void {
    this.load();
  }

  load(): void {
    this.loading.set(true);
    this.error.set(null);
    this.tutorService.getAll().subscribe({
      next: (tutores) => this.tutores.set(tutores),
      error: (err: ErrorResponse) => this.error.set(err.mensagem ?? 'Erro ao carregar tutores.'),
      complete: () => this.loading.set(false),
    });
  }

  excluir(tutor: Tutor): void {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      data: { titulo: 'Excluir tutor', mensagem: `Deseja excluir "${tutor.nome}"?` },
    });

    dialogRef.afterClosed().subscribe((confirmado) => {
      if (!confirmado) return;
      this.tutorService.delete(tutor.id).subscribe({
        next: () => {
          this.snackBar.open('Tutor excluído.', 'OK', { duration: 3000 });
          this.load();
        },
        error: (err: ErrorResponse) =>
          this.snackBar.open(err.mensagem ?? 'Erro ao excluir.', 'OK', { duration: 4000 }),
      });
    });
  }

  novo(): void {
    this.router.navigate(['/tutores/novo']);
  }
}
