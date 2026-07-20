import { Component, OnInit, inject, signal } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MAT_DATE_LOCALE, MatNativeDateModule } from '@angular/material/core';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatIconModule } from '@angular/material/icon';
import { MatSnackBar } from '@angular/material/snack-bar';
import { TutorService } from '../../core/services/tutor.service';
import { CepService } from '../../core/services/cep.service';
import { ErrorResponse } from '../../core/models/error.model';

@Component({
  selector: 'app-tutor-form',
  imports: [
    ReactiveFormsModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatProgressSpinnerModule,
    MatIconModule,
  ],
  providers: [{ provide: MAT_DATE_LOCALE, useValue: 'pt-BR' }],
  templateUrl: './tutor-form.component.html',
  styleUrl: './tutor-form.component.scss',
})
export class TutorFormComponent implements OnInit {
  private readonly fb = inject(FormBuilder);
  private readonly route = inject(ActivatedRoute);
  private readonly router = inject(Router);
  private readonly tutorService = inject(TutorService);
  private readonly cepService = inject(CepService);
  private readonly snackBar = inject(MatSnackBar);

  readonly loading = signal(false);
  readonly saving = signal(false);
  readonly error = signal<string | null>(null);
  readonly cepErro = signal<string | null>(null);
  readonly cepConsultando = signal(false);

  private readonly idSignal = signal<number | null>(null);
  readonly id = this.idSignal;

  readonly form = this.fb.nonNullable.group({
    nome: ['', Validators.required],
    dataNascimento: [new Date(), Validators.required],
    cep: ['', [Validators.required, Validators.pattern(/^\d{5}-?\d{3}$/)]],
    numeroCasa: [0, [Validators.required, Validators.min(1)]],
    logradouro: [{ value: '', disabled: true }],
    bairro: [{ value: '', disabled: true }],
    cidade: [{ value: '', disabled: true }],
    uf: [{ value: '', disabled: true }],
  });

  ngOnInit(): void {
    const param = this.route.snapshot.paramMap.get('id');
    if (param) {
      this.idSignal.set(Number(param));
      this.loading.set(true);
      this.tutorService.getById(this.idSignal()!).subscribe({
        next: (tutor) => {
          this.form.patchValue({
            nome: tutor.nome,
            dataNascimento: new Date(tutor.dataNascimento),
            cep: tutor.endereco.cep,
            numeroCasa: tutor.endereco.numeroCasa,
            logradouro: tutor.endereco.logradouro,
            bairro: tutor.endereco.bairro,
            cidade: tutor.endereco.cidade,
            uf: tutor.endereco.uf,
          });
        },
        error: (err: ErrorResponse) => this.error.set(err.mensagem ?? 'Tutor não encontrado.'),
        complete: () => this.loading.set(false),
      });
    }
  }

  consultarCep(): void {
    if (this.form.controls.cep.invalid) {
      return;
    }
    this.cepConsultando.set(true);
    this.cepErro.set(null);

    this.cepService.consultar(this.form.controls.cep.getRawValue()).subscribe({
      next: (res) => {
        if (res.erro) {
          this.cepErro.set('CEP não encontrado.');
          this.limparEndereco();
          return;
        }
        this.form.patchValue({
          logradouro: res.logradouro,
          bairro: res.bairro,
          cidade: res.localidade,
          uf: res.uf,
        });
      },
      error: () => {
        this.cepErro.set('Falha ao consultar o CEP.');
        this.limparEndereco();
      },
      complete: () => this.cepConsultando.set(false),
    });
  }

  submit(): void {
    if (this.form.controls.nome.invalid || this.form.controls.cep.invalid || this.form.controls.numeroCasa.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.saving.set(true);
    this.error.set(null);

    const value = this.form.getRawValue();
    const dto = {
      nome: value.nome,
      dataNascimento: this.toIso(value.dataNascimento),
      cep: value.cep,
      numeroCasa: value.numeroCasa,
    };

    const request$ = this.idSignal()
      ? this.tutorService.update(this.idSignal()!, dto)
      : this.tutorService.create(dto);

    request$.subscribe({
      next: () => {
        this.snackBar.open('Tutor salvo.', 'OK', { duration: 3000 });
        this.router.navigate(['/tutores']);
      },
      error: (err: ErrorResponse) => this.error.set(err.mensagem ?? 'Erro ao salvar tutor.'),
      complete: () => this.saving.set(false),
    });
  }

  cancelar(): void {
    this.router.navigate(['/tutores']);
  }

  private limparEndereco(): void {
    this.form.patchValue({ logradouro: '', bairro: '', cidade: '', uf: '' });
  }

  private toIso(date: Date): string {
    return new Date(date.getTime() - date.getTimezoneOffset() * 60000).toISOString();
  }
}
