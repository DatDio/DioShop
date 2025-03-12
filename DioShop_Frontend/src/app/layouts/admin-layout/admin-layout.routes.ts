import { Routes } from '@angular/router';
import { RouterOutlet } from '@angular/router';
import { HomeComponent } from 'src/app/admin/home/home.component';
import { ProductDashboardComponent } from 'src/app/admin/product/product-dashboard/product-dashboard.component';
import { AddProductComponent } from 'src/app/admin/product/add-product/add-product.component';

export const ADMIN_LAYOUT_ROUTES: Routes = [
   { path: '', component: HomeComponent },
   { path: 'products', component: ProductDashboardComponent },
   { path: 'add-product', component: AddProductComponent }
   //   { path: 'products', component: ProductManagerComponent }
];