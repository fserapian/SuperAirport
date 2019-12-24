import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Arrival } from '../models/Arrival';
import { Departure } from '../models/Departure';
import { Notification } from '../models/Notification';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
  providedIn: 'root'
})
export class FlightService {

  constructor(private http: HttpClient) { }

  getArrivals(): Observable<Arrival[]> {
    return this.http.get<Arrival[]>(`${environment.baseUrl}/arrivals`);
  }

  getDepartures(): Observable<Departure[]> {
    return this.http.get<Departure[]>(`${environment.baseUrl}/departures`);
  }

  saveNotification(notification: Notification): Observable<Notification> {
    return this.http.post<Notification>(`${environment.baseUrl}/notifications`, notification, httpOptions);
  }
}
