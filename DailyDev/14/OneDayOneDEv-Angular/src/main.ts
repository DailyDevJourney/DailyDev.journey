
import { provideHttpClient } from '@angular/common/http';

import { bootstrapApplication, platformBrowser } from '@angular/platform-browser';
import { LoginComponent } from './app/login/login.component';
import { provideRouter } from '@angular/router';
import { routes } from './app/app-routing.module';
import { AppComponent } from './app/app.component';
import { AppSettings } from "../public/AppSettings";


export function InitialiseApp(AppSettings: AppSettings) {
  return () => AppSettings.loadConfig();
}
bootstrapApplication(AppComponent, {
  providers : [provideHttpClient(),provideRouter(routes)]
})
  .catch(err => console.error(err));
