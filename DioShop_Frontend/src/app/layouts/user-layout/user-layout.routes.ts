import { Routes } from '@angular/router';
import { HomeComponent } from "src/app/user/home/home.component";
import { LoginComponent } from 'src/app/user/login/login.component';
import { ProductListComponent } from 'src/app/user/product-list/product-list.component';
export const USER_LAYOUT_ROUTES: Routes = [
    { path: '', component: HomeComponent },
    // { path: 'product', component: ProductComponent },
     { path: 'login', component: LoginComponent },
     { path: 'product-list', component: ProductListComponent }
  ];