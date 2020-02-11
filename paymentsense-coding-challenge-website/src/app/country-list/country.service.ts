import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ICountry } from './country';

@Injectable({
  providedIn: 'root'
})
export class CountryService {

  private readonly  url: string = "https://localhost:44341/PaymentsenseCodingChallenge/countries";

  constructor(private http: HttpClient) { }  

  getCountries(): Observable<ICountry[]>
  {
    return this.http.get<ICountry[]>(this.url);
  }
}
