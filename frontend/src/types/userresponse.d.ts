export interface UserResponse {
    success: boolean;
    message: string;
    data: UserDto[]
  }
  
  export interface UserDto {
    id: number;
    username: string;
   email: string;
   isAdmin: boolean;
  }