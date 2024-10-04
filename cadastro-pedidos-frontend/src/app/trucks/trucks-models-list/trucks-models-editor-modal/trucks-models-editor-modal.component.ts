import {Component, Inject} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";
import {TruckModelType} from "../../api/models/truck-model-type";
import {TruckModelInput} from "../../api/models/truck-model-input";
import {TrucksModelsApiService} from "../../api/services/trucks-models-api.service";
import {TruckModelOutput} from "../../api/models/truck-model-output";

@Component({
  selector: 'app-trucks-models-editor-modal',
  templateUrl: './trucks-models-editor-modal.component.html',
  styleUrls: ['./trucks-models-editor-modal.component.scss']
})
export class TrucksModelsEditorModalComponent {
  public form!: FormGroup;

  public readonly TruckModelType = TruckModelType;

  public types: TruckModelType[] = [
    TruckModelType.Fh,
    TruckModelType.Fm
  ];

  private isCreating = false;
  private currentYear = Number(new Date().getFullYear());
  private nextYear = this.currentYear + 1;

  public years: number[] = [this.currentYear, this.nextYear];

  public get canSave(): boolean {
    return this.form.valid && this.form.dirty;
  }

  constructor(private formBuilder: FormBuilder, private matDialogRef: MatDialogRef<TrucksModelsEditorModalComponent>,
              private service: TrucksModelsApiService, @Inject(MAT_DIALOG_DATA) private truckModel: TruckModelOutput) {
    this.isCreating = !truckModel;
    this.createForm();
  }

  public closeModal(): void {
    this.matDialogRef.close();
  }

  public save(): void {
    const input: TruckModelInput = this.form.getRawValue();

    const action = this.isCreating ? this.service.create(input) : this.service.update(input.id, input);

    action.subscribe(() => {
      this.matDialogRef.close();
    });
  }

  private createForm(): void {
    if (this.isCreating) {
      this.truckModel = {
        id: '',
        year: this.currentYear,
        name: '',
        type: TruckModelType.Fh,
      };
    }

    this.form = this.formBuilder.group({
      id: [this.truckModel.id, [Validators.required]],
      name: [this.truckModel.name, [Validators.required]],
      type: [this.truckModel.type, [Validators.required]],
      year: [this.truckModel.year, [Validators.required, Validators.min(this.currentYear), Validators.max(this.nextYear)]],
    });
  }
}
