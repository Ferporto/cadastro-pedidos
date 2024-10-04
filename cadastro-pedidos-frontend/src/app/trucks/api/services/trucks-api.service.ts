import {Inject, Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";

import {Observable} from "rxjs";

import {TruckInput} from "../models/truck-input";
import {TruckOutput} from "../models/truck-output";

@Injectable({providedIn: 'root'})
export class TrucksApiService {
  constructor(private httpClient: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
  }

  public create(input: TruckInput): Observable<never> {
    return this.httpClient.post<never>(this.basePath, input);
  }

  public get(id: string): Observable<TruckOutput> {
    return this.httpClient.get<TruckOutput>(`${this.basePath}/${id}`);
  }

  public getList(): Observable<TruckOutput[]> {
    return this.httpClient.get<TruckOutput[]>(this.basePath);
  }

  public update(id: string, input: TruckInput): Observable<never> {
    return this.httpClient.put<never>(`${this.basePath}/${id}`, input);
  }

  public delete(id: string): Observable<never> {
    return this.httpClient.delete<never>(`${this.basePath}/${id}`);
  }

  private get basePath(): string {
    return `${this.baseUrl}trucks`;
  }
}
