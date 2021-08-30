import { Component, OnInit } from '@angular/core';
import { WeatherForecast } from '../Models/WeatherForecast';
import { WeatherForecastService } from '../Services/weatherforecast.service';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent implements OnInit {
  public forecasts: WeatherForecast[];

  constructor(private weatherForecastService: WeatherForecastService) {

  }
  ngOnInit(): void {
    this.weatherForecastService.getAll()
      .subscribe(
        response => {
          this.forecasts = response as WeatherForecast[];
        },
        error => {
          console.log(error);
        }
      )
  }
}
