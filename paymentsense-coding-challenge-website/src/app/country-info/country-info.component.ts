import { Component, OnInit } from '@angular/core';
import { CountryInfoService } from './countryinfo.service';
import { ICountryInfo } from './countryinfo';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-country-info',
  templateUrl: './country-info.component.html',
  styleUrls: ['./country-info.component.scss']
})
export class CountryInfoComponent implements OnInit 
{
  public info: ICountryInfo;

  constructor(private route: ActivatedRoute, private service: CountryInfoService) { }

  ngOnInit() {
    let country = this.route.snapshot.paramMap.get("country");
    this.service.getCountryInfo(country).subscribe({
      next: countryInfo => this.info = countryInfo,
      error: err => console.error(err)
    });
  }
}
