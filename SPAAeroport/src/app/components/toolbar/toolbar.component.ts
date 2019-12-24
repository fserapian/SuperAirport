import { Component, OnInit } from '@angular/core';
import { WeatherService } from 'src/app/services/weather.service';
import { Weather } from 'src/app/models/Weather';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-toolbar',
  templateUrl: './toolbar.component.html',
  styleUrls: ['./toolbar.component.css']
})
export class ToolbarComponent implements OnInit {
  weather: Weather;
  lang = 'En';

  constructor(private weatherService: WeatherService, public translate: TranslateService) {
    translate.setDefaultLang('Fr');
  }

  ngOnInit() {
    this.weatherService.getWeatherFr().subscribe(response => this.weather = response);
  }

  switchLanguage() {
    if (this.lang === 'En') {
      this.weatherService.getWeather().subscribe(response => this.weather = response);
      this.lang = 'Fr';
      this.translate.use('En');
    } else {
      this.weatherService.getWeatherFr().subscribe(response => this.weather = response);
      this.lang = 'En';
      this.translate.use('Fr');
    }
  }

  // Takes Fahrenheit
  toCelsius(f: number): string {
    const c = ((f - 32) * 5 / 9).toFixed();
    return c === '-0' ? '0' : c;
  }
}
