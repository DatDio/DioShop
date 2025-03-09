import { Routes } from '@angular/router';
import { RouterOutlet } from '@angular/router';
import { HomeComponent } from 'src/app/admin/home/home.component';
// import { DashboardComponent } from '../../features/admin/dashboard/dashboard.component';
// import { ProductManagerComponent } from '../../features/admin/product-manager/product-manager.component';

export const ADMIN_LAYOUT_ROUTES: Routes = [
   { path: '', component: HomeComponent },
//   { path: 'products', component: ProductManagerComponent }
];