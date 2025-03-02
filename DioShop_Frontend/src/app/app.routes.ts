import { Routes } from '@angular/router';
import { ProductDetailComponent } from './user/product-detail/product-detail.component';
import { HomeComponent } from './user/home/home.component';
import { LoginComponent } from './user/login/login.component';
export const routes: Routes = [
    { path: '', component: HomeComponent }, // Khi đường dẫn rỗng, hiển thị app-home
    { path: 'product-detail', component: ProductDetailComponent },
    { path: 'login', component: LoginComponent },
    { path: '**', redirectTo: '' } // Nếu không tìm thấy, redirect về home
  ];
