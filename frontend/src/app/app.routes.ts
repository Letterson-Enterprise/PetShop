import { Routes } from '@angular/router';
import { authGuard } from './core/guards/auth.guard';
import { LoginComponent } from './features/auth/login.component';
import { RegistroComponent } from './features/auth/registro.component';
import { LayoutComponent } from './shared/layout/layout.component';

export const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'registro', component: RegistroComponent },
  {
    path: '',
    component: LayoutComponent,
    canActivate: [authGuard],
    children: [
      { path: '', redirectTo: 'animais', pathMatch: 'full' },
      {
        path: 'animais',
        loadComponent: () =>
          import('./features/animais/animais-list.component').then(
            (m) => m.AnimaisListComponent,
          ),
      },
      {
        path: 'animais/novo',
        loadComponent: () =>
          import('./features/animais/animal-form.component').then(
            (m) => m.AnimalFormComponent,
          ),
      },
      {
        path: 'animais/:id/editar',
        loadComponent: () =>
          import('./features/animais/animal-form.component').then(
            (m) => m.AnimalFormComponent,
          ),
      },
      {
        path: 'tutores',
        loadComponent: () =>
          import('./features/tutores/tutores-list.component').then(
            (m) => m.TutoresListComponent,
          ),
      },
      {
        path: 'tutores/novo',
        loadComponent: () =>
          import('./features/tutores/tutor-form.component').then(
            (m) => m.TutorFormComponent,
          ),
      },
      {
        path: 'tutores/:id/editar',
        loadComponent: () =>
          import('./features/tutores/tutor-form.component').then(
            (m) => m.TutorFormComponent,
          ),
      },
    ],
  },
  { path: '**', redirectTo: '' },
];
