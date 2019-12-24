import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Weather } from '../models/Weather';

@Injectable({
  providedIn: 'root'
})
export class WeatherService {
  proxy = 'https://cors-anywhere.herokuapp.com';
  key = 'YOUR_DARKSKY_API_KEY';
  baseUrl = 'https://api.darksky.net/forecast';

  // Montreal coordinates
  lat = '45.5017';
  long = '-73.5673';

  constructor(private http: HttpClient) { }

  getWeather(): Observable<Weather> {
    return this.http.get<Weather>(`${this.proxy}/${this.baseUrl}/${this.key}/${this.lat},${this.long}`);
  }

  getWeatherFr(): Observable<Weather> {
    return this.http.get<Weather>(`${this.proxy}/${this.baseUrl}/${this.key}/${this.lat},${this.long}?lang=fr`);
  }
}
