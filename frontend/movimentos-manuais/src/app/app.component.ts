import { Component } from '@angular/core';
import { FormComponent } from "./components/form/form.component";
import { GridComponent } from "./components/grid/grid.component";


@Component({
  selector: 'app-root',
  standalone: true,
  imports: [FormComponent, GridComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'movimentos-manuais';
}
