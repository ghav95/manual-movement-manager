export interface ProductCosif {
  ProductId: string;
  CosifId: string;
  ClassificationId: string;
  Status: string;
}

export interface Product {
  Id: string;
  Description: string;
  Status: string;
  ProductCosifs: ProductCosif[];
}

export interface ProductResponse {
  Products: Product[];
}
