import { Routes } from '@angular/router';
import { ProductDetailComponent } from './user/product-detail/product-detail.component';
import { HomeComponent } from './user/home/home.component';
import { LoginComponent } from './user/login/login.component';
import { CartComponent} from './user/cart/cart.component';
import { CheckoutComponent} from './user/checkout/checkout.component';
export const routes: Routes = [
    { path: '', component: HomeComponent }, // Khi đường dẫn rỗng, hiển thị app-home
    { path: 'product-detail', component: ProductDetailComponent },
    { path: 'login', component: LoginComponent },
    { path: 'cart', component: CartComponent },
    { path: 'checkout', component: CheckoutComponent },
    { path: '**', redirectTo: '' } // Nếu không tìm thấy, redirect về home
  ];
