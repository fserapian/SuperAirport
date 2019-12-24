import { Component, OnInit, ViewChild } from '@angular/core';
import { FlightService } from 'src/app/services/flight.service';
import { MatDialog, MatTableDataSource, MatSort } from '@angular/material';
import { FollowComponent } from '../follow/follow.component';
import { Arrival } from 'src/app/models/Arrival';
import { Departure } from 'src/app/models/Departure';

// Arrivals column definitions
const columnDefinitions1 = [
  { def: 'volCeduleId', showCol: false },
  { def: 'numVol', showCol: true },
  { def: 'heurePrevue', showCol: true },
  { def: 'datePrevue', showCol: false },
  { def: 'heureRevisee', showCol: true },
  { def: 'compLogo', showCol: true },
  { def: 'provenance', showCol: true },
  { def: 'statut', showCol: true },
  { def: 'suivre', showCol: true },
];

// Departures column definitions
const columnDefinitions2 = [
  { def: 'volCeduleId', showCol: false },
  { def: 'numVol', showCol: true },
  { def: 'heurePrevue', showCol: true },
  { def: 'datePrevue', showCol: false },
  { def: 'heureRevisee', showCol: true },
  { def: 'compLogo', showCol: true },
  { def: 'destination', showCol: true },
  { def: 'statut', showCol: true },
  { def: 'porte', showCol: true },
  { def: 'suivre', showCol: true },
];

@Component({
  selector: 'app-flight-table',
  templateUrl: './flight-table.component.html',
  styleUrls: ['./flight-table.component.css']
})
export class FlightTableComponent implements OnInit {
  // Open Sidenav
  opened = true;

  // Show arrivals by default
  arrivalsShow = true;

  public arrivalsTableData: MatTableDataSource<Arrival>;
  public departuresTableData: MatTableDataSource<Departure>;

  @ViewChild('arrivalsTableSort', { static: true }) public arrivalsTableSort: MatSort;
  @ViewChild('departuresTableSort', { static: true }) public departuresTableSort: MatSort;

  constructor(private flightService: FlightService, public dialog: MatDialog) { }

  ngOnInit() {
    this.flightService.getArrivals().subscribe(arrivals => {
      this.arrivalsTableData = new MatTableDataSource(arrivals);
      this.arrivalsTableData.sort = this.arrivalsTableSort;
    });

    this.flightService.getDepartures().subscribe(departures => {
      this.departuresTableData = new MatTableDataSource(departures);
      this.departuresTableData.sort = this.departuresTableSort;
    });
  }

  onClickArrivals() {
    this.arrivalsShow = true;
  }

  onClickDepartures() {
    this.arrivalsShow = false;
  }

  // Get arrivals column definitions
  getDisplayedColumns1(): string[] {
    return columnDefinitions1
      .filter(x => x.showCol)
      .map(x => x.def);
  }

  // Get departures column definitions
  getDisplayedColumns2(): string[] {
    return columnDefinitions2
      .filter(x => x.showCol)
      .map(x => x.def);
  }

  openDialog(flight: any) {
    let dialogRef = this.dialog.open(FollowComponent, {
      data: {
        numVol: flight.numVol,
        volCeduleId: flight.volCeduleId
      }
    });
  }

  // Filter table
  applyFilter(filterValue: string) {
    this.arrivalsTableData.filter = filterValue.trim().toLowerCase();
    this.departuresTableData.filter = filterValue.trim().toLowerCase();
  }

  // Custom sort scheduled date in table
  sortByDate(id: string, start?: 'asc' | 'desc') {
    const matSortArrivals = this.arrivalsTableData.sort;
    const matSortDepartures = this.departuresTableData.sort;
    const toState = 'active';
    const disableClear = false;

    matSortArrivals.sort({ id: null, start, disableClear });
    matSortArrivals.sort({ id, start, disableClear });

    matSortDepartures.sort({ id: null, start, disableClear });
    matSortDepartures.sort({ id, start, disableClear });

    this.arrivalsTableData.sort = this.arrivalsTableSort;
    this.departuresTableData.sort = this.departuresTableSort;
  }
}
