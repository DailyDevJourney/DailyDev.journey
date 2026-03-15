
import { provideHttpClient } from '@angular/common/http';

import { bootstrapApplication, platformBrowser } from '@angular/platform-browser';
import { LoginComponent } from './app/login/login.component';
import { provideRouter } from '@angular/router';
import { routes } from './app/app-routing.module';


bootstrapApplication(LoginComponent, {
  providers : [provideHttpClient(),provideRouter(routes)]
})
  .catch(err => console.error(err));
