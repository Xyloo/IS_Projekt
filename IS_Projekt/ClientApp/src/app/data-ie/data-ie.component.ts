import { HttpClient, HttpEventType, HttpResponse } from '@angular/common/http';
import { Component, Input } from '@angular/core';
import { finalize, Subscription } from 'rxjs';
import { saveAs } from 'file-saver';

@Component({
  selector: 'app-data-ie',
  templateUrl: './data-ie.component.html',
  styleUrls: ['./data-ie.component.css']
})
export class DataIEComponent {

  selectedFile: string = 'internetuse.json';
  selectedFileName?: string | null = null;
  fileName = '';

  uploadProgress: number | null = null;
  uploadSub: Subscription;

  fileUpload: File | null;
  importURL = ``;

  constructor(private http: HttpClient) { }

  ngOnInit() {
  }

  onFileSelected(event: Event) {
    const fileInput = event.target as HTMLInputElement;
    const file: File | null = fileInput.files?.[0] || null;
    this.selectedFileName = file?.name;
    this.fileName = this.selectedFileName ? this.selectedFileName : '';

    if (file) {
      const fileExtension = file.name.split('.').pop()?.toLowerCase();

      if (fileExtension === 'json' || fileExtension === 'xml') {
        this.selectedFileName = this.fileName;
        this.fileUpload = file;
        this.importURL = `/api/${fileExtension}/import/${this.fileName.split(".")[0]}`;

      } else {
        // Invalid file extension
        console.log('Invalid file type. Please choose a JSON or XML file.');
        this.selectedFileName = null;
      }
    } else {
      this.selectedFileName = null;
    }

  }

  uploadFile() { 
    if (this.fileUpload) {
      
      const formData = new FormData();
      formData.append("file", this.fileUpload, this.fileUpload.name);
      const upload$ = this.http.post(this.importURL, formData, {
        reportProgress: true,
        observe: 'events'
      }).pipe(
        finalize(() => this.reset())
      );

      upload$.subscribe(event => {
        if (event.type === HttpEventType.UploadProgress) {
          // This is an upload progress event. Compute and show the % done:
          const percentDone = Math.round(100 * event.loaded / event.total!);
          console.log(`File is ${percentDone}% uploaded.`);
        } else if (event instanceof HttpResponse) {
          console.log('File is completely uploaded!');
        }
      },
        error => {
          // Handle error here
          console.error('An error occurred while uploading the file.', error);
        });

    }
  }

  reset() {
    this.uploadProgress = null;
    this.fileName = '';
  }


  downloadFile() {
    let temp = this.selectedFile.split(".");
    console.log("temp: ", temp)
    let exportUrl = `/api/${temp[1]}/export/${temp[0]}`

    this.http.get(exportUrl, { responseType: 'blob' }).subscribe((res: Blob) => {
      saveAs(res, `${temp[0]}_export.${temp[1]}`);
    });
  }

}
