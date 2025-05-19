export interface JwtClaims {
    sub?: string;
    name?: string;
    email?: string;
    exp?: number;
    [key:string]: any;
}
export function getClaimsFromJwt(token: string): JwtClaims {
    const base64Url = token.split('.')[1];
    const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
    const jsonPayload = decodeURIComponent(
        atob(base64)
            .split('')
            .map((c) => '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2))
            .join('')
    );

    const decoded: JwtClaims = JSON.parse(jsonPayload);
    return decoded;
}