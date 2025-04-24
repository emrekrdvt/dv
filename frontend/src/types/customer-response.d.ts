export interface CustomerResponse {
    success: boolean;
    message: string;
    data: CustomerDto[]
  }
  
  export interface CustomerDto {
    id: number;
    name: string;

  }