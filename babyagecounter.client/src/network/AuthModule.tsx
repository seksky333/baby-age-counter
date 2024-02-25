import { isDevelopment, getServerAddress } from "../utilities/EnvManager";

const SERVER = getServerAddress();

export interface IdentityModel {
    token: string,
    refreshToken: string
}

export async function get() {
    
    const url = isDevelopment() ? `${getServerAddress()}/baby` : "/baby";
    const response = await fetch(url);
    return await response.json();
}