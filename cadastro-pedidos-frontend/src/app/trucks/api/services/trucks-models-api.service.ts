import {Inject, Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {TruckModelOutput} from "../models/truck-model-output";
import {TruckModelInput} from "../models/truck-model-input";

@Injectable({providedIn: 'root'})
export class TrucksModelsApiService {
  constructor(private httpClient: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
  }

  public create(input: TruckModelInput): Observable<never> {
    return this.httpClient.post<never>(this.basePath, input);
  }

  public get(id: string): Observable<TruckModelOutput> {
    return this.httpClient.get<TruckModelOutput>(`${this.basePath}/${id}`);
  }

  public getList(): Observable<TruckModelOutput[]> {
    return this.httpClient.get<TruckModelOutput[]>(this.basePath);
  }

  public update(id: string, input: TruckModelInput): Observable<never> {
    return this.httpClient.put<never>(`${this.basePath}/${id}`, input);
  }

  public delete(id: string): Observable<never> {
    return this.httpClient.delete<never>(`${this.basePath}/${id}`);
  }

  private get basePath(): string {
    return `${this.baseUrl}truck-models`;
  }
}
