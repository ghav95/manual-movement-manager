import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select'; // For dropdowns (ComboBox)
import { NgxMaskDirective } from 'ngx-mask';
import { ManualMovementService } from '../../services/manual-movement.service';
import { Product, ProductCosif } from '../../services/product.model';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatSnackBarModule } from '@angular/material/snack-bar';

@Component({
  selector: 'app-form',
  standalone: true,
  encapsulation: ViewEncapsulation.None,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    NgxMaskDirective,
    MatSnackBarModule
  ],
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.css']
})
export class FormComponent implements OnInit {
  movForm: FormGroup;
  products: Product[] = []; // Strongly typed products array
  filteredCosifs: ProductCosif[] = []; // Strongly typed cosifs array

  constructor(
    private fb: FormBuilder,
    private service: ManualMovementService,
    private snackBar: MatSnackBar
  ){
    this.movForm = this.fb.group({
      produto: [null], // Product combo box
      cosif: [null] // Cosif combo box
    });
  }

  ngOnInit() {
    this.movForm = this.fb.group({
      mes: ['', [Validators.required, Validators.min(1), Validators.max(12)]],
      ano: ['', [Validators.required, Validators.min(1900), Validators.max(2100)]],
      produto: [null, Validators.required],
      cosif: [null, Validators.required],
      descricao: ['', Validators.required],
      valor: ['', Validators.required]
    });
    this.loadProducts();
  }

  // Fetch products from the API
  loadProducts(): void {
    this.service.getProducts().subscribe(
      (response) => {
        this.products = response.Products; // Access the Products array in the API payload
      },
      (error) => {
        console.error('Error fetching products:', error);
      }
    )};


  onProductChange(productId: string): void {
    const selectedProduct = this.products.find(p => p.Id === productId);
    this.filteredCosifs = selectedProduct?.ProductCosifs || [];
    this.movForm.get('cosif')?.setValue(null); // Clear the cosif selection
  }

  clear() {
    this.movForm.reset();
    this.movForm.disable()
  }

  new() {
    this.movForm.enable();
  }

  get mes() {
    return this.movForm.get('mes');
  }

  get ano() {
    return this.movForm.get('ano');
  }



  send(): void {
    const payload = {
      Month: this.movForm.get('mes')?.value,
      Year: this.movForm.get('ano')?.value,
      ProductId: this.movForm.get('produto')?.value,
      CosifId: this.movForm.get('cosif')?.value,
      Value: this.movForm.get('valor')?.value,
      Description: this.movForm.get('descricao')?.value,
      UserId: 'TESTE' // Replace with the actual user ID
    };

    // Send the payload to the API
    this.service.addMovement(payload).subscribe(
      (response) => {
        this.snackBar.open('Movimento incluído com sucesso!', 'Fechar', {
          duration: 6000,
          panelClass: ['success-snackbar']
        });
        this.movForm.reset(); // Reset the form after successful submission
        this.movForm.disable();

        // Wait 2 seconds, then trigger grid refresh
        setTimeout(() => {
          const gridComponent = document.querySelector('app-grid') as any;
          gridComponent?.refreshGrid(); // Call the grid's refresh method
        }, 2000);


      },
      (error) => {
        this.snackBar.open('Erro ao incluir o movimento.', 'Fechar', {
          duration: 6000,
          panelClass: ['error-snackbar'],
          horizontalPosition: 'end',
          verticalPosition: 'bottom'
        });
        console.error('Error adding movement:', error);
      }
    );
  }

}
