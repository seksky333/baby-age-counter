const DEV_ENV = "DEVELOPMENT";

export function isDevelopment(): boolean {
    const env = import.meta.env.VITE_ENVIRONMENT;
    return env == DEV_ENV;
}

export const serverAddress ="http://localhost:9090";