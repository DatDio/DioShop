import { Routes } from '@angular/router';
import { HomeComponent } from "src/app/user/home/home.component";
import { LoginComponent } from 'src/app/user/login/login.component';

export const USER_LAYOUT_ROUTES: Routes = [
    { path: '', component: HomeComponent },
    // { path: 'product', component: ProductComponent },
     { path: 'login', component: LoginComponent }
  ];