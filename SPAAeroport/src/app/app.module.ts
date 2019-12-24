// Import Modules
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { SkyconsModule } from 'ngx-skycons';
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import {
  MatButtonModule,
  MatSidenavModule,
  MatGridListModule,
  MatExpansionModule,
  MatCardModule,
  MatTabsModule,
  MatTableModule,
  MatSortModule,
  MatIconModule,
  MatToolbarModule,
  MatInputModule,
  MatDialogModule,
  MatFormFieldModule,
  MatSnackBarModule,
} from '@angular/material';

// Import Services
import { WeatherService } from './services/weather.service';
import { FlightService } from './services/flight.service';

// Import Components
import { ToolbarComponent } from './components/toolbar/toolbar.component';
import { FollowComponent } from './components/follow/follow.component';
import { FlightTableComponent } from './components/flight-table/flight-table.component';
import { FooterComponent } from './components/footer/footer.component';

// Angular Material Modules
const MaterialModules = [
  MatButtonModule,
  MatTableModule,
  MatIconModule,
  MatSidenavModule,
  MatGridListModule,
  MatExpansionModule,
  MatCardModule,
  MatTabsModule,
  MatSortModule,
  MatToolbarModule,
  MatDialogModule,
  MatInputModule,
  MatFormFieldModule,
  MatSnackBarModule
];

// Factory function
export function HttpLoaderFactory(http: HttpClient) {
  return new TranslateHttpLoader(http);
}

@NgModule({
  declarations: [
    AppComponent,
    ToolbarComponent,
    FollowComponent,
    FlightTableComponent,
    FooterComponent,
  ],
  entryComponents: [FollowComponent], // the dialog component
  imports: [
    HttpClientModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    MaterialModules,
    SkyconsModule,
    BrowserModule,
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: HttpLoaderFactory,
        deps: [HttpClient]
      }
    })
  ],
  providers: [
    WeatherService,
    FlightService,
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
