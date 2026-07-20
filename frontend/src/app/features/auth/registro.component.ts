import { Component, inject, signal } from '@angular/core';
import { AbstractControl, ValidationErrors } from '@angular/forms';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSnackBar } from '@angular/material/snack-bar';
import { UsuarioService } from '../../core/services/usuario.service';
import { ErrorResponse } from '../../core/models/error.model';

@Component({
  selector: 'app-registro',
  imports: [
    ReactiveFormsModule,
    RouterLink,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatProgressSpinnerModule,
  ],
  templateUrl: './registro.component.html',
  styleUrl: './registro.component.scss',
})
export class RegistroComponent {
  private readonly fb = inject(FormBuilder);
  private readonly usuarioService = inject(UsuarioService);
  private readonly router = inject(Router);
  private readonly snackBar = inject(MatSnackBar);

  readonly loading = signal(false);
  readonly error = signal<string | null>(null);

  readonly form = this.fb.nonNullable.group(
    {
      login: ['', Validators.required],
      senha: ['', [Validators.required, Validators.minLength(5)]],
      confirmarSenha: ['', Validators.required],
    },
    { validators: this.senhasIguais },
  );

  submit(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.loading.set(true);
    this.error.set(null);

    const { login, senha } = this.form.getRawValue();
    this.usuarioService.create({ login, senha }).subscribe({
      next: () => {
        this.snackBar.open('Gestor criado. Faça login.', 'OK', { duration: 3500 });
        this.router.navigate(['/login']);
      },
      error: (err: ErrorResponse) => this.error.set(err.mensagem ?? 'Erro ao criar gestor.'),
      complete: () => this.loading.set(false),
    });
  }

  private senhasIguais(group: AbstractControl): ValidationErrors | null {
    const senha = group.get('senha')?.value;
    const confirmarSenha = group.get('confirmarSenha')?.value;
    const senhaInvalida = !!senha && !!confirmarSenha && senha !== confirmarSenha;
    return senhaInvalida ? { senhasDiferentes: true } : null;
  }
}
