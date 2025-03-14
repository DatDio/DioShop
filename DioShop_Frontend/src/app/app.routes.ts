import { Routes } from '@angular/router';
import { AdminLayoutComponent } from './layouts/admin-layout/admin-layout.component';
import { UserLayoutComponent } from './layouts/user-layout/user-layout.component';

export const routes: Routes = [
  {
    path: '',
    component: UserLayoutComponent,
    loadChildren: () => import('./layouts/user-layout/user-layout.routes').then(m => m.USER_LAYOUT_ROUTES)
  },
  {
    path: 'admin',
    component: AdminLayoutComponent,
    loadChildren: () => import('./layouts/admin-layout/admin-layout.routes').then(m => m.ADMIN_LAYOUT_ROUTES)
  },
  { path: '**', redirectTo: '' }
];
