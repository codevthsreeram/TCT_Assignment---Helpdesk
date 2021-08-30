import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';

@Injectable()

export class CommonService {
  private actualUrl: string = this.baseUrl + this.module;
  constructor(public http: HttpClient, public baseUrl: string, public module: string) {

  }
  getAll() {
    return this.http.get(this.actualUrl);
  }
  getById(id: any) {
    return this.http.get(this.actualUrl + '/' + id);
  }
  create(resouce: any) {
    return this.http.post(this.actualUrl, resouce);
  }
  update(id: any, resouce: any) {
    return this.http.put(this.actualUrl + '/' + id, resouce);
  }
  delete(id: any) {
    return this.http.delete(this.actualUrl + '/' + id);
  }
}
