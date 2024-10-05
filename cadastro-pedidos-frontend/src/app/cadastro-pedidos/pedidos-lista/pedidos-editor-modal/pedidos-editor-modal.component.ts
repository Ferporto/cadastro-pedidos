import {Component, Inject} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";
import {CadastroPedidosApiService} from "../../api/services/cadastro-pedidos-api.service";
import {TruckOutput} from "../../api/models/truck-output";
import {TruckInput} from "../../api/models/truck-input";
import {TruckModelType} from "../../api/models/truck-model-type";
import {TruckModelOutput} from "../../api/models/truck-model-output";
import {CadastroPedidosModelsApiService} from "../../api/services/cadastro-pedidos-models-api.service";

@Component({
  selector: 'app-cadastro-pedidos-editor-modal',
  templateUrl: './cadastro-pedidos-editor-modal.component.html',
  styleUrls: ['./cadastro-pedidos-editor-modal.component.scss']
})
export class CadastroPedidosEditorModalComponent {
  public form!: FormGroup;
  public truckModels: TruckModelOutput[] = [];

  public readonly TruckModelType = TruckModelType;

  private isCreating = false;
  private currentYear = Number(new Date().getFullYear());

  public get canSave(): boolean {
    return this.form.valid && this.form.dirty;
  }

  constructor(private formBuilder: FormBuilder, private matDialogRef: MatDialogRef<CadastroPedidosEditorModalComponent>,
              private service: CadastroPedidosApiService, @Inject(MAT_DIALOG_DATA) private truck: TruckOutput,
              private cadastro-pedidosModelService: CadastroPedidosModelsApiService) {
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

    this.cadastroPedidosModelService.getList().subscribe((cadastro-pedidosModels: TruckModelOutput[]) => {
      this.truckModels = cadastro-pedidosModels;
    })
  }
}
