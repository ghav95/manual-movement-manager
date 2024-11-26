import { Component, Inject, OnInit, ViewEncapsulation } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { ManualMovementService } from '../../services/manual-movement.service';
import { MatIconModule } from '@angular/material/icon';
import { MatSnackBar } from '@angular/material/snack-bar'; // Import MatSnackBar for toasts
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { catchError, retry } from 'rxjs/operators';
import { throwError } from 'rxjs';

@Component({
  selector: 'app-grid',
  standalone: true,
  encapsulation: ViewEncapsulation.None, // Disable encapsulation
  imports: [
    CommonModule,
    MatTableModule,
    MatIconModule,
    MatSnackBarModule
  ],
  templateUrl: './grid.component.html',
  styleUrls: ['./grid.component.css']
})

export class GridComponent implements OnInit {
  displayedColumns: string[] = [
    'mes',
    'ano',
    'produtoCodigo',
    'produtoDescricao',
    'nrLancamento',
    'descricao',
    'valor'];

  data: any[] = [];


  constructor(
    private service: ManualMovementService,
    private snackBar: MatSnackBar) {}

  ngOnInit(): void {
    this.fetchDataWithRetry();
  }

  fetchDataWithRetry(): void {
    this.service.getMovements().pipe(
      retry(3), // Retry the API call up to 3 times
      catchError((error) => {
        console.error('Error fetching data after retries:', error);
        return throwError(() => error); // Pass the error to the subscriber
      })
    ).subscribe(
      (response) => {
        // Map the response to your table structure
        this.data = response.map(item => ({
          mes: item.Month,
          ano: item.Year,
          produtoCodigo: item.ProductId,
          descricao: item.Description,
          nrLancamento: item.LaunchNumber,
          valor: parseFloat(item.Value) // Convert Value to a number
        }))
        .sort((a, b) => {
          // Convert strings to numbers for sorting
          const mesA = parseInt(a.mes, 10);
          const mesB = parseInt(b.mes, 10);
          const anoA = parseInt(a.ano, 10);
          const anoB = parseInt(b.ano, 10);
          const nrLancamentoA = parseInt(a.nrLancamento, 10);
          const nrLancamentoB = parseInt(b.nrLancamento, 10);

          // Sort by year (ascending)
          if (anoA !== anoB) {
            return anoA - anoB;
          }

          // Sort by month (ascending)
          if (mesA !== mesB) {
            return mesA - mesB;
          }

          // Sort by launch number (ascending)
          return nrLancamentoA - nrLancamentoB;
        });
        // Show success toast
        this.snackBar.open('Data loaded successfully!', 'Close', {
          duration: 3000,
          panelClass: ['success-snackbar'],
          horizontalPosition: 'end',
          verticalPosition: 'top'
        });
      },
      (error) => {
        this.snackBar.open('Failed to load data after 3 attempts.', 'Close', {
          duration: 3000,
          panelClass: ['error-snackbar'],
          horizontalPosition: 'end',
          verticalPosition: 'top'
        });
      }
    );
  }

    refreshGrid(): void {
      this.fetchDataWithRetry(); // Re-fetch the grid data
    }

    isLastColumn(column: string): boolean {
      return this.displayedColumns.indexOf(column) === this.displayedColumns.length - 1;
    }

}
