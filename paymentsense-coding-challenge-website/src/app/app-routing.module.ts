import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CountryListComponent } from './country-list/country-list.component';
import { CountryInfoComponent } from './country-info/country-info.component';

const routes: Routes = [
  { path: 'countries', component: CountryListComponent },
  { path: 'countryinfo/:country', component: CountryInfoComponent },
  { path: '', redirectTo: 'countries', pathMatch: 'full' }  
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
