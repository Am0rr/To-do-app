import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: 'login',
    loadComponent: () =>
      import('./features/auth/login/login.component').then((m) => m.LoginComponent),
  },

  {
    path: 'register',
    loadComponent: () =>
      import('./features/auth/registration/register.component').then((m) => m.RegisterComponent),
  },

  {
    path: 'tasks',
    loadComponent: () => import('./features/tasks/tasks.component').then((m) => m.TasksComponent),
  },
];
