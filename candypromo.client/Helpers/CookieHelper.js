export function GetCookie(name) {
    let cookie = document.cookie.split(';').find(c => c.includes(name));
    return cookie ? cookie.split('=')[1] : null;
}