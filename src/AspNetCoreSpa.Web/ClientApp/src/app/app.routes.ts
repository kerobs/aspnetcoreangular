import { Routes } from '@angular/router';
import { HomeComponent } from '@app/home/home.component';

export const routes: Routes = [
  { path: '', component: HomeComponent, pathMatch: 'full', data: { state: 'home' } }
];
