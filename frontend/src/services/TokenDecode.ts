export interface TokenPayload {
    username: string;
    isAdmin: string;
    userid: number;
  }
  
  export const decodeToken = (): TokenPayload | null => {
    const token = localStorage.getItem("token");
    if (!token) {
      return null; 
    }
  
    try {
      const base64Url = token.split(".")[1]; 
      const base64 = base64Url.replace(/-/g, "+").replace(/_/g, "/");
      const jsonPayload = decodeURIComponent(
        atob(base64)
          .split("")
          .map((c) => `%${("00" + c.charCodeAt(0).toString(16)).slice(-2)}`)
          .join("")
      );
  
      const payload: TokenPayload = JSON.parse(jsonPayload);
      return payload;  
    } catch (error) {
      console.error("Invalid token:", error);
      return null;
    }
  };