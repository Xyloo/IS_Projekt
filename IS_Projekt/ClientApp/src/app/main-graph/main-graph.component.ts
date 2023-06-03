import { Component } from '@angular/core';
import { DataService } from '../data.service';
import { Chart, registerables } from 'chart.js';
import { DataFilter } from '../data-filter';
import { DataModel } from '../data-model';

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

  selectedYears = [];
  selectedCountries = [];
  selectedIC_EC = [];
  selectedIC_IU = [];

  public chart: any;
  isHidden: boolean = true; 

  constructor(private dataService: DataService) { }


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

    let tempDataFilter: DataFilter = {
      years: [2020, 2019, 2018],
      countries: ["Italy"],
      criteria: ["I_BLT12"]
    };

    let tempDataFilter2: DataFilter = {
      years: [2020, 2019, 2018],
      countries: ["Italy"],
      criteria: ["I_ILT12"]
    };

    this.dataService.getDataEC(tempDataFilter).subscribe(data => {
      this.dataEC = data;
    });
    this.dataService.getDataIU(tempDataFilter2).subscribe(data => {
      this.dataIU = data;
    });

    console.log(this.dataIU);
    console.log("EC", this.dataEC);

    this.chart = new Chart("MyChart", {
      type: 'line', //this denotes tha type of chart

      data: {// values on X-Axis
        labels: ['2022-05-10', '2022-05-11', '2022-05-12', '2022-05-13',
          '2022-05-14', '2022-05-15', '2022-05-16', '2022-05-17'],
        datasets: [
          {
            label: "Sales",
            data: ['467', '576', '572', '79', '92',
              '574', '573', '576'],
            backgroundColor: 'blue'
          },
          {
            label: "Profit",
            data: ['542', '542', '536', '327', '17',
              '0.00', '538', '541'],
            backgroundColor: 'limegreen'
          }
        ]
      },
      options: {
        aspectRatio: 2.5
        
/*        ,plugins: {
          title: {
            display: true,
              text: "Simple Chart",

          }
        }*/
      }

    });
  }

  deleteChart() {
    this.chart.destroy();
    this.isHidden = true;
  }

}
