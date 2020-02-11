import { Component, OnInit } from '@angular/core';
import { CountryService } from './country.service';
import { ICountry } from './country';

@Component({
  selector: 'app-country-list',
  templateUrl: './country-list.component.html',
  styleUrls: ['./country-list.component.scss']
})
export class CountryListComponent implements OnInit {

  public countries: ICountry[] = [];
  public pageOfCountries: Array<any>;

  constructor(private service: CountryService ) { }

  ngOnInit() {    
    this.service.getCountries().subscribe({
      next: countries => this.countries = countries,    
      error: err => console.error(err)
    });
  }

  onChangePage(pageOfCountries: Array<any>)
  {
    this.pageOfCountries = pageOfCountries;
  }
}