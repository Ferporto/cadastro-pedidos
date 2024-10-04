import {Component, Inject} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";
import {TrucksApiService} from "../../api/services/trucks-api.service";
import {TruckOutput} from "../../api/models/truck-output";
import {TruckInput} from "../../api/models/truck-input";
import {TruckModelType} from "../../api/models/truck-model-type";
import {TruckModelOutput} from "../../api/models/truck-model-output";
import {TrucksModelsApiService} from "../../api/services/trucks-models-api.service";

@Component({
  selector: 'app-trucks-editor-modal',
  templateUrl: './trucks-editor-modal.component.html',
  styleUrls: ['./trucks-editor-modal.component.scss']
})
export class TrucksEditorModalComponent {
  public form!: FormGroup;
  public truckModels: TruckModelOutput[] = [];

  public readonly TruckModelType = TruckModelType;

  private isCreating = false;
  private currentYear = Number(new Date().getFullYear());

  public get canSave(): boolean {
    return this.form.valid && this.form.dirty;
  }

  constructor(private formBuilder: FormBuilder, private matDialogRef: MatDialogRef<TrucksEditorModalComponent>,
              private service: TrucksApiService, @Inject(MAT_DIALOG_DATA) private truck: TruckOutput,
              private trucksModelService: TrucksModelsApiService) {
    this.isCreating = !truck;
    this.createForm();
  }

  public closeModal(): void {
    this.matDialogRef.close();
  }

  public save(): void {
    const input: TruckInput = this.form.getRawValue();

    const action = this.isCreating ? this.service.create(input) : this.service.update(input.id, input);

    action.subscribe(() => {
      this.matDialogRef.close();
    });
  }

  private createForm(): void {
    if (this.isCreating) {
      this.truck = {
        id: '',
        manufacturingYear: this.currentYear,
      };
    }

    this.form = this.formBuilder.group({
      id: [this.truck.id, [Validators.required]],
      licensePlate: [this.truck.licensePlate],
      modelId: [this.truck.modelId, [Validators.required]],
      manufacturingYear: [this.truck.manufacturingYear, [Validators.required]],
    });

    this.trucksModelService.getList().subscribe((trucksModels: TruckModelOutput[]) => {
      this.truckModels = trucksModels;
    })
  }
}
