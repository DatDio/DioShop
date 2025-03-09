import { Component, Renderer2, Inject, PLATFORM_ID, AfterViewInit } from '@angular/core';
import { isPlatformBrowser } from '@angular/common';
import { RouterOutlet } from '@angular/router';
@Component({
  selector: 'app-admin-layout',
  imports: [RouterOutlet],
  standalone: true,
  templateUrl: './admin-layout.component.html',
  styleUrls: ['./admin-layout.component.css']
})
export class AdminLayoutComponent implements AfterViewInit {
  constructor(
    private renderer: Renderer2,
    @Inject(PLATFORM_ID) private platformId: any
  ) {}

  ngAfterViewInit() {
    if (isPlatformBrowser(this.platformId)) {
      this.loadStyles();
      this.loadScripts();
    }
  }

  private loadStyles() {
    const styles = [
      'assets/admin/assets/vendors/feather/feather.css',
      'assets/admin/assets/vendors/mdi/css/materialdesignicons.min.css',
      'assets/admin/assets/vendors/ti-icons/css/themify-icons.css',
      'assets/admin/assets/vendors/font-awesome/css/font-awesome.min.css',
      'assets/admin/assets/vendors/typicons/typicons.css',
      'assets/admin/assets/vendors/simple-line-icons/css/simple-line-icons.css',
      'assets/admin/assets/vendors/css/vendor.bundle.base.css',
      'assets/admin/assets/vendors/bootstrap-datepicker/bootstrap-datepicker.min.css',
      'assets/admin/assets/vendors/datatables.net-bs4/dataTables.bootstrap4.css',
      'assets/admin/assets/js/select.dataTables.min.css',
      'assets/admin/assets/css/style.css'
    ];
    styles.forEach(href => this.addCss(href));
  }

  private loadScripts() {
    const scripts = [
      'assets/admin/assets/vendors/js/vendor.bundle.base.js',
      'assets/admin/assets/vendors/bootstrap-datepicker/bootstrap-datepicker.min.js',
      'assets/admin/assets/vendors/chart.js/chart.umd.js',
      'assets/admin/assets/vendors/progressbar.js/progressbar.min.js',
      'assets/admin/assets/js/off-canvas.js',
      'assets/admin/assets/js/template.js',
      'assets/admin/assets/js/settings.js',
      'assets/admin/assets/js/hoverable-collapse.js',
      'assets/admin/assets/js/todolist.js',
      'assets/admin/assets/js/jquery.cookie.js',
      'assets/admin/assets/js/dashboard.js'
    ];
    let loadedCount = 0;

    scripts.forEach(src => {
      this.addScript(src, () => {
        loadedCount++;
        if (loadedCount === scripts.length) {
          this.runCustomScript();
        }
      });
    });
  }

  private addCss(href: string) {
    if (isPlatformBrowser(this.platformId)) {
      const link = this.renderer.createElement('link');
      link.rel = 'stylesheet';
      link.href = href;
      this.renderer.appendChild(document.head, link);
    }
  }

  private addScript(src: string, onLoad?: () => void) {
    if (isPlatformBrowser(this.platformId)) {
      const script = this.renderer.createElement('script');
      script.src = src;
      script.async = false;
      script.defer = false;
      if (onLoad) {
        script.onload = onLoad;
      }
      this.renderer.appendChild(document.body, script);
    }
  }

  private runCustomScript() {
    console.log('Admin scripts loaded successfully!');
  }
}
