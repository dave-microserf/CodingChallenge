import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { ICountryInfo } from './countryinfo';

@Injectable({
  providedIn: 'root'
})
export class CountryInfoService {
  private readonly  url: string = "https://localhost:44341/PaymentsenseCodingChallenge/countryInfo/";

  constructor(private http: HttpClient) { }  

  getCountryInfo(country: string): Observable<ICountryInfo>
  {
    return this.http.get<ICountryInfo>(`${this.url}${country}`);
  }
}
