import { Injectable, Inject } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { CommonService } from "../common/common.service";

@Injectable()

export class WeatherForecastService extends CommonService {
  constructor(public http: HttpClient, @Inject('BASE_URL') public baseUrl: string) {
    super(http, baseUrl, 'weatherforecast')
  }
}
