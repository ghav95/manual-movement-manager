import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map, catchError } from 'rxjs/operators'; // Import map here
import { ManualHandlingResponse, ManualHandling } from './manual-handling.model';
import { throwError } from 'rxjs';
import { ProductResponse } from './product.model';

@Injectable({
  providedIn: 'root'
})

export class ManualMovementService {
  private urlMovement = 'http://localhost:49448/api/v1/movement';
  private urlProduct = 'http://localhost:49448/api/v1/products';

  constructor(private http: HttpClient) {}

  getMovements(): Observable<ManualHandling[]> {
    return this.http.get<ManualHandlingResponse>(this.urlMovement).pipe(
      map(response => response.ManualHandlings) // Extract the ManualHandlings array
    );
  }

  getProducts() : Observable<ProductResponse>{
    return this.http.get<ProductResponse>(this.urlProduct); // Assumes the API returns an array of objects
  }

  addMovement(payload: any): Observable<any> {
    return this.http.post(this.urlMovement, payload);
  }

}
