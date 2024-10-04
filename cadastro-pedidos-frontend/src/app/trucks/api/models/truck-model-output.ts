import {TruckModelType} from "./truck-model-type";

export interface TruckModelOutput {
  id: string;
  name: string;
  type: TruckModelType;
  year: number;
}
