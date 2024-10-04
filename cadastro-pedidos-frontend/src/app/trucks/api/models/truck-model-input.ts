import {TruckModelType} from "./truck-model-type";

export interface TruckModelInput {
  id: string;
  name: string;
  type: TruckModelType;
  year: number;
}
