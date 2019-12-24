export interface Weather {
    latitude: number;
    logitude: number;
    timezone?: string;
    summary?: string;
    icon?: string;
    temperature: number;
}