import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import {FooterComponent} from './share/footer/footer.component';
import { HeaderComponent } from "./user/header/header.component";

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, FooterComponent, HeaderComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'DioShop_Frontend';
}
