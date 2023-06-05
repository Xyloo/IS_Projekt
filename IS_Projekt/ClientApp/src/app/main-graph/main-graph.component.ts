import { Component } from '@angular/core';
import { DataService } from '../data.service';
import { Chart, registerables } from 'chart.js';
import { DataFilter } from '../data-filter';
import { DataModel } from '../data-model';
import { LineItem } from '../line-item';

@Component({
  selector: 'app-main-graph',
  templateUrl: './main-graph.component.html',
  styleUrls: ['./main-graph.component.css']
})

export class MainGraphComponent {

  public years: number[] = [];
  public countries: string[] = [];
  public IC_ECommerce: string[] = [];
  public IC_InternetUse: string[] = [];
  public dataEC: DataModel[] = [];
  public dataIU: DataModel[] = [];
  public charts = [];

  //user input
  public selectedYears: number[] = [];
  selectedCountries = [];
  selectedIC_EC = [];
  selectedIC_IU = [];

  public chart: any;
  isHidden: boolean = true;

  constructor(private dataService: DataService) { }

  getRandomRGB() {
    const r = Math.floor(Math.random() * 256);
    const g = Math.floor(Math.random() * 256);
    const b = Math.floor(Math.random() * 256);
    return `rgb(${r},${g},${b})`;
  }



  ngOnInit() {
    this.dataService.getDataInfo().subscribe(bundle => {
      this.years = bundle.years;
      this.countries = bundle.countries;
      this.IC_ECommerce = bundle.iC_ECommerce;
      this.IC_InternetUse = bundle.iC_InternetUse;
    });
  }
  createChart() {
    if (this.chart != null) this.chart.destroy();
    Chart.register(...registerables);
    this.isHidden = false;

    //creating filters
    let dataFilterEC: DataFilter = {
      years: this.selectedYears,
      countries: this.selectedCountries,
      criteria: this.selectedIC_EC
    };
    let dataFilterIU: DataFilter = {
      years: this.selectedYears,
      countries: this.selectedCountries,
      criteria: this.selectedIC_IU
    };

    //geting data from endpoint
    this.dataService.getDataEC(dataFilterEC).subscribe(data => {
      this.dataEC = data;
      this.dataService.getDataIU(dataFilterIU).subscribe(data => {
        this.dataIU = data;

        // Prepare the data for ecommerce
        let labels = Array.from(new Set(this.dataEC.map((item: DataModel) => item.year))).sort();
        let datasets: LineItem[] = [];
        let linesArray: { [key: string]: LineItem } = {};

        for (let item of this.dataEC) {
          let key = `${item.country} - ${item.individualCriteria}`;

          if (!linesArray[key]) {
            linesArray[key] = {
              label: key,
              data: [],
              fill: false,
              borderColor: this.getRandomRGB(),
            };
          }

          let index = labels.indexOf(item.year);
          linesArray[key].data[index] = item.value;
        }

        //Prepare the data for internetuse
        for (let item of this.dataIU) {
          let key = `${item.country} - ${item.individualCriteria}`;

          if (!linesArray[key]) {
            linesArray[key] = {
              label: key,
              data: [],
              fill: false,
              borderColor: this.getRandomRGB(),
            };
          }

          let index = labels.indexOf(item.year);
          linesArray[key].data[index] = item.value;
        }

        for (let key in linesArray) {
          datasets.push(linesArray[key]);
        }

        this.chart = new Chart("MyChart", {
          type: 'line',
          data: {
            labels: labels,
            datasets: datasets
          },
          options: {
            aspectRatio: 2.5,
            scales: {
              y: {
                beginAtZero: true
              }
            }
          }
        });
      });
      });



  }

  deleteChart() {
    this.dataEC = [];
    this.dataIU = [];
    this.selectedCountries = [];
    this.selectedIC_EC = [];
    this.selectedYears = [];
    this.selectedIC_IU = [];
    this.chart.destroy();
    this.isHidden = true;

  }

}
