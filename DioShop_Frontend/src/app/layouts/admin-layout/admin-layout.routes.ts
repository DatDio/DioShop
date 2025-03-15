import { Routes } from '@angular/router';
import { RouterOutlet } from '@angular/router';
import { AdminhomeComponent } from 'src/app/admin/adminhome/adminhome.component';
import { ProductDashboardComponent } from 'src/app/admin/product/product-dashboard/product-dashboard.component';
import { AddProductComponent } from 'src/app/admin/product/add-product/add-product.component';

export const ADMIN_LAYOUT_ROUTES: Routes = [
   { path: '', loadComponent: () => import('src/app/admin/adminhome/adminhome.component').then(m => m.AdminhomeComponent), pathMatch: 'full' },
   { path: 'products', loadComponent: () => import('src/app/admin/product/product-dashboard/product-dashboard.component').then(m => m.ProductDashboardComponent), pathMatch: 'full' },
   { path: 'add-product', loadComponent: () => import('src/app/admin/product/add-product/add-product.component').then(m => m.AddProductComponent), pathMatch: 'full' }
];
