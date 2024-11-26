export interface ManualHandling {
  Month: string;
  Year: string;
  LaunchNumber: string;
  ProductId: string;
  CosifId: string;
  Description: string;
  Date: string;
  UserId: string;
  Value: string;
}

export interface ManualHandlingResponse {
  ManualHandlings: ManualHandling[];
}
