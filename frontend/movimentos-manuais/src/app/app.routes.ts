import { Routes } from '@angular/router';
import { FormComponent } from './components/form/form.component';
import { GridComponent } from './components/grid/grid.component';

export const routes: Routes = [
  { path: '', component: FormComponent },
  { path: '*', component: GridComponent },  // Default route
  { path: '**', redirectTo: 'form' }            // Wildcard route for any unmatched routes, redirects to home (FormComponent)
];
